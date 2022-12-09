using AutoMapper;
using WebApi.Data.S4.Entities;
using WebApi.Models.COGI;

namespace WebApi.Mappers
{
    public class COGIProfile : Profile
    {
        public COGIProfile()
        {
            CreateMap<COGI, COGIView>()
            .ForMember(model => model.Plant, view => view.MapFrom(i => i.Plant))
            .ForMember(model => model.ReservNo, view => view.MapFrom(i => i.ReservNo))
            .ForMember(model => model.OrderNo, view => view.MapFrom(i => i.OrderNo))
            .ForMember(model => model.Material, view => view.MapFrom(i => i.Material))
            .ForMember(model => model.Location, view => view.MapFrom(i => i.Location))
            .ForMember(model => model.Quantity, view => view.MapFrom(i => i.Quantity))
            .ForMember(model => model.Unit, view => view.MapFrom(i => i.Unit))
            .ForMember(model => model.MovementType, view => view.MapFrom(i => i.MovementType))
            .ForMember(model => model.MessageNo, view => view.MapFrom(i => i.MessageNo))
            .ForMember(model => model.MessageType, view => view.MapFrom(i => i.MessageType))
            .ForMember(model => model.ErrorMessage, view => view.MapFrom(i => i.ErrorMessage))
            .ForMember(model => model.Mrp, view => view.MapFrom(i => i.Mrp))
            .ForMember(model => model.PostingDate, view => view.MapFrom(i => i.PostingDate))
            .ForMember(model => model.RowId, view => view.MapFrom(i => i.RowId))
            .ForMember(model => model.TimeStamp, view => view.MapFrom(i => i.TimeStamp))
            .ReverseMap();
        }
    }
}