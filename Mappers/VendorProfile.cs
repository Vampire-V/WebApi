using AutoMapper;
using WebApi.Models.Vendor;
using WebApi.Data.ChequeDirect.Entities;

namespace WebApi.Mappers
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorView>()
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.Name, view => view.MapFrom(i => i.Name))
            .ForMember(model => model.Address, view => view.MapFrom(i => i.Address))
            .ForMember(model => model.Fax, view => view.MapFrom(i => i.Fax))
            .ForMember(model => model.Tel, view => view.MapFrom(i => i.Tel))
            .ForMember(model => model.PND, view => view.MapFrom(i => i.PND))
            .ForMember(model => model.TaxIdVendor1, view => view.MapFrom(i => i.TaxIdVendor1))
            .ForMember(model => model.TaxIdVendor2, view => view.MapFrom(i => i.TaxIdVendor2))
            .ForMember(model => model.TaxIdVendor3, view => view.MapFrom(i => i.TaxIdVendor3))
            .ForMember(model => model.VATRegisNo, view => view.MapFrom(i => i.VATRegisNo))
            .ReverseMap();

            CreateMap<Vendor, VendorCreate>()
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.Name, view => view.MapFrom(i => i.Name))
            .ForMember(model => model.Address, view => view.MapFrom(i => i.Address))
            .ForMember(model => model.Fax, view => view.MapFrom(i => i.Fax))
            .ForMember(model => model.Tel, view => view.MapFrom(i => i.Tel))
            .ForMember(model => model.PND, view => view.MapFrom(i => i.PND))
            .ForMember(model => model.TaxIdVendor1, view => view.MapFrom(i => i.TaxIdVendor1))
            .ForMember(model => model.TaxIdVendor2, view => view.MapFrom(i => i.TaxIdVendor2))
            .ForMember(model => model.TaxIdVendor3, view => view.MapFrom(i => i.TaxIdVendor3))
            .ForMember(model => model.VATRegisNo, view => view.MapFrom(i => i.VATRegisNo))
            .ReverseMap();

            CreateMap<Vendor, VendorUpdate>()
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.Name, view => view.MapFrom(i => i.Name))
            .ForMember(model => model.Address, view => view.MapFrom(i => i.Address))
            .ForMember(model => model.Fax, view => view.MapFrom(i => i.Fax))
            .ForMember(model => model.Tel, view => view.MapFrom(i => i.Tel))
            .ForMember(model => model.PND, view => view.MapFrom(i => i.PND))
            .ForMember(model => model.TaxIdVendor1, view => view.MapFrom(i => i.TaxIdVendor1))
            .ForMember(model => model.TaxIdVendor2, view => view.MapFrom(i => i.TaxIdVendor2))
            .ForMember(model => model.TaxIdVendor3, view => view.MapFrom(i => i.TaxIdVendor3))
            .ForMember(model => model.VATRegisNo, view => view.MapFrom(i => i.VATRegisNo))
            .ReverseMap();
        }
    }
}