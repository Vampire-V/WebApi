using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.SaleRebate;
using WebApi.Services.Interface;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleRebateController : ControllerBase
    {
        private ISaleRebateService _SaleRebateService;
        public SaleRebateController(ISaleRebateService SaleRebateService)
        {
            _SaleRebateService = SaleRebateService;
        }

        [HttpGet("GetSaleRebateView")]
        public async Task<ActionResult<List<SaleRebateView>>> GetSaleRebateViewFilterAsync([FromQuery] SaleRebateParameter SaleRebateParameter)
        {
            try
            {
                var SaleRebateView = await _SaleRebateService.GetSaleRebateViewFilterAsync(SaleRebateParameter);
                return Ok(SaleRebateView);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("GetSaleRebateDetail")]
        public async Task<ActionResult<List<SaleRebateDetail>>> GetSaleRebateDetailFilterAsync([FromQuery] SaleRebateParameter SaleRebateParameter)
        {
            try
            {
                var SaleRebateDetail = await _SaleRebateService.GetSaleRebateDetailFilterAsync(SaleRebateParameter);
                return Ok(SaleRebateDetail);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("GetSaleRebate-export-to-excel")]
        public async Task<IActionResult> GetSaleRebateForExcel([FromQuery] SaleRebateParameter SaleRebateParameter)
        {
            try
            {

                var content = await _SaleRebateService.GetSaleRebateForExcel(SaleRebateParameter);
                var datetime = DateTimeSystem.Utc(DateTime.UtcNow);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Sale Rebate{datetime.ToString("yyyyMMddHmmss")}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("GetSaleRebateTemplate-export-to-excel")]
        public async Task<IActionResult> GetSaleRebateTemplateForExcel([FromQuery] SaleRebateParameter SaleRebateParameter)
        {
            try
            {
                var content = await _SaleRebateService.GetSaleRebateTemplateForExcel(SaleRebateParameter);
                var datetime = DateTimeSystem.Utc(DateTime.UtcNow);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Sale Rebate Template{datetime.ToString("yyyyMMddHmmss")}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}