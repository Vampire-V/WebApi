using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Models;
using WebApi.Models.Employee;
using WebApi.Services.Base;

namespace WebApi.Data.NitgenAccessManager.Repositories.Interfaces
{
    public interface IEmployeeRepository :IScopedService, IBaseRepository<Employee>
    {
         Task<List<Employee>> StaffImage();
         Task<List<Employee>> Filter(EmployeeParameter paramiter);
    }
}