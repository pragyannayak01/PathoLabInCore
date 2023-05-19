using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.TestType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathoLab.Domain.TestType;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PathoLab.Web.Controllers
{
    public class TestTypeController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ITestType _TestType;
        public IConfiguration Configuration { get; }
        public TestTypeController(IHostingEnvironment hostingEnvironment, ITestType TestType, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _TestType = TestType;
        }
        public IActionResult AddTestType()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddTestType(TestTypeMaster tst)
        {
            try
            {
                if (tst.TestType == null)
                {
                    return Json("TestType  Shouldn't Be Blank");

                }
               else if ((!Regex.IsMatch(tst.TestType, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)*$", RegexOptions.IgnoreCase)))
                {

                    return Json(" Please Enter Only Alphabets  ");
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
        public async Task<IActionResult> ViewTestType()
        {
            ViewBag.Result = await _TestType.GetAll(new TestTypeMaster());
            return View();
        }
        [HttpPost]
        public IActionResult ViewTestType(TestTypeMaster tst)
        {
            ViewBag.Result = _TestType.GetAll(tst);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int TestTypeID)
        {
            try
            {
                int Result = _TestType.Delete(TestTypeID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        [HttpGet]
        public IActionResult GetByTestTypeID(int TestTypeID)
        {
            var client = _TestType.GetOne(Convert.ToInt32(TestTypeID)).Result;
            return Ok(JsonConvert.SerializeObject(client));
        }


    }
}
