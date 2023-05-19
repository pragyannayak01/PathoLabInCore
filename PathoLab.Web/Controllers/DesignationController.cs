using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PathoLab.Domain.DesignationMaster;
using PathoLab.IRepository.DegisnationMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Web.Controllers
{
    public class DesignationController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IDesignationRepository _designationRepository;
        public IConfiguration Configuration { get; }
        public DesignationController(IHostingEnvironment hostingEnvironment, IDesignationRepository designationRepository, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            _designationRepository = designationRepository;
        }
        public IActionResult AddDesignation()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }
        public async Task<IActionResult> ViewDesignation()
        {
            //without login cant't access any pages
            var UserId = HttpContext.Session.GetInt32("UserId");
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                ViewBag.Result = await _designationRepository.GetAll(new DesignationName());
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }

        }

        [HttpPost]
        public IActionResult ViewDesignation(DesignationName designationname)
        {
            ViewBag.Result = _designationRepository.GetAll(designationname);
            return View();
        }
        public IActionResult ExportToExcelDesignation()
        {
            DesignationExportToExcel(_designationRepository.GetAll(new DesignationName()).Result);
            return View();
        }
        private void DesignationExportToExcel(List<DesignationName> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("DesignationReport");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "DesignationId";
                worksheet.Cell(currentRow, 2).Value = "Designation";

                foreach (DesignationName val in data)
                {
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = val.DesignationId;
                        worksheet.Cell(currentRow, 2).Value = val.Designation;
                    }
                }
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                Response.Clear();
                Response.Headers.Add("content-disposition", "attachment;filename=DesignationReport.xls");
                Response.ContentType = "application/xls";
                Response.Body.WriteAsync(content);
                Response.Body.Flush();
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddDesignation(DesignationName entity)
        {
            try
            {
                int retMsg = _designationRepository.Create(entity).Result;

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
        public IActionResult DeleteDesignation(int DesignationID)
        {
            try
            {
                int Result = _designationRepository.Delete(DesignationID).Result;
                return Json(Result);
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

