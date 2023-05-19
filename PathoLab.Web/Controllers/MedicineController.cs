using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathoLab.Domain.MedicineMaster;
using PathoLab.IRepository.Medicine_Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class MedicineController : Controller
    {
       
        private readonly IMedicine log;

        public MedicineController(IMedicine _log)
        {
            log = _log;
        }

        public async Task<IActionResult> AddMedicine()
        {
          

            List<Medicine> pc1 = new List<Medicine>();
           // DesignationName ds = new DesignationName();//////for search
            pc1 = await log.UnitBind();
            pc1.Insert(0, new Medicine { UnitId = 0, UnitName = "Select" });
            ViewBag.UnitName = pc1;

            List<Medicine> pc2 = new List<Medicine>();
            pc2 = await log.BrandBind();
            pc2.Insert(0, new Medicine { BrandId = 0, BrandName = "Select" });
            ViewBag.BrandName = pc2;

            List<Medicine> pc3 = new List<Medicine>();
            pc3 = await log.HsnCodeBind();

            pc3.Insert(0, new Medicine { HsnId = 0, ddlHSnCode = "Select" });
            ViewBag.ddlHSnCodEName = pc3;

            List<Medicine> pc4 = new List<Medicine>();
            pc4 = await log.CatagoryBind();
            pc4.Insert(0, new Medicine { CatagoryId = 0, CatagoryName = "Select" });
            ViewBag.Catagory = pc4;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCatByCId(int HospitalID)
        {
            List<Medicine> pc4 = new List<Medicine>();
            pc4 = await log.CatagoryBind();
            pc4.Insert(0, new Medicine { CatagoryId = 0, CatagoryName = "Select" });
            ViewBag.Catagory = pc4;

            var Slots = log.SubCatagoryBind(HospitalID).Result;
            return Ok(JsonConvert.SerializeObject(Slots));
        }

        public async Task<IActionResult> ViewMedicine()
        {
            List<Medicine> pc4 = new List<Medicine>();
            pc4 = await log.CatagoryBind();
            pc4.Insert(0, new Medicine { CatagoryId = 0, CatagoryName = "Select" });
            ViewBag.Catagory = pc4;

            ViewBag.Result = await log.GetAllMedicine(new Medicine());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ViewMedicine(Medicine us)
        {
            List<Medicine> pc4 = new List<Medicine>();
            pc4 = await log.CatagoryBind();
            pc4.Insert(0, new Medicine { CatagoryId = 0, CatagoryName = "Select" });
            ViewBag.Catagory = pc4;

            ViewBag.Result = await log.GetAllMedicine(us);
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Add(Medicine entity)
        {
            try
            {

                //if (entity.UserName == null)
                //{
                //    return Json("Please Enter UserName");
                //}
                //else if (entity.Password == null)
                //{
                //    return Json("Please Enter Password");
                //}
                //else if (entity.Passwordconfirm == null)
                //{
                //    return Json("Please Enter ConfirmPassward");
                //}
                //else if (entity.Password != entity.Passwordconfirm)
                //{
                //    return Json("Passward and conferm passwars Should Match");
                //}
                //else if (entity.FullName == null)
                //{
                //    return Json("Please Enter FullName");
                //}
                //else if (entity.Email == null)
                //{
                //    return Json("Please Enter Email");
                //}
                //else if (entity.Mobile == null)
                //{
                //    return Json("Please Enter Mobile");
                //}
                //else if (entity.Gender == "Select")
                //{
                //    return Json("Please select Gender");
                //}

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
            catch (Exception ex)
            {
                return Json("Please Fill All The Field");
            }
        }

      




        [HttpPost]
        public IActionResult Delete(int id)
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
        public IActionResult MedicineGetById(int id)
        {
            var Doctors = log.GetMedicineById(Convert.ToInt32(id)).Result;
        
         

            return Ok(JsonConvert.SerializeObject(Doctors));
        }

       
    }
}