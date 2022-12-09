using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Middleware.Exceptions;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMailService _mailServise;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger, IMailService mailServise)
        {
            _userService = userService;
            _logger = logger;
            _mailServise = mailServise;
        }

        // [Authorize(Roles = "Staff")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> GetUsers([FromQuery] UserParameter parameter)
        {
            try
            {
                var users = await _userService.GetUsers(parameter);
                _logger.LogInformation("Returned users from database.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get users all : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }

        // [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get user : {ex.Message}");
                throw new BadRequestException(ex.Message);
            }
        }
    }
}