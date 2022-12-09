using WebApi.Data.CosmoIm9773.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface ICosmoIm9773UniOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IProductionOrderRepository ProductionOrderRepository { get; set; }
    }
}