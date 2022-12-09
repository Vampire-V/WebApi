using AutoMapper;
using WebApi.Services.Interface;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using WebApi.Extensions;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.UOW.Interface;
using WebApi.Middleware.Exceptions;

namespace WebApi.Services.Implementations
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUserContextUnitOfWork _userContextUnitOfWork;
        private readonly ILogger<RefreshTokenService> _logger;
        public RefreshTokenService(ILogger<RefreshTokenService> logger, IUserContextUnitOfWork userContextUnitOfWork)
        {
            _logger = logger;
            _userContextUnitOfWork = userContextUnitOfWork;
        }

        public async Task<RefreshToken> GenerateRefreshToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                Created = DateTimeSystem.Utc(DateTime.UtcNow),
                Expires = DateTimeSystem.Utc(DateTime.UtcNow).AddDays(7)
            };
            try
            {

                await _userContextUnitOfWork.RefreshTokenRepository.MakeToken(refreshToken);
                await _userContextUnitOfWork.SaveAsync();
                return refreshToken;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<RefreshToken> GetToken(string token)
        {
            return await _userContextUnitOfWork.RefreshTokenRepository.GetToken(token);
        }

        public async Task<bool> IsExpire(string token)
        {
            var refreshToken = await _userContextUnitOfWork.RefreshTokenRepository.GetToken(token);
            if (refreshToken is null)
                return true;
            if (DateTime.Compare(DateTimeSystem.Utc(DateTime.UtcNow), DateTimeSystem.Utc(refreshToken.Expires)) >= 0)
                return true;
            return false;
        }

        public async Task RemoveByUser(int id)
        {
            try
            {
                await _userContextUnitOfWork.RefreshTokenRepository.RemoveByUser(id);
                await _userContextUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
            
        }
    }
}