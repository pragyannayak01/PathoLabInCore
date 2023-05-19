using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.Client;
using PathoLab.IRepository.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class ClientController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IClientRepository _clientRepository;
        public IConfiguration Configuration { get; }
        public ClientController(IHostingEnvironment hostingEnvironment, IClientRepository clientRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _clientRepository = clientRepository;
        }
        public IActionResult AddClient()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<JsonResult> AddClient(ClientMaster client)
        {
            try
            {
                if (client.Name == null || client.Address == null || client.City == "Select" || client.phoneno == null)
                {
                    return Json("Please Fill All The Field");
                }
                //if (client.Name == null)
                //{
                //    return Json("Please Enter Name");
                //}
                //else if (client.Address == null)
                //{
                //    return Json("Please Enter your Address");
                //}
                //else if (client.City == "Select")
                //{
                //    return Json("Please select your City");
                //}
                //else if (client.phoneno == null)
                //{
                //    return Json("Please Enter your Phone No");
                //}
                else  if (!Regex.IsMatch(client.phoneno, @"^([0]|\+91)?\d{10}", RegexOptions.IgnoreCase))
                {
                    return Json("Mobile No. Is Invalid");
                }
                else if ((!Regex.IsMatch(client.Name, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)*$", RegexOptions.IgnoreCase)))
                {

                    return Json("Name  Is  Invalid");
                }
                else
                {

                    int retMsg = _clientRepository.Create(client).Result;

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
                        return Json("Record Deleted Successfully");
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
     
        public async Task<IActionResult> ViewClient() 
        {
            ViewBag.Result = await _clientRepository.GetAll(new ClientMaster());
            return View();
        }
        [HttpPost]
        public IActionResult ViewClient(ClientMaster client)
        {
            ViewBag.Result = _clientRepository.GetAll(client);
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int ClintID)
        {
            try
            {
                int Result = _clientRepository.Delete(ClintID).Result;
                return Json(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        [HttpGet]
        public IActionResult GetByClintID(int ClintID)
        {
            var client = _clientRepository.GetOne(Convert.ToInt32(ClintID)).Result;
            return Ok(JsonConvert.SerializeObject(client));
        }







        
    }
}


    

