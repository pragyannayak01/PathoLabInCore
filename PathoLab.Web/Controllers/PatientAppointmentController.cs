using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class PatientAppointmentController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISlotMappingRepository _slotMappingRepository;
        private readonly IUser _userRepository;
        private readonly IPatientAppointmentRepository _patientAppointmentRepository;
        public IConfiguration Configuration { get; }
        public PatientAppointmentController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IHospitalRepository hospitalRepository, 
           IDepartmentRepository departmentRepository, ISlotMappingRepository _slotMappingRepository,IUser userRepository,IPatientAppointmentRepository patientAppointmentRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _hospitalRepository = hospitalRepository;
            _userRepository = userRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _departmentRepository = departmentRepository;
        }
        public IActionResult AddPatientAppointment()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
                ViewBag.Department = _departmentRepository.DepartmentDDL().Result;
                ViewBag.DoctorName = _userRepository.DoctorDDL().Result;
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        public async Task<IActionResult> ViewPatientAppointment()
        {
           
            //without login cant't access any pages
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.Result = await _patientAppointmentRepository.GetAll(new PatientAppointment());
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [HttpPost]
        public   IActionResult ViewPatientAppointment(PatientAppointment patientAppointment)
        {
            ViewBag.Result =  _patientAppointmentRepository.GetAll(new PatientAppointment());
            return View();
        }
        
        [HttpPost]
        public async Task<JsonResult> AddPatientAppointment(PatientAppointment entity)
        {
            try
            {
                entity.Password= EncodePasswordToBase64("Password");
                int retMsg = _patientAppointmentRepository.Create(entity).Result;

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


        public static string EncodePasswordToBase64(string password)
        {   
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetDoctorByDepartmentId( int DepartmentId)
        {
            var slotMapping = _patientAppointmentRepository.GetDoctorByDepartmentId(Convert.ToInt32(DepartmentId)).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }


        [HttpGet]
        public IActionResult GetSlotByHostIdAndDoctIdAndDOA(int HospitalID, int DoctorId, DateTime DateOfAppointment)
        {
            var slotMapping = _patientAppointmentRepository.GetSlotByHostIdAndDoctIdAndDOA(Convert.ToInt32(HospitalID), Convert.ToInt32(DoctorId),DateOfAppointment).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }

        [HttpGet]
        public IActionResult DoctorNameByHAndDId(int HospitalID, int DepartmentId)
        {
            var slotMapping = _patientAppointmentRepository.DoctorNameByHAndDId(Convert.ToInt32(HospitalID), Convert.ToInt32(DepartmentId)).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }


        [HttpGet]
        public IActionResult GetAvlCaptBySIdNDOA( int SlotID, DateTime DateOfAppointment)
        {
            var slotMapping = _patientAppointmentRepository.GetAvlCaptBySIdNDOA( Convert.ToInt32(SlotID), DateOfAppointment).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }
        [HttpPost]
        public IActionResult DeleteAppointment(int AppointmentId)
        {
            try
            {
                int Result = _patientAppointmentRepository.DeleteAppointment(AppointmentId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult PatientAppointmentGetById(int AppointmentId)
        {
            var Appointments = _patientAppointmentRepository.GetOne(Convert.ToInt32(AppointmentId)).Result;
            return Ok(JsonConvert.SerializeObject(Appointments));
        }
        public IActionResult ExportToExcelPatientAppointment()
        {
            PatientAppointmentExportToExcel(_patientAppointmentRepository.GetAll(new PatientAppointment()).Result);
            return View();
        }
        private void PatientAppointmentExportToExcel(List<PatientAppointment> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("PatientAppointmentReport");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "RegdNo";
                worksheet.Cell(currentRow, 2).Value = "DateOfAppointment";
                worksheet.Cell(currentRow, 3).Value = "HospitalName";
                worksheet.Cell(currentRow, 4).Value = "SlotName";
                worksheet.Cell(currentRow, 5).Value = "Slot_TimeFrom";
                worksheet.Cell(currentRow, 6).Value = "Slot_TimeTo";
                worksheet.Cell(currentRow, 7).Value = "PatientName";

                foreach (PatientAppointment val in data)
                {
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = val.RegdNo;
                        worksheet.Cell(currentRow, 2).Value = val.DateOfAppointment;
                        worksheet.Cell(currentRow, 3).Value = val.HospitalName;
                        worksheet.Cell(currentRow, 4).Value = val.SlotName;
                        worksheet.Cell(currentRow, 5).Value = val.Slot_TimeFrom;
                        worksheet.Cell(currentRow, 6).Value = val.Slot_TimeTo;
                        worksheet.Cell(currentRow, 7).Value = val.PatientName;
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
    }
}
