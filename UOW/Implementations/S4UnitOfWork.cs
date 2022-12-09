using WebApi.Data.S4;
using WebApi.Data.S4.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class S4UnitOfWork : IS4UnitOfWork
    {
        private readonly S4 _dbContext;
        public ICOGIRepository COGIRepository { get; set; }
        public ISASORepository SASORepository { get; set; }

        public S4UnitOfWork(S4 dbContext, ICOGIRepository cOGIRepository, ISASORepository sASORepository)
        {
            _dbContext = dbContext;
            COGIRepository = cOGIRepository;
            SASORepository = sASORepository;
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