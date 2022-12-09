using System.Net;
using IronOcr;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OCRController : ControllerBase
    {
        public OCRController()
        {
        }

        [HttpPost("[action]"), DisableRequestSizeLimit]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            try
            {
                var file = files.First();
                // var filestream = System.IO.File.OpenRead();
                // var folderName = Path.Combine("wwwroot", "images");
                // var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                // var test = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\images\\test.png");
                if (file.Length > 0)
                {
                    OcrResult response;
                    var Ocr = new IronTesseract();
                    // Ocr.Configuration.BlackListCharacters = "~`$#^*_}{][|\\";
                    // Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
                    // Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
                    // Ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;
                    Ocr.Language = OcrLanguage.English;
                    // Ocr.AddSecondaryLanguage(OcrLanguage.English);



                    using (var Input = new OcrInput())
                    {
                        Input.AddImage(file.OpenReadStream());
                        // Input.Deskew();
                        response = await Ocr.ReadAsync(Input);
                        // response.SaveAsTextFile(@"C:\Users\pipat.p\Desktop\Screenshot_6.txt");
                    }
                    return Ok(response.Text);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}