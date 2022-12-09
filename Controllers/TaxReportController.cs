using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Accounting.Entities;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;
using WebApi.Models.TaxReportBSEG;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxReportController : ControllerBase
    {
        private ITaxReportService _taxReportService;

        public TaxReportController(ITaxReportService taxReportService)
        {
            _taxReportService = taxReportService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaxReportView>>> GetTaxReport([FromQuery] TaxReportParameter taxReportParameter)
        {
            try
            {
                var taxReport = await _taxReportService.GetTaxReport(taxReportParameter);
                return Ok(taxReport);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("export-to-excel")]
        public async Task<IActionResult> GetTaxReportForExcel([FromQuery] TaxReportParameter taxReportParameter)
        {
            try
            {
                var content = await _taxReportService.GetTaxReportForExcel(taxReportParameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHmmss");
                return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"TaxReport{datetime}.xlsx"
                            );
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}