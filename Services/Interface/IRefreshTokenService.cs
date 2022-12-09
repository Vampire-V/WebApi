using WebApi.Data.UserContext.Entities;
using WebApi.Services.Base;

namespace WebApi.Services.Interface{
    public interface IRefreshTokenService : IScopedService
    {
        Task<RefreshToken> GetToken(string token);
        Task<bool> IsExpire(string token);
        Task<RefreshToken> GenerateRefreshToken(int userId);
        Task RemoveByUser(int id);
    }
}