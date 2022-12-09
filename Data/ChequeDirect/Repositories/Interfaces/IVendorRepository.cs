using WebApi.Models.Vendor;
using WebApi.Data.ChequeDirect.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.ChequeDirect.Repositories.Interfaces
{
    public interface IVendorRepository :IScopedService, IBaseRepository<Vendor>
    {
        Task<List<Vendor>> GetVendorsFilterAsync(VendorParameter vendorParameter);
    }
}