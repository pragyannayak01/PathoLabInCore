using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.PermissionMaster;
using PathoLab.Domain.SubMenuMaster;
using PathoLab.IRepository.DegisnationMaster;
using PathoLab.IRepository.PermissionMaster;
using PathoLab.IRepository.SubMenuMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class PermissionController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISubMenuRepository _submenuRepository;
        private readonly IDesignationRepository _designationRepository;
        public IConfiguration Configuration { get; }
        public PermissionController(IHostingEnvironment hostingEnvironment, ISubMenuRepository submenuRepository, IPermissionRepository permissionRepository, IDesignationRepository designationRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _submenuRepository = submenuRepository;
            _permissionRepository = permissionRepository;
            _designationRepository = designationRepository;
        }
        public IActionResult AddPermission()
        {
            ViewBag.DesignationName = _designationRepository.DesignationDDL().Result;
            return View();
        }
        [HttpPost]
        public IActionResult CreatePermission([FromBody]List<Permission> entity)
        {
            try
            {
                if (entity != null && entity.Count != 0)
                {
                    //First Delete And Then Update The Permission Data
                    int retdMsg = _permissionRepository.PermissionUpdateToDelete(entity[0].DesignationId, entity[0].UserId).Result;
                    foreach (var item in entity)
                    {
                        int retMsgPD = _permissionRepository.PermissionInsert(item).Result;
                    }
                    return Json("Permission Updated Successfully");
                }
                else
                {
                    return Json("Please select any of these Permission!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult GetSelectedPermissions(int UserId, int DesignationId)
        {
            ViewBag.DesignationId = DesignationId;
            ViewBag.UserId = UserId;
            ViewBag.DesignationName = _designationRepository.DesignationDDL().Result;
            ViewBag.SubMenu = _permissionRepository.GetSelectedSubMenus(DesignationId,UserId).Result;
            return View("AddPermission");
        }
        public IActionResult GetSelectedMenuByDesig(int DesignationId, int UserId)
        {
            var MenuClasses= _permissionRepository.GetSelectedMenuByDesig(DesignationId, UserId).Result;
            var SubMenuClasses = _permissionRepository.GetSelectedSubMenuByDesig(DesignationId, UserId).Result;
            return Ok(new { MenuClasses= JsonConvert.SerializeObject(MenuClasses), SubMenuClasses = JsonConvert.SerializeObject(SubMenuClasses) }); 
        }
    }
}
