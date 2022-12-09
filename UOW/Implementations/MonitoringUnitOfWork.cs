
using WebApi.Data.Monitoring;
using WebApi.Data.Monitoring.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class MonitoringUnitOfWork : IMonitoringUnitOfWork
    {
        private readonly Monitoring _dbContext;

        public IGRGIPlanRepository GRGIPlanRepository { get; set; }

        public MonitoringUnitOfWork(Monitoring dbContext, IGRGIPlanRepository gRGIPlanRepository)
        {
            _dbContext = dbContext;
            GRGIPlanRepository = gRGIPlanRepository;
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