using WebApi.Data.Accounting.Entities;
using WebApi.Models.SaleRebate;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface ISaleRebateService : IScopedService
    {
        Task<List<SaleRebateView>> GetSaleRebateViewFilterAsync(SaleRebateParameter SaleRebateParameter);
        Task<List<SaleRebateDetail>> GetSaleRebateDetailFilterAsync(SaleRebateParameter SaleRebateParameter);
        Task<byte[]> GetSaleRebateForExcel(SaleRebateParameter SaleRebateParameter);
        Task<byte[]> GetSaleRebateTemplateForExcel(SaleRebateParameter SaleRebateParameter);
    }
}