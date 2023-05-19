using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DepartmentMaster;
using PathoLab.IRepository.DepartmentMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IDepartmentRepository _departmentRepository;
        public IConfiguration Configuration { get; }
        public DepartmentController(IHostingEnvironment hostingEnvironment, IDepartmentRepository departmentRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _departmentRepository = departmentRepository;
        }
        public IActionResult AddDepartment()
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
        public async Task<IActionResult> ViewDepartment()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.Result = await _departmentRepository.GetAll(new DepartmentName());
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [HttpPost]
        public async Task<IActionResult >ViewDepartment(DepartmentName departmentname)
        {
            ViewBag.Result = await  _departmentRepository.GetAll(departmentname);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddDepartment(DepartmentName entity)
        {
            try
            {
                int retMsg = _departmentRepository.Create(entity).Result;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult DeleteDepartment(int DepartmentId)
        {
            try
            {
                int Result = _departmentRepository.Delete(DepartmentId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult DepartmentGetById(int DepartmentId)
        {
            var Departments = _departmentRepository.GetOne(Convert.ToInt32(DepartmentId)).Result;
            return Ok(JsonConvert.SerializeObject(Departments));
        }
    }
}

