using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathoLab.Domain.DepartmentMaster;
using PathoLab.Domain.DesignationMaster;
using PathoLab.Domain.Email;
using PathoLab.Domain.HospitalMaster;
using PathoLab.Domain.RoleMaster;
using PathoLab.Domain.UserRegistration;
using PathoLab.IRepository.DegisnationMaster;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.Email;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.IRepository.UserRegistration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class RegistrationUserController : Controller
    {
        private readonly IUser log;
        private readonly IDesignationRepository desig;
        private readonly IDepartmentRepository dept;
        private readonly IHospitalRepository hos;
        private readonly IMailService mailService;

        public RegistrationUserController(IUser _log, IDesignationRepository _desig, IDepartmentRepository _dept, IHospitalRepository _hos, IMailService mailServicee)
        {
            log = _log;
            desig = _desig;
            dept = _dept;
            hos = _hos;
            mailService = mailServicee;

        }

        public async Task<IActionResult> User_Registration()
        {
           

            List<DesignationName> pc1 = new List<DesignationName>();
            DesignationName ds=new DesignationName();//////for search
            pc1 = await desig.GetAll(ds);
            pc1.Insert(0, new DesignationName { DesignationId = 0, Designation = "Select" });
            ViewBag.DesignationName = pc1;

            List<DepartmentName> pc2 = new List<DepartmentName>();
            DepartmentName pc22 = new DepartmentName();

            pc2 = await dept.GetAll(pc22);
            pc2.Insert(0, new DepartmentName { DepartmentId = 0, Department = "Select" });
            ViewBag.DepartmentName = pc2;

            List<HospitalEntity> pc3 = new List<HospitalEntity>();
            HospitalEntity hs = new HospitalEntity();
            pc3 = await hos.GetAll(hs);
            pc3.Insert(0, new HospitalEntity { HospitalID = 0, HospitalName = "Select" });
            ViewBag.HospitalEntity = pc3;



            return View();
        }
        public async Task<IActionResult> GetAllUsers()
        {
            List<DesignationName> pc1 = new List<DesignationName>();
            DesignationName ds = new DesignationName();
            pc1 = await desig.GetAll(ds);
            pc1.Insert(0, new DesignationName { DesignationId = 0, Designation = "Select" });
            ViewBag.DesignationName = pc1;

            ViewBag.Result = await log.GetAllUser(new UserModel());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(UserModel us)
        {
            List<DesignationName> pc1 = new List<DesignationName>();
            DesignationName ds = new DesignationName();//////for search
            pc1 = await desig.GetAll(ds);
            pc1.Insert(0, new DesignationName { DesignationId = 0, Designation = "Select" });
            ViewBag.DesignationName = pc1;
            ViewBag.Result = await log.GetAllUser(us);
            return View();
        }

    
        [HttpPost]
        public async Task<JsonResult> Add(UserModel entity)
        {
            try
            {
               
                if (entity.UserName == null)
                {
                    return Json("Please Enter UserName");
                }
                else if (entity.Password == null)
                {
                    return Json("Please Enter Password");
                }
                else if (entity.Passwordconfirm == null)
                {
                    return Json("Please Enter ConfirmPassward");
                }
                else if (entity.Password != entity.Passwordconfirm)
                {
                    return Json("Passward and conferm passwars Should Match");
                }
                else if (entity.FullName == null)
                {
                    return Json("Please Enter FullName");
                }
                else if (entity.Email == null)
                {
                    return Json("Please Enter Email");
                }
                else if (entity.Mobile == null)
                {
                    return Json("Please Enter Mobile");
                }
                else if (entity.Gender == "Select")
                {
                    return Json("Please select Gender");
                }
               
                else if (entity.DesignationId == 0)
                {
                    return Json("Please select Designation Name");
                }
                else if (entity.DepartmentId == 0)
                {
                    return Json("Please select Department Name");

                }
                //else if (entity.HospitalID == 0)
                //{
                //    return Json("Please select Hospital Name");
                //}

                else if (entity.Address == null)
                {
                    return Json("Please Enter first Address");
                }
                else if (entity.Address1 == null)
                {
                    return Json("Please Enter second Address");
                }
                else if (entity.City == null)
                {
                    return Json("Please Enter City");
                }

                else if (!(entity.Address.Length <= 500))
                {
                    return Json("Maxmimum Length Of Address Field id 500");
                }
              


                else if (!Regex.IsMatch(entity.UserName, @"^[A-Za-z][A-Za-z0-9_]{7,29}$", RegexOptions.IgnoreCase))
                {
                    return Json("UserName Is Invalid");
                }
                else if ((!Regex.IsMatch(entity.FullName, @"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$")))
                {
                    return Json("FullName No. Is Invalid");
                }
                else if ((!Regex.IsMatch(entity.Email, @"^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$")))
                {
                    return Json("Email  Is Invalid");
                }
                else if ((!Regex.IsMatch(entity.Mobile, @"^([0-9]{10})$")))
                {
                    return Json("Mobile No. Is Invalid");
                }

                else
                {
                    string s2 = EncodePasswordToBase64(entity.Password);
                    entity.Password = s2;

                    int retMsg = await log.insert(entity);

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
            }
            catch (Exception ex)
            {
                return Json("Please Fill All The Field");
            }
        }

        [HttpPost]
        public async Task<JsonResult> SendSucessEmail(MailRequest email)
        {
            await mailService.SendEmailAsync(email);
            if (true)
            {
                return Json(1);
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




        [HttpPost]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                int Result = log.delete(id).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult UserGetById(int id)
        {
            var Doctors = log.GetbyidUser(Convert.ToInt32(id)).Result;
            string s1 = DecodeFrom64(Doctors.Password);

            Doctors.Password = s1;

            return Ok(JsonConvert.SerializeObject(Doctors));
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public IActionResult ExportToExcelRegistrationUser()
        {
            RegistrationUserExportToExcel(log.GetAllUser(new UserModel()).Result);
            return View();
        }
        private void RegistrationUserExportToExcel(List<UserModel> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("RegistrationUserReport");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "FullName";
                worksheet.Cell(currentRow, 2).Value = "UserName";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "Mobile";
                worksheet.Cell(currentRow, 5).Value = "Gender";
                worksheet.Cell(currentRow, 6).Value = "Designation";

                foreach (UserModel val in data)
                {
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = val.FullName;
                        worksheet.Cell(currentRow, 2).Value = val.UserName;
                        worksheet.Cell(currentRow, 3).Value = val.Email;
                        worksheet.Cell(currentRow, 4).Value = val.Mobile;
                        worksheet.Cell(currentRow, 5).Value = val.Gender;
                        worksheet.Cell(currentRow, 6).Value = val.Designation;
                    }
                }
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                Response.Clear();
                Response.Headers.Add("content-disposition", "attachment;filename=RegistrationUserReport.xls");
                Response.ContentType = "application/xls";
                Response.Body.WriteAsync(content);
                Response.Body.Flush();
            }
        }
    }
}