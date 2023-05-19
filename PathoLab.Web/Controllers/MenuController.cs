using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.MenuMaster;
using PathoLab.IRepository.MenuMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class MenuController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IMenuRepository _menuRepository;
        public IConfiguration Configuration { get; }
        public MenuController(IHostingEnvironment hostingEnvironment, IMenuRepository menuRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _menuRepository = menuRepository;
        }
        public IActionResult AddMenu()
        {
                return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddMenu(MenuClass entity)
        {
            try
            {
                int retMsg = _menuRepository.MenuInsertAndUpdate(entity).Result;

                if (retMsg == 1)
                {
                    return Json("Menu Saved Successfully");
                }
                else if (retMsg == 2)
                {
                    return Json("Menu Updated Successfully");
                }
                else
                {
                    return Json("Menu Already Exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult ViewMenu( )
        {
            ViewBag.Result =  _menuRepository.MenuSelectAll(new MenuClass()).Result;
            return View();
        }
        
        [HttpPost]
        public IActionResult DeleteMenu(int MenuId)
        {
            try
            {
                int Result = _menuRepository.MenuDelete(MenuId).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult MenuGetById(int MenuId)
        {
            var Menus = _menuRepository.MenuSelectOne(Convert.ToInt32(MenuId)).Result;
            return Ok(JsonConvert.SerializeObject(Menus));
        }
    }
}
