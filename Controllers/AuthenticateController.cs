using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Middleware.Exceptions;
using Microsoft.IdentityModel.Tokens;
using WebApi.Config;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Extensions;
using WebApi.Middleware;
using WebApi.Models;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UnauthorizedAccessException = WebApi.Middleware.Exceptions.UnauthorizedAccessException;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using WatchDog;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailServise;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILineService _lineService;
        private readonly IOptions<JwtSettings> _jwtSetting;
        private readonly IOptions<LineDev> _lineDev;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKeyName = "_State";

        public AuthenticateController(IUserService userService, ILogger<AuthenticateController> logger, IMailService mailServise, IOptions<JwtSettings> jwtSetting, IRefreshTokenService refreshTokenService, IOptions<LineDev> lineDev, IHttpContextAccessor httpContextAccessor, ILineService lineService)
        {
            _userService = userService;
            _logger = logger;
            _mailServise = mailServise;
            _jwtSetting = jwtSetting;
            _refreshTokenService = refreshTokenService;
            _lineDev = lineDev;
            _httpContextAccessor = httpContextAccessor;
            _lineService = lineService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MakeVerifyCode([FromBody] EmailRequest emailRequest)
        {
            try
            {
                var randomGenerator = new Random();
                string code = randomGenerator.Next(0000, 9999).ToString();
                var user = await _userService.SetVerifyCodeByEmail(emailRequest.Email, code);
                // send mail VerifyCode
                await _mailServise.MailVerifyCode(user.Email, code);
                // set verify code and exp verify code to user
                // return Ok(new { message = "Check VerifyCode from email." });
                return Ok();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthRsponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthRsponse>> Signin([FromBody] AuthRequest authRequest)
        {
            try
            {
                var user = await _userService.UserByEmail(authRequest.Email);
                if (user is null)
                    throw new BadRequestException($"{authRequest.Email} not found.");

                if (authRequest.VerifyCode != user.VerifyCode) // เช็ค VerifyCode หมดอายุด้วย
                    throw new BadRequestException($"VerifyCode {authRequest.VerifyCode} incorrect.");

                string token = GenToken(user);
                RefreshToken refreshToken = await _refreshTokenService.GenerateRefreshToken(user.Id); //GenerateRefreshToken();
                // SetRefreshToken(refreshToken);
                return Ok(new AuthRsponse { Token = token, RefreshToken = refreshToken.Token });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthRsponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedObjectResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthRsponse>> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            try
            {
                var refreshToken = await _refreshTokenService.GetToken(model.Token);
                if (model.Token is null || refreshToken is null)
                    throw new UnauthorizedAccessException("Invalid refresh-token.");

                bool result = await _refreshTokenService.IsExpire(refreshToken.Token);
                if (result)
                    throw new UnauthorizedAccessException("Refresh-token expires.");

                var user = await _userService.GetUser(refreshToken.UserId);
                string token = GenToken(user);
                RefreshToken newRefreshToken = await _refreshTokenService.GenerateRefreshToken(user.Id);
                // SetRefreshToken(newRefreshToken);
                return Ok(new AuthRsponse { Token = token, RefreshToken = newRefreshToken.Token });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthRsponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedObjectResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public IActionResult Me()
        {
            try
            {
                if (HttpContext is null)
                    throw new UnauthorizedAccessException("token expires.");
                string username = HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
                return Ok(new { username });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                User user = await _userService.UserByEmail(email);
                await _refreshTokenService.RemoveByUser(user.Id);
                return Accepted();
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous] // Line section
        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        // [AutoValidateAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LinkAccessLine()
        {
            try
            {
                string RequestUrl = _httpContextAccessor?.HttpContext?.Request.Headers["Origin"].ToString() ?? "";
                string link = await _lineService.GenLink(RequestUrl);
                return Ok(new { Link = link });
            }
            catch (Exception ex)
            {
                
                throw new BadRequestException(ex.Message);
            }
        }

        // callback line for api system
        [AllowAnonymous]
        [HttpGet("[action]")] // for other client
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthRsponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthRsponse>> LineCallBack([FromQuery] LineCallBackParameter param)
        {
            try
            {
                LineAccessTokenResponse result = await _lineService.AccessToken(param);
                LineUserProfile profile = await _lineService.Profile(result.AccessToken ?? "");
                if (profile.UserId is null)
                    throw new BadRequestException("Line Profile is null");
                var user = await _userService.ByLineId(profile.UserId);
                if (user is null)
                    return Ok(new AuthRsponse { Token = profile.UserId, RefreshToken = "" });
                string token = GenToken(user);
                RefreshToken refreshToken = await _refreshTokenService.GenerateRefreshToken(user.Id); //GenerateRefreshToken();
                // SetRefreshToken(refreshToken);
                return Ok(new AuthRsponse { Token = token, RefreshToken = refreshToken.Token });
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")] // for other client
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LineUserProfile))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<LineUserProfile>> LoginWithLine([FromQuery] LineCallBackParameter param)
        {
            try
            {
                LineAccessTokenResponse result = await _lineService.AccessToken(param);
                if (result is null)
                    throw new BadRequestException("line access token is null");

                LineUserProfile profile = await _lineService.Profile(result.AccessToken ?? "");

                return Ok(profile);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthRsponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthRsponse>> SignInWithLine([FromBody] LineSignIn formSignIn)
        {
            try
            {
                var user = await _userService.ByEmployeeNumber(formSignIn.EmployeeNo);
                if (user is null)
                    throw new BadRequestException($"Employee : {formSignIn.EmployeeNo} not found.");

                var duplicate = await _userService.ByLineId(formSignIn.LineId);
                if (duplicate is not null) // เช็ค VerifyCode หมดอายุด้วย
                    throw new BadRequestException($"already exists with this employee {duplicate.Username}");

                user.LineId = formSignIn.LineId;
                await _userService.SaveAsync();
                string token = GenToken(user);
                RefreshToken refreshToken = await _refreshTokenService.GenerateRefreshToken(user.Id);
                return Ok(new AuthRsponse { Token = token, RefreshToken = refreshToken.Token });
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        private string GenToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Username),
                new Claim(ClaimTypes.Name,user?.NameLanguages?.SingleOrDefault(n => n.Locale == "en")?.Name ?? ""),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Staff"),
                new Claim(ClaimTypes.Role,"BirdKak")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTimeSystem.Utc(DateTime.UtcNow).AddMinutes(15), signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}