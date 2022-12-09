using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Models.ProductionOrder;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IProductionOrderService : IScopedService
    {
        Task<List<FGProductionOrder>> GetFGProductionOrderFilterAsync(ProductionOrderParameter Parameter);
        Task<List<SFGProductionOrder>> GetSFGProductionOrderFilterAsync(ProductionOrderParameter Parameter);
        Task<byte[]> GetFGProductionOrderForExcel(ProductionOrderParameter Parameter);
        Task<byte[]> GetSFGProductionOrderForExcel(ProductionOrderParameter Parameter);

        Task<List<FGOffline>> GetFGOfflineFilterAsync(OfflineParameter Parameter);
        Task<List<SFGOffline>> GetSFGOfflineFilterAsync(OfflineParameter Parameter);
        Task<List<OfflineSummarize>> GetOfflineSummarizeFilterAsync(string OrderNo);
        Task<byte[]> GetFGOfflineForExcel(OfflineParameter Parameter);
        Task<byte[]> GetSFGOfflineForExcel(OfflineParameter Parameter);
        Task<byte[]> GetOfflineSummarizeForExcel(string OrderNo);

        Task<List<PoInformation>> GetPoInformationFilterAsync(PoInformationParameter Parameter);
        Task<byte[]> GetPoInformationForExcel(PoInformationParameter Parameter);
    }
}