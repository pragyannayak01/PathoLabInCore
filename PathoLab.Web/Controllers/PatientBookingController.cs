using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PathoLab.IRepository.PatientMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class PatientBookingController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IPatientBookingRepository _patientBooking;
        public IConfiguration Configuration { get; }
        public PatientBookingController(IHostingEnvironment hostingEnvironment, IPatientBookingRepository patientBooking, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _patientBooking = patientBooking;
        }
        [HttpGet]
        public async Task<IActionResult> PatientBookingSchedule()
        {
            int x = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.Result = await _patientBooking.PatientBookingDetails(x);
            return View();
        }

        
    }
}
