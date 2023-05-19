using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathoLab.Domain.RoleMaster;
using PathoLab.IRepository.RoleMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRole log;

        public RoleController(IRole _log)
        {
            log = _log;
        }
        public IActionResult Role_Insert()
        {
            return View();
        }
        public async Task<IActionResult> GetAllRole()
        {
            ViewBag.Result = await log.GetAllRole();
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Add(Role entity)
        {
            try
            {
                if(entity.RoleName==null)
                {
                    return Json("Please Enter Role_Name!");
                }
                  if ((!Regex.IsMatch(entity.RoleName, @"^[a-zA-Z. ]+$")))
                {
                    return Json("Role_Name Is Invalid!");
                }
                else
                {
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
                throw ex;
            }
        }
        public async Task<int> Delete(int id)
        {
            int x = await log.delete(id);
            return x;
        }

        [HttpGet]
        public IActionResult UserGetById(int id)
        {
            var Doctors = log.RoleGetbyid(Convert.ToInt32(id)).Result;

            return Ok(JsonConvert.SerializeObject(Doctors));
        }
    }
}
