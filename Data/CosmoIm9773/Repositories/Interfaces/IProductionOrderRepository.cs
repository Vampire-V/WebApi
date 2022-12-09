
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Models.ProductionOrder;
using WebApi.Services.Base;

namespace WebApi.Data.CosmoIm9773.Repositories.Interfaces
{
    public interface IProductionOrderRepository : IScopedService
    {
        Task<List<FGProductionOrder>> GetFGProductionOrderFilterAsync(ProductionOrderParameter Parameter);
        Task<List<SFGProductionOrder>> GetSFGProductionOrderFilterAsync(ProductionOrderParameter Parameter);
        Task<List<FGOffline>> GetFGOfflineFilterAsync(OfflineParameter Parameter);
        Task<List<SFGOffline>> GetSFGOfflineFilterAsync(OfflineParameter Parameter);
        Task<List<OfflineSummarize>> GetOfflineSummarizeFilterAsync(string OrderNo);
    }
}