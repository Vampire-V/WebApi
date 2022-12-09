using AutoMapper;
using WebApi.Services.Interface;
using WebApi.Models.User;
using WebApi.Extensions;
using WebApi.Data.UserContext.Entities;
using WebApi.UOW.Interface;
using WebApi.Middleware.Exceptions;

namespace WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserContextUnitOfWork _userContextUnitOfWork;
        public readonly IMapper _mapper;
        private readonly ILogger<MailService> _logger;
        public UserService(IMapper mapper, ILogger<MailService> logger, IUserContextUnitOfWork userContextUnitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _userContextUnitOfWork = userContextUnitOfWork;
        }

        public async Task<List<User>> GetUsers(UserParameter parameter)
        {
            return await _userContextUnitOfWork.UserRepository.GetUsersFilter(parameter);
        }

        public async Task<User> GetUser(int id)
        {
            return await _userContextUnitOfWork.UserRepository.GetById(id);
        }

        public async Task<User> UserByEmail(string email)
        {
            return await _userContextUnitOfWork.UserRepository.ByEmail(email);
        }

        public async Task<User> SetVerifyCodeByEmail(string email, string code)
        {
            var user = await _userContextUnitOfWork.UserRepository.ByEmail(email);
            user.VerifyCode = code;
            user.ExpiredCode = DateTimeSystem.Utc(DateTime.UtcNow).AddMinutes(10);
            await _userContextUnitOfWork.SaveAsync();
            return user;
        }

        public async Task<User?> ByLineId(string id)
        {
            try
            {
                return await _userContextUnitOfWork.UserRepository.WithLineToken(id);
            }
            catch (Exception ex)
            {
                
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<User?> ByEmployeeNumber(string code)
        {
            try
            {
                return await _userContextUnitOfWork.UserRepository.ByEmpCode(code);
            }
            catch (Exception ex)
            {
                
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                // user.LineId = lineId;
                await _userContextUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                
                throw new BadRequestException(ex.Message);
            }
        }
    }
}