using Microsoft.AspNetCore.Mvc;
using WebApi.Middleware.Exceptions;
using WebApi.Models;
using WebApi.Models.SASO;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SASOController : ControllerBase
    {

        private ISASOService _sasoService;
        private readonly ILogger<SASOController> logger;
        public SASOController(ISASOService sasoService, ILogger<SASOController> logger)
        {
            _sasoService = sasoService;
            this.logger = logger;
        }

        [HttpGet("{pbdate},{pcdate}")]
        public ActionResult<List<SASOView>> GetSASO(DateTime pbdate, DateTime pcdate)
        {
            try
            {
                return Ok( _sasoService.GetSASO(pbdate, pcdate));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Get SACompare : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }
    }
}