using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Employee;
using WebApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeView>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeeView>>> GetEmployees()
        {
            try
            {
                var items = await _employeeService.GetEmployees();
                if (items is null)
                {
                    return NotFound();
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Get Employees : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StaffScan>> PostScanInOutWorkAsync(StaffScan staffScan)
        {
            try
            {
                // Thread.Sleep(2000);
                await _employeeService.StaffScanFace(staffScan);
                // return StatusCode(((int)HttpStatusCode.InternalServerError), $"Internal server error: test.");
                return StatusCode(((int)HttpStatusCode.Created), staffScan);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Post ScanInOutWork : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Employee>>> GetStaffForScan()
        {
            try
            {
                var items = await _employeeService.GetStaffAndImage();
                return Ok(items);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Get StaffForScan : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Employee>>> Filter([FromQuery] EmployeeParameter parameter)
        {
            try
            {
                var items = await _employeeService.Filter(parameter);
                return Ok(items);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Get Employees : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete("[action]/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteImageScan(int id)
        {
            try
            {
                var items = await _employeeService.RemoveStaffImage(id);
                return items ? Ok(true) : NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Delete DeleteImageScan : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }
    }
}