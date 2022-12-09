using System.Globalization;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WebApi.Data.Monitoring.Entities;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;
using WebApi.Models;
using WebApi.Models.COGI;
using WebApi.Models.GRGI;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GRGIController : ControllerBase
    {
        private IGRGIService _GRGIService;
        private IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<GRGIController> logger;
        public GRGIController(IGRGIService GRGIService, ILogger<GRGIController> logger, IWebHostEnvironment hostEnvironment)
        {
            _GRGIService = GRGIService;
            this.logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<GrGiPlan>>> GetGRGI()
        {
            try
            {

                return Ok(await _GRGIService.GetGRGIPlan());
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost("import/excel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try
            {
                DateTime datenow = DateTimeSystem.Utc(DateTime.UtcNow);
                string[] permittedExtensions = { ".xlsx" };
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    throw new ArgumentException($"The file type must be xlsx.");
                }

                var list = Excel<GrGiPlanImport>.ImportExcel(file.OpenReadStream(), "Sheet upload");
                var entity = list.Select(g => new GrGiPlan
                {
                    Plant = g.Plant.Trim(),
                    Mrp = g.Mrp.Trim(),
                    PlanDate = g.PlanDate.Trim(),
                    PlanType = g.PlanType.Trim(),
                    MonthTarget = g.MonthTarget,
                    DayTarget = g.DayTarget
                }).ToList();
                if (entity.Count == 0)
                {
                    throw new ArgumentException($"Cannot edit or add information before the current date.");
                }
                await _GRGIService.AddOrUpdateRange(entity);

                return Ok(new { message = "Upload plan success." });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("download/template")]
        public IActionResult DownloadTemplate()
        {
            try
            {
                string fileTemplate = Path.Combine(_hostEnvironment.WebRootPath, "Template/Production and Dispatch/") + "Template ProductionDispatch.xlsx";
                byte[] bytes = System.IO.File.ReadAllBytes(fileTemplate);
                return File(bytes, "application/octet-stream", "Template ProductionDispatch.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}