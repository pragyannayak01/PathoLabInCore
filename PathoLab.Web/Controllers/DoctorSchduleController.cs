using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.Domain.DoctorMaster;
using PathoLab.IRepository.DoctorMaster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using PathoLab.IRepository.PrescriptionMaster;
using PathoLab.Domain.PrescriptionMaster;

namespace PathoLab.Web.Controllers
{
    public class DoctorSchduleController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDoctorSchedule _doctorSchedule;
        private IPrescriptionRepository _prescriptionRepository;

        public IConfiguration Configuration { get; }
        public DoctorSchduleController(IHostingEnvironment hostingEnvironment, IDoctorSchedule doctorSchedule, IPrescriptionRepository prescriptionRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _doctorSchedule = doctorSchedule;
            _prescriptionRepository = prescriptionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            int x = (int)HttpContext.Session.GetInt32("UserId");

            ViewBag.Result = await _doctorSchedule.GetOne((int)x);

            return View();
        }
        public IActionResult Prescription()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPatientDetailsByPatientID(int PatientID)
        {
            var Doctors = _prescriptionRepository.GetPatientDetailsByPatientID(Convert.ToInt32(PatientID)).Result;
            return Ok(JsonConvert.SerializeObject(Doctors));
        }
        [HttpPost]
        public async Task<JsonResult> CreatePrescription(Prescription entity)
        {
            try
            {
                List<string> Dignosis = entity.DignosisCommaSepareted.Split(',').ToList();
                if (entity.PrescriptionId != 0 )
                {
                    //First Delete And Then Update The Prescribe Dignosis Data
                    int retdMsg = _prescriptionRepository.DeleteToUpdatePresDig(entity.PrescriptionId).Result;
                    //First Delete And Then Update The Prescribe Medicine Data
                    int retmMsg = _prescriptionRepository.DeleteToUpdatePresMed(entity.PrescriptionId).Result;
                    int retMsg = _prescriptionRepository.CreatePrescription(entity).Result;//retMsg-Carry prescriptionId
                    if(Dignosis!=null)
                    {
                        foreach (var dignosis in Dignosis)
                        {
                            entity.DignosisID = Convert.ToInt32(dignosis);
                            int retMsgPD = _prescriptionRepository.CreatePresDignosis(entity).Result;
                        }
                    }
                    if(entity.Medicines!=null)
                    {
                        foreach (var medicine in entity.Medicines)
                        {
                            medicine.PrescriptionId = entity.PrescriptionId;
                            int retMsgPM = _prescriptionRepository.CreatePresMedicine(medicine).Result;
                        }
                    }
                    return Json("Prescription Updated Successfully");
                }
                else
                {
                    int retMsg = _prescriptionRepository.CreatePrescription(entity).Result;//retMsg-Carry prescriptionId
                    if (retMsg != 0)
                    {
                        int retMsgPD = 0;//PrescribeDignosisId
                        if (Dignosis != null)
                        {
                            foreach (var dignosis in Dignosis)
                            {
                                entity.DignosisID = Convert.ToInt32(dignosis);
                                if (entity.DignosisID != 0){
                                    entity.PrescriptionId = retMsg;
                                    retMsgPD = _prescriptionRepository.CreatePresDignosis(entity).Result;
                                }
                                
                            }
                        }
                        int retMsgPM = 0;//PrescribeMedicineId
                        if (entity.Medicines != null)
                        {
                            foreach (var medicine in entity.Medicines)
                            {
                                if (medicine.Id != null)
                                {
                                    medicine.PrescriptionId = retMsg;
                                    retMsgPM = _prescriptionRepository.CreatePresMedicine(medicine).Result;
                                }
                                
                            }
                        }
                        return Json("Prescription Saved Successfully");
                    }
                    else
                    {
                        return Json("Some Error Occurs");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetDignosis()
        {
            var dignosis = _prescriptionRepository.GetDignosis().Result;
            return Ok(JsonConvert.SerializeObject(dignosis));
        }

        [HttpGet]
        public IActionResult GetDignosisByPId(int PrescriptionId)
        {
            var dignosis = _prescriptionRepository.GetAllSelectedDignosis(PrescriptionId).Result;
            return Ok(JsonConvert.SerializeObject(dignosis));
        }

        [HttpGet]
        public IActionResult GetMedicineByPId(int PrescriptionId)
        {
            var medicine = _prescriptionRepository.GetAllSelectedMedicine(PrescriptionId).Result;
            return Ok(JsonConvert.SerializeObject(medicine));
        }

        //Code For AutoFill Medicine Name
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var prescription = (from Prescription in _prescriptionRepository.GetMedicineName().Result
                                where Prescription.Name.ToUpper().StartsWith(prefix.ToUpper())
                                select new
                                {
                                    label = Prescription.Name,
                                    val = Prescription.Id
                                }).ToList();

            return Json(prescription);
        }

        public async Task<IActionResult> ViewsPrescription()
        {
                ViewBag.Result = await _prescriptionRepository.GetAllPrescriptionDetails(new Prescription());
                return View();
        }
        
        public async Task<IActionResult> EditPrescription()
        {
            return View();
        }
       
        public IActionResult GetPrescriptionDetailsByPId(int PrescriptionId, int PatientID)
        {
           
            var Prescription = _prescriptionRepository.GetPrescriptionDetailsByPId(Convert.ToInt32(PrescriptionId), Convert.ToInt32(PatientID)).Result;
            return Ok(JsonConvert.SerializeObject(Prescription));
        }
        public async Task<IActionResult> PrintPrescription()
        {
            return View();
        }

        public IActionResult PrintPrescriptionDetailsByPId(int PatientID,int PrescriptionId)
        {
            var printpres = _prescriptionRepository.PrintPrescriptionDetailsByPId(Convert.ToInt32(PatientID),Convert.ToInt32(PrescriptionId)).Result;
            return Ok(JsonConvert.SerializeObject(printpres));
        }

        [HttpPost]
        public IActionResult DeletePrescription(int PrescriptionId)
        {
            try
            {
                int Result = _prescriptionRepository.Delete(PrescriptionId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult GetHospitalDetails(int HospitalID)
        {
            var pathobill = _prescriptionRepository.GetHospitalDetails(Convert.ToInt32(HospitalID)).Result;
            return Ok(JsonConvert.SerializeObject(pathobill));
        }
    }
}
