using Microsoft.AspNetCore.Mvc;
using SapNwRfc;
using System.IO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SAPController : ControllerBase
    {
        private readonly ILogger<SAPController> logger;
        public SAPController(ILogger<SAPController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult TestConnectSAP()
        {
            return Ok("ไม่มี dll Rfc ที่รองรับ");
        }
    }
}