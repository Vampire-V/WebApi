using AutoMapper;
using WebApi.Data.S4.Entities;
using WebApi.Models.SASO;

namespace WebApi.Mappers
{
    public class SASOProfile : Profile
    {
        public SASOProfile()
        {
            CreateMap<SASO, SASOView>()
            .ForMember(model => model.Plnt, view => view.MapFrom(i => i.Plnt))
            .ForMember(model => model.Customer, view => view.MapFrom(i => i.Customer))
            .ForMember(model => model.CustName, view => view.MapFrom(i => i.CustName))
            .ForMember(model => model.Material, view => view.MapFrom(i => i.Material))
            .ForMember(model => model.MaterialDesc, view => view.MapFrom(i => i.MaterialDesc))
            .ForMember(model => model.TotalB, view => view.MapFrom(i => i.TotalB))
            .ForMember(model => model.TotalC, view => view.MapFrom(i => i.TotalC))
            .ForMember(model => model.TotalDiff, view => view.MapFrom(i => i.TotalDiff))
            .ForMember(model => model.TotalSOB, view => view.MapFrom(i => i.TotalSOB))
            .ForMember(model => model.TotalSOC, view => view.MapFrom(i => i.TotalSOC))
            .ForMember(model => model.TotalSODiff, view => view.MapFrom(i => i.TotalSODiff))
            .ReverseMap();
        }
    }
}