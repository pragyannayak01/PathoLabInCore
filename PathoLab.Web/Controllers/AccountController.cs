using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.Account;
using System;
using System.Reflection;
using System.Threading.Tasks;
using PathoLab.Domain.Account;
using System.IO;
using Serilog;

namespace PathoLab.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public IConfiguration Configuration { get; }
        public AccountController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            Log.Information("Login Started");
            return View();
        }
        
        [HttpPost]
        public async Task<JsonResult> Login(string UserName, string Password)
        {
            Log.Information("Login Post Started");
            try
            {
                string pass = EncodePasswordToBase64(Password);//encode password
                var x = await _userRepository.UserGetByUserNamePwd(UserName, pass);
                if (x != null )
                {
                    //if (x.UserName == UserName && x.Password == pass && x.DesignationId ==1)
                    //{
                    //    SetSession(x);//admin
                    //    return Json(1);
                    //}
                    //else if (x.UserName == UserName && x.Password == pass && x.DesignationId ==2 )
                    //{
                    //    SetSession(x);//doctor
                    //    return Json(6);
                    //}
                    //else if (x.UserName == UserName && x.Password == pass && x.DesignationId == 5)
                    //{
                    //    SetSession(x);//frontoffice
                    //    return Json(9);
                    //}
                    //else if (x.UserName == UserName && x.Password == pass && x.DesignationId == 3)
                    //{
                    //    SetSession(x);//patient
                    //    return Json(7);
                    //}
                    //else
                    //{
                    //    SetSession(x);
                    //    return Json(12);
                    //}
                    if (x.UserName == UserName && x.Password == pass)
                    {
                        SetSession(x);//admin
                        return Json(1);
                    }

                }
                else
                {
                    return Json(2);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message+"\n"+ex.StackTrace);
                return Json(0);
            }
            return Json(0);
        }
        public void SetSession(User x)
        {
            HttpContext.Session.SetInt32("UserId", x.UserId);
            HttpContext.Session.SetString("UserName", x.UserName);
            HttpContext.Session.SetInt32("HospitalID", x.HospitalID);
            HttpContext.Session.SetString("HospitalName", x.HospitalName);
            HttpContext.Session.SetString("Password", x.Password);
            HttpContext.Session.SetString("FullName", x.FullName);
            HttpContext.Session.SetString("Email", x.Email);
            HttpContext.Session.SetString("Mobile", x.Mobile);
            HttpContext.Session.SetString("Gender", x.Gender);
            HttpContext.Session.SetInt32("DesignationId", x.DesignationId);
         
        }
        //Encode Password
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                return password;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.Designation = HttpContext.Session.GetInt32("DesignationId");
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.pwd = HttpContext.Session.GetString("Password");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public async Task<JsonResult> ChangePassword(User doc)
        {
            string s2 = EncodePasswordToBase64(doc.Password);
            doc.Password = s2;
            doc.UserName = HttpContext.Session.GetString("UserName");//get
            var x = await _userRepository.UpdatePassword(doc);
            if (x == 1)
            {
                return Json("Password Saved Successfully");
            }
            else if (x == 2)
            {
                return Json("Please check your inserted password");
            }
            return Json(x);
        }


        //logout method
        public IActionResult Logout()
        {
            //Delete the Session object.
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        //[HttpGet]
        ////[AllowAnonymous]
        //public IActionResult GetAuthCode()
        //{
        //    string code = "";
        //    MemoryStream ms = new CaptchaHelper().Create(out code);
        //    WebHelper.WriteSession(ConstParameters.VerifyCodeKeyName, Md5Hash.Md5(code.Replace(" ", "").ToString(), 16));
        //    Response.Body.Dispose();
        //    return File(ms.ToArray(), @"image/png");
        //    //return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        //}



    }
}
