using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Data.UserContext.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IScopedService, IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetToken(string token);

        Task MakeToken(RefreshToken entity);
        Task RemoveByUser(int id);

    }
}