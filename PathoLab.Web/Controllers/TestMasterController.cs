
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.TestMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.Domain.TestMaster;
using Newtonsoft.Json;
using PathoLab.Domain.TestType;
namespace PathoLab.Web.Controllers
{
    public class TestMasterController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ITestRepository _TestType;
        public IConfiguration Configuration { get; }
        public TestMasterController(IHostingEnvironment hostingEnvironment, ITestRepository TestType, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _TestType = TestType;
        }
        public async Task< IActionResult> AddTest()
        {
            List<TestTypeMaster> pc = new List<TestTypeMaster>();
            pc = await _TestType.Dropdown();
            pc.Insert(0, new TestTypeMaster { TestTypeID = 0, TestType = "---Select---" });
            ViewBag.message = pc;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddTest(Test tst)
        {
            try
            {
                if (tst.TestType == "Select" || tst.TestName == null)
                {
                    return Json("Please Fill All The Field");
                }
                else
                {
                    int retMsg = _TestType.Create(tst).Result;

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
        public async Task<IActionResult> ViewTest()
        {
            ViewBag.Result = await _TestType.GetAll(new Test());
            return View();
        }
        [HttpPost]
        public IActionResult ViewTest(Test tst)
        {
            ViewBag.Result = _TestType.GetAll(tst);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int TestID)
        {
            try
            {
                int Result = _TestType.Delete(TestID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        [HttpGet]
        public IActionResult GetByTestTypeID(int TestID)
        {
            var client = _TestType.GetOne(Convert.ToInt32(TestID)).Result;
            return Ok(JsonConvert.SerializeObject(client));
        }
        
    }
}
