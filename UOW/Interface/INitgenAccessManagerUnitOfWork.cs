using WebApi.Data.NitgenAccessManager.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface INitgenAccessManagerUnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IEmployeeRepository EmployeeRepository { get; set; }
        IEmployeeImageRepository EmployeeImageRepository { get; set; }
        IAuthLogRepository AuthLogRepository { get; set; }
    }
}