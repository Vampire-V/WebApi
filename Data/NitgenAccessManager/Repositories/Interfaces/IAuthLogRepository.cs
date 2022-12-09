using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.NitgenAccessManager.Repositories.Interfaces
{
    public interface IAuthLogRepository :IScopedService, IBaseRepository<AuthLog>
    {
        //  Task<List<Employee>> StaffImage();
    }
}