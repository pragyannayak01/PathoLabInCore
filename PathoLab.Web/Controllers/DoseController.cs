using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DoseMaster;
using PathoLab.IRepository.DoseMaster;
using PathoLab.IRepository.Medicine_Master;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class DoseController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly DoseInterface _dose;
        private readonly IMedicine _medicine;
        public IConfiguration Configuration { get; }
        public DoseController(IHostingEnvironment hostingEnvironment, DoseInterface dose, IMedicine med, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
             Configuration = configuration;
            _dose = dose;
            _medicine = med;
        }
        public async Task<IActionResult> AddDose()
        {
            //List<Dose> pc4 = new List<Dose>();
            //pc4 = await _dose.BindMedicine();
            //pc4.Insert(0, new Dose { id = 0, Name = "Select" });
            //ViewBag.Medicine = pc4;

            //List<Dose> pc4 = new List<Dose>();
            //pc4 = await _dose.BindMedicine();
            //pc4.Insert(0, new Dose { id = 0, Name = "Select" });
            //ViewBag.Medicine = pc4;
            ViewBag.MedicineCategory = _medicine.CatagoryBind().Result;

            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}
        }
        public async Task<IActionResult> ViewDose()
        {
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            //List<Dose> pc4 = new List<Dose>();
            //pc4 = await _dose.BindDoseCatagoryName(new Dose());
            //pc4.Insert(0, new Dose { DoseCatagoryID = 0, DoseCategoryName = "Select" });
            //ViewBag.Catagory = pc4;
            //List<Dose> pc4 = new List<Dose>();
            //pc4 = await _dose.BindMedicine();
            //pc4.Insert(0, new Dose { id = 0, Name = "Select" });
            //ViewBag.Medicine = pc4;
            ViewBag.MedicineCategory = _medicine.CatagoryBind().Result;
            List<Dose> dl = new List<Dose>();
            List<Dose> dle = new List<Dose>();
            dl = await _dose.GetAll(new Dose());
            foreach (Dose d in dl)
            {

                d.DoseIdString = Encrypt(d.DoseId.ToString());
                dle.Add(d);
            }

            //ViewBag.Result = await _dose.GetAll(new Dose());
            ViewBag.Result = dle;

            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}

        }

        [HttpPost]

        public async Task<IActionResult> ViewDose(Dose d)
        {
            List<Dose> pc4 = new List<Dose>();
            pc4 = await _dose.BindMedicine();
            pc4.Insert(0, new Dose { id = 0, Name = "Select" });
            ViewBag.Medicine = pc4;
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            //List<Dose> pc4 = new List<Dose>();
            //pc4 = await _dose.BindDoseCatagoryName(new Dose());
            //pc4.Insert(0, new Dose { DoseCatagoryID = 0, DoseCategoryName = "Select" });
            //ViewBag.Catagory = pc4;


            ViewBag.Result = await _dose.GetAll(d);
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}

        }
        [HttpPost]
        public async Task<JsonResult> AddDose(Dose entity)
        {
            try
            {


                //if ((!Regex.IsMatch(entity.Name, @"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$")))
                //{
                //    return Json("Name  Is  Invalid");
                //}
                //else
                //{
                    int retMsg = _dose.Create(entity).Result;

                    if (retMsg == 1)
                    {
                        return Json("Record Saved Successfully");
                    }
                    else if (retMsg == 2)
                    {
                        return Json("Record Updated Successfully");
                    }
                    else
                    {
                        return Json("Record Already Exist");
                    }
               // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult DeleteDose(int dd)
        {
            try
            {
                int Result = _dose.Delete(dd).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult GetSubCatByMId(int MedicineId)
        {
           
            var Doses = _dose.GetSubCatagoryNameByMedicineId(MedicineId).Result;
            return Ok(JsonConvert.SerializeObject(Doses));
        }
        [HttpGet]
        public IActionResult DoseGetById(string DignosisID)
        {
            var id = DignosisID + "=";
            int DoseIDD = Convert.ToInt32(Decrypt(id));
            var Doses = _dose.GetById(Convert.ToInt32(DoseIDD)).Result;
            return Ok(JsonConvert.SerializeObject(Doses));
        }
        [HttpGet]
        public IActionResult DoseGetByMedId(string MedicineId) 
        {
            var Doses = _dose.DosesGetByCatId(Convert.ToInt32(MedicineId)).Result;
            return Ok(JsonConvert.SerializeObject(Doses));
        }
        

        public string Encrypt(string strToDecrypt)
        {
            string key = "C#S@$R%!";
            byte[] inputByptArr;
            byte[] keyBytes;
            string strToEncrypt;
            if (string.IsNullOrWhiteSpace(strToDecrypt))
            {
                strToEncrypt = null;
            }
            else
            {
                try
                {
                    inputByptArr = ASCIIEncoding.ASCII.GetBytes(strToDecrypt);
                    keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

                    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                    ICryptoTransform transform = provider.CreateEncryptor(keyBytes, keyBytes);
                    CryptoStreamMode mode = CryptoStreamMode.Write;

                    MemoryStream memStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
                    cryptoStream.Write(inputByptArr, 0, inputByptArr.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] encryptedInputByteArr = new byte[memStream.Length];
                    memStream.Position = 0;
                    memStream.Read(encryptedInputByteArr, 0, encryptedInputByteArr.Length);

                    strToEncrypt = Convert.ToBase64String(encryptedInputByteArr).Replace('/', '_').Replace('+', '-');
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return strToEncrypt;
        }
        public string Decrypt(string strToEncrypt)
        {
            string key = "C#S@$R%!";
            byte[] encryptedInputByteArr;
            byte[] keyBytes;
            string decryptedMsg;
            if (string.IsNullOrWhiteSpace(strToEncrypt))
            {
                decryptedMsg = null;
            }
            else
            {
                try
                {
                    //strToEncrypt = strToEncrypt.Trim();
                    strToEncrypt = strToEncrypt.Replace('-', '+').Replace('_', '/');
                    byte[] data = Encoding.UTF8.GetBytes(strToEncrypt);
                    encryptedInputByteArr = Convert.FromBase64String(strToEncrypt);
                    keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

                    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                    ICryptoTransform transform = provider.CreateDecryptor(keyBytes, keyBytes);
                    CryptoStreamMode mode = CryptoStreamMode.Write;

                    MemoryStream memStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
                    cryptoStream.Write(encryptedInputByteArr, 0, encryptedInputByteArr.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] decryptedInputByteArr = new byte[memStream.Length];
                    memStream.Position = 0;
                    memStream.Read(decryptedInputByteArr, 0, decryptedInputByteArr.Length);

                    decryptedMsg = ASCIIEncoding.ASCII.GetString(decryptedInputByteArr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return decryptedMsg;
        }

    }


}
