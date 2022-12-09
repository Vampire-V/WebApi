using Microsoft.AspNetCore.Mvc;
using WebApi.Models.AccountingIndirectVendor;
using WebApi.Services.Interface;
using WebApi.Extensions;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndirectVendorController : ControllerBase
    {
        private IAccountingIndirectVendorService _indirectVendorService;
        public IndirectVendorController(IAccountingIndirectVendorService indirectVendorService)
        {
            _indirectVendorService = indirectVendorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IndirectVendorView>>> GetIndirectVendors([FromQuery] IndirectVendorParameter indirectVendorParameter)
        {
            try
            {
                var indirectVendor = await _indirectVendorService.GetIndirectVendors(indirectVendorParameter);
                return Ok(indirectVendor);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("{vendorCode}")]
        public async Task<ActionResult<IndirectVendorView>> GetIndirectVendor(string vendorCode)
        {
            try
            {
                var vendor = await _indirectVendorService.GetIndirectVendor(vendorCode.Trim());
                return Ok(vendor);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<IndirectVendorView>>> Create(IndirectVendorCreate indirectVendorCreate)
        {
            try
            {
                await _indirectVendorService.CreateIndirectVendor(indirectVendorCreate);
                return CreatedAtRoute("VendorById", new { vendorCode = indirectVendorCreate.VendorCode }, indirectVendorCreate);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(string vendorCode, IndirectVendorUpdate indirectVendorUpdate)
        {
            try
            {
                await _indirectVendorService.UpdateIndirectVendor(vendorCode.Trim(), indirectVendorUpdate);
                return Ok(new { message = $"Vendor {vendorCode} modified." });

            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpDelete("{vendorCode}")]
        public async Task<ActionResult> Delete(string vendorCode)
        {
            try
            {
                await _indirectVendorService.Delete(vendorCode);
                return Ok(new { message = $"Vendor {vendorCode} deleted." });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("export-to-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] IndirectVendorParameter indirectVendorParameter)
        {
            try
            {
                var content = await _indirectVendorService.GetIndirectVendorForExcel(indirectVendorParameter);
                var datetime = DateTimeSystem.Utc(DateTime.UtcNow);
                return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"indirectVendors {datetime.ToString("yyyyMMddHmmss")}.xlsx"
                            );
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}