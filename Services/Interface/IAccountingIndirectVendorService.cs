using WebApi.Models.AccountingIndirectVendor;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IAccountingIndirectVendorService : IScopedService
    {
         Task<List<IndirectVendorView>> GetIndirectVendors(IndirectVendorParameter indirectVendorParameter);
         Task<byte[]> GetIndirectVendorForExcel(IndirectVendorParameter indirectVendorParameter);
         Task<IndirectVendorView> GetIndirectVendor(string vendorCode);
         Task CreateIndirectVendor(IndirectVendorCreate indirectVendorCreate);
         Task UpdateIndirectVendor(string vendorCode,IndirectVendorUpdate indirectVendorUpdate);
         Task Delete(string vendorCode);
    }
}