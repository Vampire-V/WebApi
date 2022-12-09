using AutoMapper;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.AccountingIndirectVendor;

namespace WebApi.Mappers
{
    public class AccountingIndirectVendorProfile : Profile
    {
        public AccountingIndirectVendorProfile()
        {
            CreateMap<AccountingIndirectVendor, IndirectVendorView>()
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.VendorName, view => view.MapFrom(i => i.VendorName))
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.HeadOfficeId, view => view.MapFrom(i => i.HeadOfficeId))
            .ForMember(model => model.BranchId, view => view.MapFrom(i => i.BranchId))
            .ReverseMap();

            CreateMap<AccountingIndirectVendor, IndirectVendorCreate>()
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.VendorName, view => view.MapFrom(i => i.VendorName))
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.HeadOfficeId, view => view.MapFrom(i => i.HeadOfficeId))
            .ForMember(model => model.BranchId, view => view.MapFrom(i => i.BranchId))
            .ReverseMap();

            CreateMap<AccountingIndirectVendor, IndirectVendorUpdate>()
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.VendorName, view => view.MapFrom(i => i.VendorName))
            .ForMember(model => model.TaxId, view => view.MapFrom(i => i.TaxId))
            .ForMember(model => model.HeadOfficeId, view => view.MapFrom(i => i.HeadOfficeId))
            .ForMember(model => model.BranchId, view => view.MapFrom(i => i.BranchId))
            .ReverseMap();

        }
    }
}