using WebApi.Data.Accounting.Entities;
using WebApi.Models.AccountingIndirectVendor;
using WebApi.Services.Base;

namespace WebApi.Data.Accounting.Repositories.Interfaces
{
    public interface IAccountingIndirectVendorRepository : IScopedService, IBaseRepository<AccountingIndirectVendor>
    {
        Task<List<AccountingIndirectVendor>> GetIndirectVendorsFilterAsync(IndirectVendorParameter indirectVendorParameter);
    }
}