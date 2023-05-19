using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DesignationMaster;
using PathoLab.IRepository.DegisnationMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationApiController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IDesignationRepository _designationRepository;
        public IConfiguration Configuration { get; }
        public DesignationApiController(IHostingEnvironment hostingEnvironment, IDesignationRepository designationRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _designationRepository = designationRepository;
        }
        [HttpGet("ViewDesignation")]
        public async Task<IActionResult> ViewDesignation()
        {
            var Result = await _designationRepository.GetAll(new DesignationName());
            return Ok(Result);
        }

        [HttpPost]
        public IActionResult ViewDesignation(DesignationName designationname)
        {
            var Result = _designationRepository.GetAll(designationname);
            return Ok(Result);
        }
        [HttpPost]
        public async Task<IActionResult> AddDesignation(DesignationName entity)
        {
            try
            {
                int retMsg = _designationRepository.Create(entity).Result;

                if (retMsg == 1)
                {
                    return Ok("Record Saved Successfully");
                }
                else if (retMsg == 2)
                {
                    return Ok("Record Updated Successfully");
                }
                else
                {
                    return Ok("Record Already Exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult DeleteDesignation(int DesignationID)
        {
            try
            {
                int Result = _designationRepository.Delete(DesignationID).Result;
                return Ok(Result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        [HttpGet]
        public IActionResult DesignationGetById(int DesignationId)
        {
            var Designations = _designationRepository.GetOne(Convert.ToInt32(DesignationId)).Result;
            return Ok(JsonConvert.SerializeObject(Designations));
        }
    }
}
