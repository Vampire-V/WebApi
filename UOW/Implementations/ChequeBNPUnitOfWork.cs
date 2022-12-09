using WebApi.Data.ChequeBNP;
using WebApi.Data.ChequeBNP.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class ChequeBNPUnitOfWork : IChequeBNPUnitOfWork
    {
        private readonly ChequeBNP _dbContext;

        public IResultDataRepository ResultDataRepository { get; set; }

        public ChequeBNPUnitOfWork(ChequeBNP dbContext, IResultDataRepository resultDataRepository)
        {
            _dbContext = dbContext;
            ResultDataRepository = resultDataRepository;
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