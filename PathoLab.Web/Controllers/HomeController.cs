using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PathoLab.Domain.PathoBilMasterl;
using PathoLab.Domain.PatientAppointmentMaster;
using PathoLab.IRepository.DignosisConfigurationMaster;
using PathoLab.IRepository.LabTestMaster;
using PathoLab.IRepository.PathoBillMaster;
using PathoLab.IRepository.PatientAppointmentMaster;
using PathoLab.IRepository.UserRegistration;
using PathoLab.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientAppointmentRepository _patientAppointmentRepository;
        private IPathoBillRepository _pathoBill;
        private IUser _user;
        private IDignosisConfigurationRepository _dignosisConfiguration;
        private ILabtest _labtest;


        public HomeController(ILogger<HomeController> logger, IPatientAppointmentRepository patientAppointmentRepository , IPathoBillRepository pathoBill, IUser user, IDignosisConfigurationRepository dignosisConfiguration, ILabtest labtest)
        {
            _logger = logger;
            _patientAppointmentRepository = patientAppointmentRepository;
            _pathoBill = pathoBill;
            _user = user;
            _dignosisConfiguration = dignosisConfiguration;
            _labtest = labtest;

        }

        public IActionResult Index() 
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            ViewBag.Result = _patientAppointmentRepository.GetAll(new PatientAppointment()).Result;
            ViewBag.PathoBill = _pathoBill.GetAllPathoBill(new PathoBill()).Result;
            return View();
        }
        
        public IActionResult DoctorDashboard()
        {
            ViewBag.Result = _patientAppointmentRepository.GetAll(new PatientAppointment()).Result;
            ViewBag.PathoBill = _pathoBill.GetAllPathoBill(new PathoBill()).Result;
            return View();
        }
        public IActionResult FrontOfficeDashboard()
        {
            ViewBag.Result = _patientAppointmentRepository.GetAll(new PatientAppointment()).Result;
            ViewBag.PathoBill = _pathoBill.GetAllPathoBill(new PathoBill()).Result;
            return View();
        }

        public IActionResult PatientDashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
