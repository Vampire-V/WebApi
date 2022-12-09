using WebApi.Data.ChequeDirect.Entities;
using WebApi.Models.Vendor;

namespace WebApi.Mappers
{
    public interface IVendorDto
    {
        Vendor ToEntity(VendorCreate model);
        Vendor ToEntity(VendorView model);
        Vendor ToEntity(VendorUpdate model);
        VendorView ToView(Vendor entity);
    }
}