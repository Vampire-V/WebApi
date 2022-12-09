using WebApi.Data.Accounting.Entities;
using WebApi.Models.SaleRebate;
using WebApi.Services.Base;

namespace WebApi.Data.Accounting.Repositories.Interfaces
{
    public interface ISaleRebateRepository : IScopedService 
    {
        Task<List<SaleRebateView>> GetSaleRebateViewFilterAsync(SaleRebateParameter SaleRebateParameter);
        Task<List<SaleRebateDetail>> GetSaleRebateDetailFilterAsync(SaleRebateParameter SaleRebateParameter);
    }
}