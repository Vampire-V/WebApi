using WebApi.Data.CosmoIm9773;
using WebApi.Data.CosmoIm9773.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class CosmoIm9773UniOfWork : ICosmoIm9773UniOfWork
    {
        private readonly CosmoIm9773 _dbContext;

        public IProductionOrderRepository ProductionOrderRepository { get; set; }

        public CosmoIm9773UniOfWork(CosmoIm9773 dbContext, IProductionOrderRepository iProductionOrderRepository)
        {
            _dbContext = dbContext;
            ProductionOrderRepository = iProductionOrderRepository;
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