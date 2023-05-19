using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DignosisMaster;
using PathoLab.IRepository.DignosisMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class DignosisController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IDignosisRepository _dignosisRepository;
        public IConfiguration Configuration { get; }
        public DignosisController(IHostingEnvironment hostingEnvironment, IDignosisRepository dignosisRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _dignosisRepository = dignosisRepository;
        }
        public async Task<IActionResult> AddDignosis()
        {
            List<Dignosis> pc4 = new List<Dignosis>();
            pc4 = await _dignosisRepository.BindDignosisCatagoryName(new Dignosis());
            pc4.Insert(0, new Dignosis { DignosisCatagoryID = 0, DignosisCategoryName = "Select" });
            ViewBag.Catagory = pc4;
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
        public async Task<IActionResult> ViewDignosis()
        {
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            List<Dignosis> pc4 = new List<Dignosis>();
            pc4 = await _dignosisRepository.BindDignosisCatagoryName(new Dignosis());
            pc4.Insert(0, new Dignosis { DignosisCatagoryID = 0, DignosisCategoryName = "Select" });
            ViewBag.Catagory = pc4;
            List<Dignosis> dl = new List<Dignosis>();
            List<Dignosis> dle = new List<Dignosis>();
            dl = await _dignosisRepository.GetAll(new Dignosis());
            foreach(Dignosis d in dl)
            {
                d.EncodDignosisID =  Encrypt(d.DignosisID.ToString());
                dle.Add(d);
            }
            
            // ViewBag.Result = await _dignosisRepository.GetAll(new Dignosis());
            ViewBag.Result = dle;

            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}
            
        }
     
        [HttpPost]

        public async Task<IActionResult> ViewDignosis(Dignosis d)

        {
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            List<Dignosis> pc4 = new List<Dignosis>();
            pc4 = await _dignosisRepository.BindDignosisCatagoryName(new Dignosis());
            pc4.Insert(0, new Dignosis { DignosisCatagoryID = 0, DignosisCategoryName = "Select" });
            ViewBag.Catagory = pc4;
           

            ViewBag.Result = await _dignosisRepository.GetAll(d);
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}

        }
        [HttpPost]
        public async Task<JsonResult> AddDignosis(Dignosis entity)
        {
            try
            {
              
             
                 if ((!Regex.IsMatch(entity.Name, @"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$")))
                {
                    return Json("Name  Is  Invalid");
                }
                else
                {
                    int retMsg = _dignosisRepository.Create(entity).Result;

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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult DeleteDignosis(int DignosisID)
        {
            try
            {
                int Result = _dignosisRepository.Delete(DignosisID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult DignosisGetById(string DignosisID)
        {
            var id = DignosisID + "=";
            int DignosisIDD = Convert.ToInt32(Decrypt(id));
            var Dignosiss = _dignosisRepository.GetOne(Convert.ToInt32(DignosisIDD)).Result;
            return Ok(JsonConvert.SerializeObject(Dignosiss));
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


