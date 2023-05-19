using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.HospitalMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.Domain.HospitalMaster;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace PathoLab.Web.Controllers
{
    public class HospitalMasterController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHospitalRepository _hospitalRepository;
        public IConfiguration Configuration { get; }
        public HospitalMasterController(IHostingEnvironment hostingEnvironment, IHospitalRepository hospitalRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _hospitalRepository = hospitalRepository;
        }
        public IActionResult AddHospital()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddHospital(HospitalEntity doc)
        {
            try
            {
                if (doc.HospitalName == null)
                {
                    return Json("Please Enter Hospital Name");

                }
                else if (doc.RegstrationNo == null)
                {
                    return Json("Please Enter Regstration No");

                }
                else if (doc.LandlineNo == null)
                {
                    return Json("Please Enter LandlineNo");

                }
                else if (doc.Address == null)
                {
                    return Json("Please enter your address");
                }


                else if (doc.City == null)
                {
                    return Json("Please Enter City");

                }

                else if (doc.State == "Select")
                {
                    return Json("Please Enter State");

                }
                else if (doc.PinCode == 0)
                {
                    return Json("Please Enter PinCode");

                }
                else if (doc.ContactPerson == null)
                {
                    return Json("Please Enter ContactPerson");

                }
                else if (doc.MobielNo == null)
                {
                    return Json("Please Enter MobielNo");

                }
                else if (doc.GSTNo == null)
                {
                    return Json("Please Enter  GSTNo");

                }
                else if ((!Regex.IsMatch(doc.LandlineNo, @"^[0-9]\d{2,4}-\d{6,8}$")))
                {
                    return Json("Land No. Is Invalid");
                }
                else if ((!Regex.IsMatch(doc.MobielNo, @"^([0-9]{10})$")))
                {
                    return Json("Mobile No. Is Invalid");
                }
                else if ((!Regex.IsMatch(doc.HospitalName, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)*$", RegexOptions.IgnoreCase)))
                {

                    return Json("Name  Is  Invalid");
                }
                else
                {

                    int retMsg = _hospitalRepository.Create(doc).Result;

                    if (retMsg == 1)
                    {
                        return Json("Record Saved Successfully");
                    }
                    else if (retMsg == 2)
                    {
                        return Json("Record Updated Successfully");
                    }
                    else if (retMsg == 3)
                    {
                        return Json("Record Deleted Successfully");
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
       
        public async Task<IActionResult> ViewHospital()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.Result = await _hospitalRepository.GetAll(new HospitalEntity());
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }
        [HttpPost]
        public async Task<IActionResult> ViewHospital(HospitalEntity client)
        {
            ViewBag.Result = await _hospitalRepository.GetAll(client);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int HospitalID)
        {
            try
            {
                int Result = _hospitalRepository.Delete(HospitalID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        [HttpGet]
        public IActionResult GetByHospitalID(int HospitalID)
        {
            var client = _hospitalRepository.GetOne(Convert.ToInt32(HospitalID)).Result;
            return Ok(JsonConvert.SerializeObject(client));
        }



    }
}

