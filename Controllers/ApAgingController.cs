using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Accounting.Entities;
using WebApi.Services.Interface;
using RestSharp;
using WebApi.Models.BotRate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Middleware.Exceptions;
using WebApi.Extensions;
using WebApi.Models.Vendor;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApAgingController : ControllerBase
    {
        private IApAgingService _ApAgingService;
        private IVendorService _vendorService;
        public ApAgingController(IApAgingService ApAgingService, IVendorService vendorService)
        {
            _ApAgingService = ApAgingService;
            _vendorService = vendorService;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ApAgingDetail>>> ApAgingDetail([FromQuery] DateTime checkDate, DateTime checkRate)
        {
            try
            {
                var ApAgingDetail = await _ApAgingService.GetApAgingDetailFilterAsync(checkDate, checkRate);
                return Ok(ApAgingDetail);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ApAgingPackage>>> ApAgingPackage([FromQuery] DateTime checkDate, DateTime checkRate)
        {
            try
            {
                var ApAgingPackage = await _ApAgingService.GetApAgingPackageFilterAsync(checkDate, checkRate);
                return Ok(ApAgingPackage);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ApAgingPbc>>> ApAgingPbc([FromQuery] DateTime checkDate, DateTime checkRate, string checkPbc)
        {
            try
            {
                var ApAgingPbc = await _ApAgingService.GetApAgingPbcFilterAsync(checkDate, checkRate, checkPbc);
                return Ok(ApAgingPbc);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApAgingReportForExcel([FromQuery] DateTime checkDate, DateTime checkRate)
        {
            try
            {
                string datetime = DateTimeSystem.Utc(DateTime.UtcNow).ToString("yyyyMMddss");
                string filename = $"AP-Aging-Report-{datetime}.xlsx";
                byte[] content = await _ApAgingService.GetApAgingReportForExcel(checkDate, checkRate);
                // using var writer = new BinaryWriter(System.IO.File.OpenWrite("filePath"));
                // writer.Write(content);
                await System.IO.File.WriteAllBytesAsync(Path.Combine(@"wwwroot", "Accounting", filename), content);

                // string myIP = Dns.GetHostByName(hostName).AddressList[1].ToString();
                string url = $"{Request.Scheme}://{Request.Host.Value}/Accounting/{filename}";
                // return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AP Aging Report {datetime}.xlsx", true);
                return Ok(new { Link = url });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<BotRate>>> GetBotRateAsync([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                string url = $"https://apigw1.bot.or.th/bot/public/Stat-ExchangeRate/v2/DAILY_AVG_EXG_RATE/?start_period={startDate.ToString("yyyy-MM-dd")}&end_period={endDate.ToString("yyyy-MM-dd")}";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("X-IBM-Client-Id", "d973a2ac-5d9e-420e-8bcd-4d6f34463ad2");
                request.AddHeader("accept", "application/json");
                RestResponse response = await client.ExecuteAsync(request);
                var myJsonString = response.Content;
                var jsonObject = JObject.Parse(myJsonString!);
                var data = jsonObject["result"]!["data"]!["data_detail"]!;
                var botRate = JsonConvert.DeserializeObject<List<BotRate>>(data!.ToString());

                return Ok(botRate);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}