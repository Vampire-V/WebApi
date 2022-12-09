using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Middleware;
using WebApi.Middleware.Exceptions;
using WebApi.Models.ProductionOrder;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionOrderController : ControllerBase
    {
        private IProductionOrderService _ProductionOrderService;
        public ProductionOrderController(IProductionOrderService ProductionOrderService)
        {
            _ProductionOrderService = ProductionOrderService;
        }

        // ..FG Order
        [HttpGet("[action]")]
        public async Task<ActionResult<List<FGProductionOrder>>> FinishedGoods([FromQuery] ProductionOrderParameter Parameter)
        {
            try
            {
                var FGProductionOrder = await _ProductionOrderService.GetFGProductionOrderFilterAsync(Parameter);
                return Ok(FGProductionOrder);

            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> FinishedGoodsForExcel([FromQuery] ProductionOrderParameter Parameter)
        {
            try
            {
                var content = await _ProductionOrderService.GetFGProductionOrderForExcel(Parameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        // ..SFG Order
        [HttpGet("[action]")]
        public async Task<ActionResult<List<SFGProductionOrder>>> SemiFinishedGoods([FromQuery] ProductionOrderParameter Parameter)
        {
            try
            {
                var SFGProductionOrder = await _ProductionOrderService.GetSFGProductionOrderFilterAsync(Parameter);
                return Ok(SFGProductionOrder);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SemiFinishedGoodsForExcel([FromQuery] ProductionOrderParameter Parameter)
        {
            try
            {
                var content = await _ProductionOrderService.GetSFGProductionOrderForExcel(Parameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }



        // ..FG Offline
        [HttpGet("[action]")]
        public async Task<ActionResult<List<FGOffline>>> FinishedGoodsOffline([FromQuery] OfflineParameter Parameter)
        {
            try
            {
                var FGOffline = await _ProductionOrderService.GetFGOfflineFilterAsync(Parameter);
                return Ok(FGOffline);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FinishedGoodsOfflineForExcel([FromQuery] OfflineParameter Parameter)
        {
            try
            {
                var content = await _ProductionOrderService.GetFGOfflineForExcel(Parameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        // ..SFG Offline
        [HttpGet("[action]")]
        public async Task<ActionResult<List<SFGOffline>>> SemiFinishedGoodsOffline([FromQuery] OfflineParameter Parameter)
        {
            try
            {
                var SFGOffline = await _ProductionOrderService.GetSFGOfflineFilterAsync(Parameter);
                return Ok(SFGOffline);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SemiFinishedGoodsOfflineForExcel([FromQuery] OfflineParameter Parameter)
        {
            try
            {
                var content = await _ProductionOrderService.GetSFGOfflineForExcel(Parameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        // ..Offline Summarize
        [HttpGet("[action]")]
        public async Task<ActionResult<List<OfflineSummarize>>> OfflineSummarize([FromQuery] string OrderNo)
        {
            try
            {
                var OfflineSummarize = await _ProductionOrderService.GetOfflineSummarizeFilterAsync(OrderNo);
                return Ok(OfflineSummarize);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> OfflineSummarizeForExcel([FromQuery] string OrderNo)
        {
            try
            {
                var content = await _ProductionOrderService.GetOfflineSummarizeForExcel(OrderNo);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        // ..PO Information
        [AllowAnonymous]
        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PoInformation>>> PoInformation([FromQuery] PoInformationParameter Parameter)
        {
            try
            {
                var PoInformation = await _ProductionOrderService.GetPoInformationFilterAsync(Parameter);
                return Ok(PoInformation);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PoInformationForExcel([FromQuery] PoInformationParameter Parameter)
        {
            try
            {
                var content = await _ProductionOrderService.GetPoInformationForExcel(Parameter);
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export{datetime}.xlsx");
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}