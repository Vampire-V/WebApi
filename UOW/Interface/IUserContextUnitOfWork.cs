using WebApi.Data.UserContext.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IUserContextUnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IUserRepository UserRepository { get; set; }
        IRefreshTokenRepository RefreshTokenRepository { get; set; }
        IApiCallBackRepository ApiCallBackRepository { get; set; }
    }
}