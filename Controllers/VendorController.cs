using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interface;
using WebApi.Models.Vendor;
using Microsoft.AspNetCore.Authorization;
using WebApi.Extensions;
using WebApi.Middleware;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private IVendorService _vendorService;
        private readonly ILogger<VendorController> _logger;
        public VendorController(IVendorService vendorService, ILogger<VendorController> logger)
        {
            _vendorService = vendorService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VendorView>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<VendorView>>> GetVendors([FromQuery] VendorParameter vendorParameter)
        {
            try
            {
                var vendors = await _vendorService.GetVendors(vendorParameter);
                _logger.LogInformation("Returned vendors from database.");
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get vendors : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("{vendorCode}", Name = "VendorById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VendorView))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorView>> GetVendor(string vendorCode)
        {
            try
            {
                var vendor = await _vendorService.GetVendor(vendorCode);
                return Ok(vendor);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Get VendorById : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VendorView))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorView>> Create(VendorCreate vendor)
        {
            try
            {
                await _vendorService.CreateVendor(vendor);
                return CreatedAtRoute("VendorById", new { vendorCode = vendor.VendorCode }, vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create Vendor : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut("{vendorCode}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(string vendorCode, VendorUpdate vendorUpdate)
        {
            try
            {
                await _vendorService.UpdateVendor(vendorCode, vendorUpdate);
                return Ok(new { message = $"Vendor {vendorCode} modified." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Vendor : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpDelete("{vendorCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(string vendorCode)
        {
            try
            {
                await _vendorService.Delete(vendorCode);
                return Ok(new { message = $"Vendor {vendorCode} deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Vendor : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("export-to-excel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExportToExcel([FromQuery] VendorParameter vendorParameter)
        {
            try
            {
                var content = await _vendorService.GetVendorForExcel(vendorParameter);
                var datetime = DateTimeSystem.Utc(DateTime.UtcNow);
                return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"vendors {datetime.ToString("yyyyMMddHmmss")}.xlsx", true
                            );
            }
            catch (Exception ex)
            {
                _logger.LogError($"ExportToExcel Vendor : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }
    }
}