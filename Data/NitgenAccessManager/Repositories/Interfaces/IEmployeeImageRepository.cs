using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.NitgenAccessManager.Repositories.Interfaces
{
    public interface IEmployeeImageRepository :IScopedService, IBaseRepository<EmployeeImage>
    {
        //  List<Employee> GetEmployees();
    }
}