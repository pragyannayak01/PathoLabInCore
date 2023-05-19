using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.PathoBilMasterl;
using PathoLab.Domain.PrescriptionMaster;
using PathoLab.IRepository.Account;
using PathoLab.IRepository.DignosisConfigurationMaster;
using PathoLab.IRepository.DignosisMaster;
using PathoLab.IRepository.DoctorMaster;
using PathoLab.IRepository.LabTestMaster;
using PathoLab.IRepository.PathoBillMaster;
using PathoLab.IRepository.PrescriptionMaster;
using PathoLab.IRepository.UserRegistration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using IronBarCode;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using Serilog;

namespace PathoLab.Web.Controllers
{
    public class PathoLabController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IPathoBillRepository _pathoBill;
        private IUser _user;
        private IDignosisConfigurationRepository _dignosisConfiguration;
        private ILabtest _labtest;

        public IConfiguration Configuration { get; }
        public PathoLabController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IPathoBillRepository pathoBill, IUser user, IDignosisConfigurationRepository dignosisConfiguration, ILabtest labtest)
        {
            _hostingEnvironment = hostingEnvironment;
            _pathoBill = pathoBill;
            _user = user;
            _dignosisConfiguration = dignosisConfiguration;
            _labtest = labtest;
        }

        public async Task<IActionResult> PathoLabBill()//Patho Lab Bill
        {
            Log.Information("PathoLabBill Started");
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.ReportedOn = _pathoBill.BindCurrentDate().Result.ReportedOn;
                ViewBag.CollectionId = _pathoBill.BindCollection().Result.CollectionId;
                ViewBag.DoctorName = _user.DoctorDDL().Result;// DropDown (Fill Doctor name)
                //string otp = GetRandomText();//Auto Generate 10 digit code
                //ViewBag.no = otp;//Auto Generate 10 digit code
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }


        [HttpPost]
        public async Task<JsonResult> InsertPathoBill(PathoBill entity)//insert patho lab bill
        {
            Log.Information("InsertPathoBill ");
            try
            {
                //entity.Barcode = generateBarcode();
                int retMsg = _pathoBill.InsertPathoBill(entity).Result;//retMsg-Carry PathoBillId
                if (retMsg != 0)
                {

                    int retMsgPM = 0;//PrescribeMedicineId
                    if (entity.Tests != null)
                    {
                        foreach (var test in entity.Tests)
                        {
                            if (test.LabTestId != 0)
                            {
                                test.PathoBillId = retMsg;
                                retMsgPM = _pathoBill.InsertingTestFields(test).Result;
                            }

                        }
                    }
                    return Json("PathoBill Saved Successfully");
                }

                else
                {
                    return Json("Some Error Occurs");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }
        }
        public async Task<JsonResult> InsertPathoReportCollection(PathoBill entity)//insert patho report Collection
        {
            Log.Information("InsertPathoReportCollection ");
            try
            {
                int retMsgg = 1;
                //int retMsgg = _pathoBill.UploadFileByBillAndLabTestId(entity).Result;//retMsg-Carry PathoBillId
                int retMsg = _pathoBill.DeleteToUpdateReportCollection(entity.PathoBillId, entity.LabTestId).Result;//retMsg-Carry PathoBillId
                if (retMsgg != 0 && retMsg != 0)
                {
                    int retMsgPM = 0;
                    if (entity.TestValues != null)
                    {
                        foreach (var value in entity.TestValues)
                        {
                            if (value.DignosisConfigId != 0)
                            {
                                retMsgPM = _pathoBill.InsertPathoReportCollection(value).Result;
                            }

                        }
                    }
                    return Json("PathoLab Record Saved Successfully");
                }
                else
                {
                    return Json("Some Error Occurs");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }

        }
        public async Task<IActionResult> PathoReportCollectionForPrint()//Patho Report View Page Before Print
        {
            Log.Information("Patho Report Collection For Print ");
            ViewBag.Result = await _pathoBill.GetAllPathoBillRecord(new PathoBill());
            return View();
        }
        
        public async Task<IActionResult> ViewPathoLabCollection()// Patho Report Collection  View Page
        {
            Log.Information("View PathoLab Collection ");
            ViewBag.Result = await _pathoBill.GetAllPathoBill(new PathoBill());
            return View();
        }
        public async Task<IActionResult> ViewsPathoLabReport()
        {
            Log.Information(" Views PathoLab Report  ");
            return View();
        }
        public async Task<IActionResult> CollectSample(int PathoBillId)//  Sample Collection  View Page
        {
            Log.Information(" Collect Sample  ");
            var pathobill = _pathoBill.GetPatientDetailsByPathoBillId(Convert.ToInt32(PathoBillId),0).Result;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SampleCollectionInfo(PathoBill entity)
        {
            Log.Information(" Sample Collection Information");
            try
            {
                int retMsg = _pathoBill.CreateInfo(entity).Result;
                if (retMsg != 0)
                {
                    int retMsgPM = 0;
                    if (entity.CollectionSample != null)
                    {
                        foreach(var samplecol in entity.CollectionSample)
                        {
                            if (samplecol.LabTestId != 0)
                            {
                                samplecol.SampleColNo = retMsg;
                                samplecol.PathoBillId = entity.PathoBillId;
                                retMsgPM = _pathoBill.CreateSample(samplecol).Result;
                            }
                        }
                    }
                    return Json("Data Saved Successfully");
                }
                else
                {
                    return Json("Some Error Occurs");
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }
        }
        public async Task<IActionResult> PrintBill()
        {
            Log.Information(" Print Bill");
            return View();
        }
        [HttpGet]
        public IActionResult PrintPathoBill(int PathoBillId)
        {
            Log.Information(" Print Patho Bill");
            var pathobill = _pathoBill.GetPatientDetailsByPathoBillId(Convert.ToInt32(PathoBillId),0).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }
        public async Task<IActionResult> PrintReport()
        {
            Log.Information(" Print Report");
            return View();
        }
        [HttpGet]
        public IActionResult PrintPathoReport(int PathoBillId)
        {
            Log.Information(" Print Patho Report");
            var pathobill = _pathoBill.GetPatientDetailsByPathoBillId(Convert.ToInt32(PathoBillId),0).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }
        [HttpGet]
        public IActionResult BindingTest(int PathoBillId)
        {
            Log.Information(" Binding Test");
            var pathobill = _pathoBill.BindingTest(Convert.ToInt32(PathoBillId)).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }

        //Get Patient Details By PathoBillId
        [HttpGet]
        public IActionResult GetPatientDetailsByPathoBillId(int PathoBillId,int LabTestId)
        {
            Log.Information(" Get Patient Details By Patho Bill Id");
            var result = _pathoBill.GetPatientDetailsByPathoBillId(Convert.ToInt32(PathoBillId), LabTestId).Result;
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpGet]
        public IActionResult BindingTestConfig(int PathoBillId)
        {
            Log.Information(" Binding Test Config");
            var pathobill = _pathoBill.BindingTestConfig(Convert.ToInt32(PathoBillId)).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }

        [HttpGet]
        public IActionResult GetPatientByMobile(string Mobile)
        {
            Log.Information(" Get Patient By Mobile");
            var Doctors = _pathoBill.GetPatientByMobile(Mobile).Result;
            return Ok(JsonConvert.SerializeObject(Doctors));
        }
        //Code For AutoFill Test Name
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            Log.Information("Auto Complete");
            var pathobill = (from PathoBill in _pathoBill.AutoFillTestName().Result
                             where PathoBill.LabTestName.ToUpper().StartsWith(prefix.ToUpper())
                             select new
                             {
                                 label = PathoBill.LabTestName,
                                 val = PathoBill.LabTestId
                             }).ToList();

            return Json(pathobill);
        }
        [HttpGet]
        public IActionResult GetPriceByTestId(int LabTestId)
        {
            Log.Information(" Get Price By TestId");
            var labTest = _pathoBill.GetPriceByTestId(Convert.ToInt32(LabTestId)).Result;
            if (labTest != null)
            {
                return Ok(JsonConvert.SerializeObject(labTest.Price));
            }
            else
            {
                return Ok("");
            }
        }

        [HttpPost]
        public IActionResult DeletePathoBill(int PathoBillId)
        {
            Log.Information("Delete Patho Bill");
            try
            {
                int Result = _pathoBill.DeletePathoBill(PathoBillId).Result;
                return Json(Result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }
        }

        //public ActionResult GetBarcodeByPathoBillId(int PathoBillId,int LabTestId)
        //{
        //    var patient = _pathoBill.GetPatientDetailsByPathoBillId(Convert.ToInt32(PathoBillId), Convert.ToInt32(LabTestId)).Result;
        //    var RndBarcode = patient.Barcode;
        //    Barcode128 code128 = new Barcode128();
        //    code128.CodeType = Barcode.CODE128;
        //    code128.ChecksumText = true;
        //    code128.GenerateChecksum = true;
        //    code128.StartStopText = true;
        //    code128.Code = RndBarcode;
        //    code128.Size = 1F;
        //    System.Drawing.Bitmap bm = new System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        bm.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        return Json(JsonConvert.SerializeObject(new { Barcode = RndBarcode, BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray()) }));
        //    }
        //}
        //public string generateBarcode()
        //{
        //    string RandomNum = "";
        //    StringBuilder randomText = new StringBuilder();
        //    string Number = "1234567890";
        //    Random r = new Random();
        //    for (int j = 1; j <= 12; j++)
        //    {
        //        //generating the random code
        //        randomText.Append(Number[r.Next(Number.Length)]);
        //    }
        //    RandomNum = randomText.ToString();
        //    return RandomNum;
        //}


        [HttpGet]
        public IActionResult GetHospitalDetails(int HospitalID)
        {
            Log.Information("Get Hospital Details");
            var pathobill = _pathoBill.GetHospitalDetails(Convert.ToInt32(HospitalID)).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }

        public async Task<IActionResult> ViewCollection() 
        {
            Log.Information("View Collection");
            ViewBag.Result = await _pathoBill.GetAllPathoBillRecord(new PathoBill());
            return View();
        }
       
       
    }
}
