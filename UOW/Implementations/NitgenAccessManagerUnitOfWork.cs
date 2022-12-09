using WebApi.Data.NitgenAccessManager;
using WebApi.Data.NitgenAccessManager.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class NitgenAccessManagerUnitOfWork : INitgenAccessManagerUnitOfWork
    {
        private readonly NitgenAccessManager _dbContext;
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IEmployeeImageRepository EmployeeImageRepository { get; set; }
        public IAuthLogRepository AuthLogRepository { get; set; }

        public NitgenAccessManagerUnitOfWork(NitgenAccessManager dbContext, IEmployeeRepository employeeRepository, IEmployeeImageRepository employeeImageRepository, IAuthLogRepository authLogRepository)
        {
            _dbContext = dbContext;
            EmployeeRepository = employeeRepository;
            EmployeeImageRepository = employeeImageRepository;
            AuthLogRepository = authLogRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}