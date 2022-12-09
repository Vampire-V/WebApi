using WebApi.Models.ChequeBNP.Example;
using WebApi.Models.Vendor;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IVendorService : IScopedService
    {
        Task<List<VendorView>> GetVendors(VendorParameter vendorParameter);
        Task<byte[]> GetVendorForExcel(VendorParameter vendorParameter);
        Task<VendorView> GetVendor(string vendorCode);
        Task CreateVendor(VendorCreate vendorCreate);
        Task UpdateVendor(string vendorCode,VendorUpdate vendorUpdate);
        Task Delete(string vendorCode);
        Task<Document> TestChequeBNP();
    }
}