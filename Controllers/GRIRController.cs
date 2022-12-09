using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;
using WebApi.Models.GRIR;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GRIRController : ControllerBase
    {
        private IPurchasingDocumentService _PurchasingDocumentService;
        public GRIRController(IPurchasingDocumentService purchasingDocumentService)
        {
            _PurchasingDocumentService = purchasingDocumentService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<GrIrDetailView>>> GetGrIrDetailView([FromQuery] GrIrReportParameter GrIrReportParameter)
        {
            try
            {

                var GrIrDetailViews = await _PurchasingDocumentService.GetGrIrDetailViewFilterAsync(GrIrReportParameter);
                return Ok(GrIrDetailViews);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<GrIrPlantView>>> GetGrIrPlantView([FromQuery] GrIrReportParameter GrIrReportParameter)
        {
            try
            {
                var GrIrPlantViews = await _PurchasingDocumentService.GetGrIrPlantViewFilterAsync(GrIrReportParameter);
                return Ok(GrIrPlantViews);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<GrIrSummaryView>>> GetGrIrSummaryView([FromQuery] GrIrReportParameter GrIrReportParameter)
        {
            try
            {
                var GrIrSummaryViews = await _PurchasingDocumentService.GetGrIrSummaryViewFilterAsync(GrIrReportParameter);
                return Ok(GrIrSummaryViews);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("GetGrIrReport-export-to-excel")]
        public async Task<IActionResult> GetGrIrReportForExcel([FromQuery] GrIrReportParameter GrIrReportParameter)
        {
            try
            {
                var content = await _PurchasingDocumentService.GetGrIrReportForExcel(GrIrReportParameter);
                var datetime = DateTimeSystem.Utc(DateTime.UtcNow);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"GRIR_Report_{datetime.ToString("yyyyMMddHmmss")}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}