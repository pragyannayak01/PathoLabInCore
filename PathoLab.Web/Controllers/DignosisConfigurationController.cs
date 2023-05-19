using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.Domain.DignosisConfigurationMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.IRepository.DignosisConfigurationMaster;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using PathoLab.IRepository.DignosisMaster;
using PathoLab.IRepository.LabTestMaster;

namespace PathoLab.Web.Controllers
{
    public class DignosisConfigurationController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDignosisConfigurationRepository _dignosisConfiguration;
        private IDignosisRepository _dignosisRepository;
        private ILabtest _labtest;

        public IConfiguration Configuration { get; }
        public DignosisConfigurationController(IHostingEnvironment hostingEnvironment,IConfiguration configuration, IDignosisConfigurationRepository dignosisConfiguration, IDignosisRepository dignosisRepository, ILabtest labtest)
        {
            _hostingEnvironment = hostingEnvironment;
            _dignosisConfiguration = dignosisConfiguration;
            _dignosisRepository = dignosisRepository;
            _labtest = labtest;
            Configuration = configuration;
        }
        public IActionResult AddDignosisConfiguration()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.Name = _dignosisConfiguration.GetAllDignosis().Result;
                ViewBag.LabTestName = _dignosisConfiguration.GetAllLabTest().Result;
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }
        public async Task<IActionResult> ViewDignosisConfiguration()
        {
            ViewBag.Result = await _dignosisConfiguration.GetAllDignosisConfiguration(new DignosisConfiguration());
            return View();
        }
        [HttpPost]
        public IActionResult CreateDignosisConfiguration([FromBody]List<DignosisConfiguration> entity)
        {
            try
            {
                if (entity[0].DignosisConfigId != 0)
                {
                    //First Delete And Then Update The Prescribe Medicine Data
                    int retmMsg = _dignosisConfiguration.DeleteToUpdateDignosisConfiguration(entity[0].DignosisID, entity[0].LabTestId).Result;
                    if (entity != null)
                    {
                        foreach (var dignosisconfiguration in entity)
                        {
                            int retMsgPM = _dignosisConfiguration.InsertUpdateDignosisConfiguration(dignosisconfiguration).Result;
                        }
                    }
                    return Json("Dignosis Configuration Updated Successfully");
                }
                else
                {
                    int retMsg = 0;
                    foreach (var dignosisconfiguration in entity)
                    {
                         retMsg = _dignosisConfiguration.InsertUpdateDignosisConfiguration(dignosisconfiguration).Result;//retMsg-Carry DignosisConfigId
                    }
                    if (retMsg != 0)
                    {
                        return Json("Dignosis Configuration Saved Successfully");
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

        public IActionResult GetDignosisConfigurationById(int DignosisID,int LabTestId)
        {

            var DignosisConfiguration = _dignosisConfiguration.GetDignosisConfigurationById(Convert.ToInt32(DignosisID),Convert.ToInt32(LabTestId)).Result;
            return Ok(JsonConvert.SerializeObject(DignosisConfiguration));
        }

        [HttpGet]
        public IActionResult GetDignosis()
        {
            var dignosis = _dignosisConfiguration.GetAllDignosis().Result;
            return Ok(JsonConvert.SerializeObject(dignosis));
        }
        [HttpGet]
        public IActionResult GetLabTest()
        {
            var dignosis = _dignosisConfiguration.GetAllLabTest().Result;
            return Ok(JsonConvert.SerializeObject(dignosis));
        }
        [HttpPost]
        public IActionResult DeleteDignosisConfiguration(int DignosisID, int LabTestId)
        {
            try
            {
                int Result = _dignosisConfiguration.DeleteDignosisConfiguration(DignosisID, LabTestId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        

    }
}
