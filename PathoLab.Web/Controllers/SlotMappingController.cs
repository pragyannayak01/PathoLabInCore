using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.SlotMappig;
using PathoLab.IRepository.HospitalMaster;
using PathoLab.IRepository.SlotMappingMaster;
using PathoLab.IRepository.SlotMaster;
using PathoLab.IRepository.UserRegistration;
using PathoLab.Repository.SlotMappingMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class SlotMappingController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ISlotMappingRepository _slotmappingRepository;
        private readonly Slot_interface _slotRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IUser _userRepository;
        public IConfiguration Configuration { get; }
        public SlotMappingController(IHostingEnvironment hostingEnvironment, ISlotMappingRepository slotmappingRepository, IConfiguration configuration, IHospitalRepository hospitalRepository, Slot_interface slotRepository, IUser userRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _slotmappingRepository = slotmappingRepository;
            _hospitalRepository = hospitalRepository;
            _slotRepository = slotRepository;
            _userRepository = userRepository;
        }
        public IActionResult AddSlotMapping()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
                //ViewBag.DoctorName = _userRepository.DoctorDDL().Result;
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        public async Task<IActionResult> ViewSlotMapping()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
                ViewBag.DoctorName = _userRepository.DoctorDDL().Result;
                ViewBag.Result = await _slotmappingRepository.GetAll(new SlotMapping());
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ViewSlotMapping(SlotMapping slotMapping)
        {
            ViewBag.HospitalName = _hospitalRepository.HospitalDDL().Result;
            //ViewBag.DoctorName = _userRepository.DoctorDDL().Result;
            ViewBag.Result = await _slotmappingRepository.GetAll(slotMapping);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddSlotMapping(SlotMapping entity)
        {
            try
            {
                int retMsg = 0;
                List<string> Days = entity.Day.Split(',').ToList();
                if(entity.SMId!=0)
                {
                    //First Delete And Then Update The Data
                    int retdMsg = _slotmappingRepository.DeleteToUpdate(entity.SlotID, entity.DoctorId).Result;
                    foreach (var day in Days)
                    {
                        entity.DaysId = Convert.ToInt32(day);
                        retMsg = _slotmappingRepository.Create(entity).Result;
                    }
                }
                else
                {
                    //Insert Data 
                    foreach (var day in Days)
                    {
                        entity.DaysId = Convert.ToInt32(day);
                        retMsg = _slotmappingRepository.Create(entity).Result;
                    }
                }

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
        public IActionResult DeleteSlotMapping(int SMId)
        {
            try
            {
                int Result = _slotmappingRepository.Delete(SMId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult SlotMappingGetById(int SlotID,int DoctorId)
        {
            var slotMapping = _slotmappingRepository.GetAllShiftBySAndDId(Convert.ToInt32(SlotID), Convert.ToInt32(DoctorId)).Result;
            return Ok(JsonConvert.SerializeObject(slotMapping));
        }
        [HttpGet]
        public IActionResult DoctorNameByHId( int HospitalID)
        {
            var hospital = _slotmappingRepository.DoctorNameByHId(Convert.ToInt32(HospitalID)).Result;
            return Ok(JsonConvert.SerializeObject(hospital));
        }
    }
}

