using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.Report;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.IRepository.PatientAppointmentMaster;
using PathoLab.IRepository.Report;
using PathoLab.IRepository.SlotMappingMaster;
using PathoLab.IRepository.UserRegistration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class ReportController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISlotMappingRepository _slotMappingRepository;
        private readonly IUser _userRepository;
        private readonly IPatientAppointmentRepository _patientAppointmentRepository;
        private readonly IReportRepository _reportRepository;

        public IConfiguration Configuration { get; }
        public ReportController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IHospitalRepository hospitalRepository,
           IDepartmentRepository departmentRepository, ISlotMappingRepository _slotMappingRepository, IUser userRepository, IPatientAppointmentRepository patientAppointmentRepository, IReportRepository reportRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _hospitalRepository = hospitalRepository;
            _userRepository = userRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _departmentRepository = departmentRepository;
            _reportRepository = reportRepository;
        }
        public IActionResult DailyDateWiseAppointment()
        {
            ViewBag.Result = _reportRepository.DailyDateWiseAppointment(new ReportEntity()).Result;
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            return View();
        }
        [HttpPost]
        public IActionResult DailyDateWiseAppointment(ReportEntity reportEntity)
        {
            ViewBag.Result = _reportRepository.DailyDateWiseAppointment(reportEntity).Result;
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            return View();
        }

        public IActionResult HospitalWiseAppointment(int HospitalID)
        {
            ReportEntity re = new ReportEntity();
            re.HospitalID = HospitalID;
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            ViewBag.Record = _reportRepository.HospitalWiseAppointment(re).Result;
            return View();
        }
        [HttpPost]
        public IActionResult HospitalWiseAppointment(ReportEntity reportEntity)
        {
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            ViewBag.Record = _reportRepository.HospitalWiseAppointment(reportEntity).Result;
            return View();
        }
        public IActionResult ExportToExcelReport()
        {
            ReportExportToExcel(_reportRepository.DailyDateWiseAppointment(new ReportEntity()).Result);
            return View();
        }
        private void ReportExportToExcel(List<ReportEntity> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "HospitalName";
                worksheet.Cell(currentRow, 2).Value = "AppointmentCount";

                foreach (ReportEntity val in data)
                {
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = val.HospitalName;
                        worksheet.Cell(currentRow, 2).Value = val.AppointmentCount;
                        
                    }
                }
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                Response.Clear();
                Response.Headers.Add("content-disposition", "attachment;filename=PatientAppointmentReport.xls");
                Response.ContentType = "application/xls";
                Response.Body.WriteAsync(content);
                Response.Body.Flush();
            }
        }

        public IActionResult PatientTestReport()
        {
            return View();
        }
    }
}