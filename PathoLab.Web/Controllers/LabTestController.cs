using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.HospitalMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.IRepository.LabTestMaster;
using Microsoft.AspNetCore.Http;
using PathoLab.Domain.LabTestMaster;
using Newtonsoft.Json;

namespace PathoLab.Web.Controllers
{
    public class LabTestController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILabtest _Repository;
        public IConfiguration Configuration { get; }
        public LabTestController(IHostingEnvironment hostingEnvironment, ILabtest Repository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _Repository = Repository;
        }
        public IActionResult AddLabTest()
        {
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
            ViewBag.Name = _Repository.GetAllDignosis().Result;
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}
        }


        [HttpPost]
        public async Task<JsonResult> AddLabTest(LabTest doc)
        {
            try
            {
                if (doc.LabTestName == null)
                {
                    return Json("Please Enter TestName");

                }

                else
                {

                    int retMsg = _Repository.insert(doc).Result;

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

        public async Task<IActionResult> ViewLabTest()
        {
            //var UserId = HttpContext.Session.GetInt32("UserId");
            //if (!string.IsNullOrEmpty(UserId.ToString()))
            //{
                ViewBag.Result = await _Repository.GetAllLabTest(new LabTest());
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logout", "Account");
            //}

        }
        [HttpPost]
        public async Task<IActionResult> ViewLabTest(LabTest client)
        {
            ViewBag.Result = await _Repository.GetAllLabTest(client);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(LabTest Id)
        {
            try
            {
                int Result = _Repository.delete(Id).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        [HttpGet]
        public IActionResult GetTestbyid(int LabTestId)
        {
            var client = _Repository.GetTestbyid(Convert.ToInt32(LabTestId)).Result;
            return Ok(JsonConvert.SerializeObject(client));
        }

    }


}
