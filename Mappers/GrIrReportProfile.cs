using AutoMapper;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.GRIR;

namespace WebApi.Mappers
{
    public class GrIrReportProfile : Profile
    {
        public GrIrReportProfile()
        {
            CreateMap<GrIrDetailView, GrIrReportView>()
            .ForMember(model => model.Assingment, view => view.MapFrom(i => i.Assingment))
            .ForMember(model => model.PurchasingDocument, view => view.MapFrom(i => i.PurchasingDocument))
            .ForMember(model => model.VendorCode, view => view.MapFrom(i => i.VendorCode))
            .ForMember(model => model.VendorName, view => view.MapFrom(i => i.Vendors!.VendorName))
            .ForMember(model => model.Plant, view => view.MapFrom(i => i.Plant))
            .ForMember(model => model.PurchaseTypeDesc, view => view.MapFrom(i => i.PurchaseTypeDesc))
            .ForMember(model => model.GlAcct, view => view.MapFrom(i => i.GlAcct))
            .ForMember(model => model.DocumentHeaderText, view => view.MapFrom(i => i.DocumentHeaderText))
            .ForMember(model => model.Reference, view => view.MapFrom(i => i.Reference))
            .ForMember(model => model.DocumentNo, view => view.MapFrom(i => i.DocumentNo))
            .ForMember(model => model.BusinessArea, view => view.MapFrom(i => i.BusinessArea))
            .ForMember(model => model.DocumentType, view => view.MapFrom(i => i.DocumentType))
            .ForMember(model => model.YearMonth, view => view.MapFrom(i => i.YearMonth))
            .ForMember(model => model.PostingDate, view => view.MapFrom(i => i.PostingDate))
            .ForMember(model => model.DocumentDate, view => view.MapFrom(i => i.DocumentDate))
            .ForMember(model => model.DebitCredit, view => view.MapFrom(i => i.DebitCredit))
            .ForMember(model => model.Quantity, view => view.MapFrom(i => i.Quantity))
            .ForMember(model => model.AmountLc, view => view.MapFrom(i => i.AmountLc))
            .ForMember(model => model.LocalCurrency, view => view.MapFrom(i => i.LocalCurrency))
            .ForMember(model => model.AmountDc, view => view.MapFrom(i => i.AmountDc))
            .ForMember(model => model.DocumentCurrency, view => view.MapFrom(i => i.DocumentCurrency))
            .ForMember(model => model.ClearingDocument, view => view.MapFrom(i => i.ClearingDocument))
            .ForMember(model => model.ProfitCenter, view => view.MapFrom(i => i.ProfitCenter))
            .ForMember(model => model.OffsetAcct, view => view.MapFrom(i => i.OffsetAcct))
            .ForMember(model => model.Text, view => view.MapFrom(i => i.Text))
            .ForMember(model => model.ObjectKey, view => view.MapFrom(i => i.ObjectKey))
            .ForMember(model => model.ReferenceKeyThree, view => view.MapFrom(i => i.ReferenceKeyThree))
            .ForMember(model => model.DateDiff, view => view.MapFrom(i => i.DateDiff))
            .ForMember(model => model.Aging, view => view.MapFrom(i => i.Aging))
            .ReverseMap();
        }
    }
}