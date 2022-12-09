using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interface;
using WebApi.Models.COGI;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class COGIController : ControllerBase
    {
        private ICOGIService _cogiservice;
        private readonly ILogger<COGIController> logger;
        public COGIController(ICOGIService cogiservice, ILogger<COGIController> logger)
        {
            _cogiservice = cogiservice;
            this.logger = logger;
        }
        [HttpGet("{TimeStamp}")]
        public ActionResult<List<COGIView>> GetCOGI(DateTime TimeStamp)
        {
            try
            {
                return Ok(_cogiservice.GetCOGI(TimeStamp));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Get COGI : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("{TimeStamp}/export-to-excel")]
        public IActionResult GetCOGIForExcel(DateTime TimeStamp)
        {
            try
            {
                var content = _cogiservice.GetCOGIForExcel(TimeStamp);

                return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"GOGI_Report_{TimeStamp.ToString("yyyy-MM-dd")}.xlsx"
                        );
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Export To Excel : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }
    }
}