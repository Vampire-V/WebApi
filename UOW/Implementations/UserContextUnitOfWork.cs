

using WebApi.Data.UserContext;
using WebApi.Data.UserContext.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class UserContextUnitOfWork : IUserContextUnitOfWork
    {
        private readonly UserContext _dbContext;
        public IUserRepository UserRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
        public IApiCallBackRepository ApiCallBackRepository { get; set; }

        public UserContextUnitOfWork(UserContext dbContext, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IApiCallBackRepository apiCallBackRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            RefreshTokenRepository = refreshTokenRepository;
            ApiCallBackRepository = apiCallBackRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}