using WebApi.Data.Monitoring.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IMonitoringUnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IGRGIPlanRepository GRGIPlanRepository { get; set; }
    }
}