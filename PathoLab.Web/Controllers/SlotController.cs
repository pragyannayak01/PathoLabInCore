using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathoLab.Domain.HospitalMaster;
using PathoLab.Domain.SloteMaster;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.IRepository.SlotMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class SlotController : Controller
    {
        private readonly Slot_interface log;
        private readonly IHospitalRepository hos;

        public SlotController(Slot_interface _log, IHospitalRepository _hos)
        {
            log = _log;
            hos = _hos;

        }
        public async Task<IActionResult> Add_Slot()
        {
            List<HospitalEntity> pc3 = new List<HospitalEntity>();
            HospitalEntity hs = new HospitalEntity();
            pc3 = await hos.GetAll(new HospitalEntity());
            pc3.Insert(0, new HospitalEntity { HospitalID = 0, HospitalName = "Select" });
            ViewBag.HospitalEntity = pc3;

            return View();
        }
        public async Task<IActionResult> View_Slot()
        {
            List<HospitalEntity> pc3 = new List<HospitalEntity>();
            HospitalEntity hs = new HospitalEntity();
            pc3 = await hos.GetAll(new HospitalEntity());
            pc3.Insert(0, new HospitalEntity { HospitalID = 0, HospitalName = "Select" });
            ViewBag.HospitalEntity = pc3; 

            ViewBag.Result = await log.GetAllSlot(new SlotEntity());
            return View();
        }

        [HttpPost]    ///////for search
        public async Task<IActionResult> View_Slot(SlotEntity doc)
        {
            List<HospitalEntity> pc3 = new List<HospitalEntity>();
            HospitalEntity hs = new HospitalEntity();
            pc3 = await hos.GetAll(new HospitalEntity()); 
            pc3.Insert(0, new HospitalEntity { HospitalID = 0, HospitalName = "Select" }); 
            ViewBag.HospitalEntity = pc3;


            ViewBag.Result = await log.GetAllSlot(doc);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Add(SlotEntity entity)
        {
            try
            {
                if(entity.HospitalID==0)
                {
                    return Json("Please Select Hospital !");
                }
                else if (entity.SlotName == null)
                {
                    return Json("Please Enter Slot Name!");
                }
                else if (entity.Slot_TimeFrom == null)
                {
                    return Json("Please Enter Time From!");
                }
                else if (entity.Slot_TimeTo == null)
                {
                    return Json("Please Enter Time From!");
                }
                //else if (float.Parse(entity.Slot_TimeFrom) > 24)
                //{
                //    return Json("Hour must be less than 25!");
                //}
                //else if (entity.Slot_TimeTo == null)
                //{
                //    return Json("Please Enter Time To!");
                //}
                //else if (float.Parse(entity.Slot_TimeTo)> 24 )
                //{
                //    return Json("Hour must be less than 25!");
                //}
                //else if (float.Parse(entity.Slot_TimeTo) == float.Parse(entity.Slot_TimeFrom))
                //{
                //    return Json("To-Time and From-Time should not Same");
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
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult DeleteSlot(int id)
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
        public IActionResult SlotGetById(int id)
        {
            var Doctors = log.GetbyidSlot(Convert.ToInt32(id)).Result;


            return Ok(JsonConvert.SerializeObject(Doctors));
        }
        [HttpGet]
        public IActionResult GetSlotByHospitalId(int HospitalID)
        {
            var Slots = log.SlotHIdDDL(HospitalID).Result;
            return Ok(JsonConvert.SerializeObject(Slots));
        }
        [HttpGet]
        public IActionResult GetShiftBySlotId(int SlotId) 
        {
            var Slots = log.GetbyidSlot(SlotId).Result;
            return Ok(JsonConvert.SerializeObject(Slots));
        }
    }
}
