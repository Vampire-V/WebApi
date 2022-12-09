using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interface;
using System.Xml.Serialization;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using WebApi.Middleware.Exceptions;
// using Microsoft.Net.Http.Headers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestBNPChequeController : ControllerBase
    {
        private IVendorService _vendorService;
        public TestBNPChequeController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> TestChequeBNP()
        {
            try
            {
                var result = await _vendorService.TestChequeBNP();
                XmlSerializer x = new XmlSerializer(result.GetType());
                // XmlSerializerNamespaces ns1 = new XmlSerializerNamespaces();

                // ns1.Add("urn:iso:std:iso:20022:tech:xsd:pain.001.001.03","");
                // ns1.Add("xsi","http://www.w3.org/2001/XMLSchema-instance");
                // ns1.Add("schemaLocation","urn:iso:std:iso:20022:tech:xsd:pain.001.001.03 file:///H:/GTB/Cash%20Management/Implementation/_Client%20list/_File%20Spec/XML/schema/pain.001.001.03.xsd");
                using (StreamWriter writer = new StreamWriter("bankbnp.xml"))
                {
                    x.Serialize(writer, result);
                }
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType("bankbnp.xml", out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync("bankbnp.xml");
                return File(bytes, contentType, Path.GetFileName("bankbnp.xml"));
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}