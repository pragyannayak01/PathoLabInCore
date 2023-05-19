using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.PatientAppointmentMaster;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.IRepository.PatientAppointmentMaster;
using PathoLab.IRepository.SlotMappingMaster;
using PathoLab.IRepository.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class WebsiteController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISlotMappingRepository _slotMappingRepository;
        private readonly IUser _userRepository;
        private readonly IPatientAppointmentRepository _patientAppointmentRepository;
        public IConfiguration Configuration { get; }
        public WebsiteController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IHospitalRepository hospitalRepository,
           IDepartmentRepository departmentRepository, ISlotMappingRepository _slotMappingRepository, IUser userRepository, IPatientAppointmentRepository patientAppointmentRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _hospitalRepository = hospitalRepository;
            _userRepository = userRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Home()
        {
            
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            ViewBag.Department = _departmentRepository.DepartmentDDL().Result;
            ViewBag.DoctorName = _userRepository.DoctorDDL().Result;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddPatientAppointment(PatientAppointment entity)
        {
            try
            {
                int retMsg = _patientAppointmentRepository.Create(entity).Result;

                if (retMsg == 1)
                {
                    return Json(" Your Appointment Have Been Scheduled Successfully");
                }
                else if (retMsg == 2)
                {
                    return Json("Record Updated Successfully");
                }
                else if (retMsg == 3)
                {
                    return Json("Patient Limit Exceeded");
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
        public IActionResult ViewPatientAppointment(PatientAppointment patientAppointment)
        {
            ViewBag.Result = _patientAppointmentRepository.GetAll(new PatientAppointment());
            return View();
        }
        [HttpGet]
        public IActionResult GetDoctorByDepartmentId(int DepartmentId)
        {
            var slotMapping = _patientAppointmentRepository.GetDoctorByDepartmentId(Convert.ToInt32(DepartmentId)).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }


        [HttpGet]
        public IActionResult GetSlotByHostIdAndDoctIdAndDOA(int HospitalID, int DoctorId, DateTime DateOfAppointment)
        {
            var slotMapping = _patientAppointmentRepository.GetSlotByHostIdAndDoctIdAndDOA(Convert.ToInt32(HospitalID), Convert.ToInt32(DoctorId), DateOfAppointment).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }


        [HttpGet]
        public IActionResult GetAvlCaptBySIdNDOA(int SlotID, DateTime DateOfAppointment)
        {
            var slotMapping = _patientAppointmentRepository.GetAvlCaptBySIdNDOA(Convert.ToInt32(SlotID), DateOfAppointment).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }
    }
}
