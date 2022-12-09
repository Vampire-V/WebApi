using AutoMapper;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
            .ForMember(model => model.Id, view => view.MapFrom(i => i.Id))
            .ForMember(model => model.Name, view => view.MapFrom(i => i.NameLanguages))
            .ForMember(model => model.Email, view => view.MapFrom(i => i.Email))
            .ForMember(model => model.Username, view => view.MapFrom(i => i.Username))
            .ForMember(model => model.Resigned, view => view.MapFrom(i => i.Resigned))
            .ReverseMap();
        }
    }
}