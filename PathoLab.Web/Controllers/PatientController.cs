using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathoLab.Domain.PatientMaster;
using PathoLab.IRepository.PatientMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly patientInterface log;

        public PatientController(patientInterface _log)
        {
            log = _log;
        }

        public IActionResult Patient_Registration()
        {
            return View();
        }
        public async Task<IActionResult> GetAllPatient()
        {
            ViewBag.Result = await log.GetAllPatient(new patient());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllPatient(patient p)
        {
            ViewBag.Result = await log.GetAllPatient(p);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Add(patient entity)
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

                //else if (entity.DesignationId == 0)
                //{
                //    return Json("Please select Designation Name");
                //}
                //else if (entity.DepartmentId == 0)
                //{
                //    return Json("Please select Department Name");

                //}
                ////else if (entity.HospitalID == 0)
                ////{
                ////    return Json("Please select Hospital Name");
                ////}

                //else if (entity.Address == null)
                //{
                //    return Json("Please Enter first Address");
                //}
                //else if (entity.Address1 == null)
                //{
                //    return Json("Please Enter second Address");
                //}
                //else if (entity.City == null)
                //{
                //    return Json("Please Enter City");
                //}

                //else if (!(entity.Address.Length <= 500))
                //{
                //    return Json("Maxmimum Length Of Address Field id 500");
                //}



                //else if (!Regex.IsMatch(entity.UserName, @"^[A-Za-z][A-Za-z0-9_]{7,29}$", RegexOptions.IgnoreCase))
                //{
                //    return Json("UserName Is Invalid");
                //}
                //else if ((!Regex.IsMatch(entity.FullName, @"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$")))
                //{
                //    return Json("FullName No. Is Invalid");
                //}
                //else if ((!Regex.IsMatch(entity.Email, @"^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$")))
                //{
                //    return Json("Email  Is Invalid");
                //}
                //else if ((!Regex.IsMatch(entity.Mobile, @"^([0-9]{10})$")))
                //{
                //    return Json("Mobile No. Is Invalid");
                //}

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
        public async Task<int> Delete(int id)
        {
          int x=await log.delete(id);
            return x;
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



        [HttpGet]
        public IActionResult UserGetById(int id)
        {
            var Doctors = log.Getbyidpatient(Convert.ToInt32(id)).Result;
            string s1 = DecodeFrom64(Doctors.Password);

            Doctors.Password = s1;
            return Ok(JsonConvert.SerializeObject(Doctors));
        }



    }
}

