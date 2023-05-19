using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.SubMenuMaster;
using PathoLab.IRepository.MenuMaster;
using PathoLab.IRepository.SubMenuMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class SubMenuController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ISubMenuRepository _submenuRepository;
        private readonly IMenuRepository _menuRepository;
        public IConfiguration Configuration { get; }
        public SubMenuController(IHostingEnvironment hostingEnvironment, ISubMenuRepository submenuRepository, IMenuRepository menuRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _submenuRepository = submenuRepository;
            _menuRepository = menuRepository;
        }
        public IActionResult AddSubMenu()
        {
            ViewBag.Name = _submenuRepository.GetAllMenu().Result;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddSubMenu(SubMenuClass entity)
        {
            try
            {
                int retMsg = _submenuRepository.SubMenuInsertAndUpdate(entity).Result;

                if (retMsg == 1)
                {
                    return Json("SubMenu Saved Successfully");
                }
                else if (retMsg == 2)
                {
                    return Json("SubMenu Updated Successfully");
                }
                else
                {
                    return Json("SubMenu Already Exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult ViewSubMenu()
        {
            ViewBag.Result = _submenuRepository.SubMenuSelectAll(new SubMenuClass()).Result;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteSubMenu(int SubMenuId)
        {
            try
            {
                int Result = _submenuRepository.SubMenuDelete(SubMenuId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult SubMenuGetById(int SubMenuId)
        {
            var SubMenus = _submenuRepository.SubMenuSelectOne(Convert.ToInt32(SubMenuId)).Result;
            return Ok(JsonConvert.SerializeObject(SubMenus));
        }

    }
}
