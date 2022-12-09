using Microsoft.AspNetCore.Mvc;
using WebApi.Models.FaceRecognition;
using WebApi.Services.Interface;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using WebApi.Extensions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IServer _server;
        private readonly IEmployeeService _employeeService;
        private string[] faceImageExtensions = { ".jpg", ".png" };
        public UploadFileController(IServer server, IEmployeeService employeeService)
        {
            _server = server;
            _employeeService = employeeService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmployeesFace([FromForm] FaceRecognitionUpload formFaceRecog)
        {
            long size = formFaceRecog.Files.Sum(f => f.Length);
            List<string> errors = new List<string>();
            formFaceRecog.Files.ForEach(action =>
            {
                var ext = Path.GetExtension(action.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !faceImageExtensions.Contains(ext))
                {
                    throw new BadRequestException($"{action.FileName} : The extension is invalid.");
                }
            });
            try
            {
                string pathTo = Path.Combine("Employee", "images", formFaceRecog.EmployeeNo);
                int index = 0;
                foreach (IFormFile file in formFaceRecog.Files)
                {
                    string path = ManageFiles.Save(pathTo, file);
                    string link = $"http://{Request.Host.Value}/{pathTo.Replace('\\', '/')}/{file.FileName}";
                    await _employeeService.CreateStaffImage(formFaceRecog.EmployeeNo, link, path, file.FileName, formFaceRecog.DetectFace[index]);
                    index++;
                }
                return Ok(formFaceRecog.EmployeeNo);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}