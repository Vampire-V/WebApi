using AutoMapper;
using WebApi.Models.Employee;
using WebApi.Data.NitgenAccessManager.Entities;

namespace WebApi.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeView>()
            .ForMember(model => model.Id, view => view.MapFrom(i => i.Id))
            .ForMember(model => model.Name, view => view.MapFrom(i => i.Name))
            .ReverseMap();
            
        }
    }
}