using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DoctorMaster;
using PathoLab.IRepository.DegisnationMaster;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.DoctorMaster;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class DoctorMasterController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDesignationRepository _designationRepository;

        public IConfiguration Configuration { get; }
        public DoctorMasterController(IHostingEnvironment hostingEnvironment, IDoctorRepository doctorRepository, IDepartmentRepository departmentRepository, IDesignationRepository designationRepository , IConfiguration configuration) 
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
            _designationRepository = designationRepository;
        }
        public IActionResult AddDoctor()
        {
            ViewBag.Desg = _designationRepository.DesignationDDL().Result;
            ViewBag.Dept = _departmentRepository.DepartmentDDL().Result;
            return View();
        }
        public async Task<IActionResult> ViewDoctor() 
        {
            ViewBag.Result = await _doctorRepository.GetAll(new Doctor());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewDoctor(Doctor doctor)
        {
            ViewBag.Result = await _doctorRepository.GetAll(doctor);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddDoctor(Doctor entity)
        {
            try
            {
                //if (entity.Prefix == "Select" || entity.DoctorName == null || entity.Designation == "Select" || entity.Department == "Select" || entity.HospitalName == null || entity.RegnNo == null || entity.Mobile == null || entity.Fees == null)
                //{
                //    return Json("Please Fill All The Field");
                //}
                //if (entity.Prefix == "Select")
                //{
                //    return Json("Please Choose Prefix");
                //}
                //else if (entity.DoctorName == null)
                //{
                //    return Json("Please Enter DoctorName");
                //}
                //else if (entity.Designation == "Select")
                //{
                //    return Json("Please Choose Designation");
                //}
                //else if (entity.Department == "Select")
                //{
                //    return Json("Please Choose Department");
                //}
                //else if (entity.HospitalName == null)
                //{
                //    return Json("Please Enter HospitalName");
                //}
                //else if (entity.RegnNo == null)
                //{
                //    return Json("Please Enter RegnNo");
                //}
                //else if (entity.Mobile == null)
                //{
                //    return Json("Please Enter Mobile");
                //}
                //else if (entity.Fees == null)
                //{
                //    return Json("Please Enter Fees");
                //}


                //regex for email-\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z
                if (!Regex.IsMatch(entity.Mobile, @"^([0]|\+91)?\d{10}", RegexOptions.IgnoreCase))
                {
                    return Json("Mobile No. Is Invalid");
                }
                else if ((!Regex.IsMatch(entity.DoctorName, @"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$")))
                {
                    return Json("Name  Is  Invalid");
                }
                else
                {
                    int retMsg = _doctorRepository.Create(entity).Result;

                    return Json(retMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult DeleteDoctor(int DoctorID)
        {
            try
            {
                int Result = _doctorRepository.Delete(DoctorID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult DoctorGetById(int DoctorId)
        {
            var Doctors = _doctorRepository.GetOne(Convert.ToInt32(DoctorId)).Result;
            return Ok(JsonConvert.SerializeObject(Doctors));
        }
    }
}
