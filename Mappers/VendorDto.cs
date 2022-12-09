using WebApi.Data.ChequeDirect.Entities;
using WebApi.Models.Vendor;

namespace WebApi.Mappers
{
    public class VendorDto : IVendorDto
    {
        public Vendor ToEntity(VendorCreate model)
        {
            return new Vendor()
            {
                TaxId = model.TaxId,
                VendorCode = model.VendorCode,
                Name = model.Name,
                Address = model.Address,
                Fax = model.Fax,
                Tel = model.Tel,
                PND = model.PND,
                TaxIdVendor1 = model.TaxIdVendor1,
                TaxIdVendor2 = model.TaxIdVendor2,
                TaxIdVendor3 = model.TaxIdVendor3,
                VATRegisNo = model.VATRegisNo
            };
        }

        public Vendor ToEntity(VendorView model)
        {
            return new Vendor()
            {
                TaxId = model.TaxId,
                VendorCode = model.VendorCode,
                Name = model.Name,
                Address = model.Address,
                Fax = model.Fax,
                Tel = model.Tel,
                PND = model.PND,
                TaxIdVendor1 = model.TaxIdVendor1,
                TaxIdVendor2 = model.TaxIdVendor2,
                TaxIdVendor3 = model.TaxIdVendor3,
                VATRegisNo = model.VATRegisNo
            };
        }

        public Vendor ToEntity(VendorUpdate model)
        {
            return new Vendor()
            {
                TaxId = model.TaxId,
                Name = model.Name,
                Address = model.Address,
                Fax = model.Fax,
                Tel = model.Tel,
                PND = model.PND,
                TaxIdVendor1 = model.TaxIdVendor1,
                TaxIdVendor2 = model.TaxIdVendor2,
                TaxIdVendor3 = model.TaxIdVendor3,
                VATRegisNo = model.VATRegisNo
            };
        }

        public VendorView ToView(Vendor entity)
        {
            return new VendorView()
            {
                TaxId = entity.TaxId,
                VendorCode = entity.VendorCode,
                Name = entity.Name,
                Address = entity.Address,
                Fax = entity.Fax,
                Tel = entity.Tel,
                PND = entity.PND,
                TaxIdVendor1 = entity.TaxIdVendor1,
                TaxIdVendor2 = entity.TaxIdVendor2,
                TaxIdVendor3 = entity.TaxIdVendor3,
                VATRegisNo = entity.VATRegisNo
            };
        }
    }
}