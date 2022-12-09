using Microsoft.EntityFrameworkCore;
using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Data.NitgenAccessManager.Repositories.Interfaces;
using WebApi.Extensions;
using WebApi.Models.Employee;

namespace WebApi.Data.NitgenAccessManager.Repositories.Implementations
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NitgenAccessManager db) : base(db)
        {
        }

        private IQueryable<Employee> NotResigned() {
            DateTime now = DateTimeSystem.Utc(DateTime.UtcNow);
            return _dbcontext.EmployeeTable.Where(e => e.ExpDate > now);
        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await NotResigned().ToListAsync();
        }

        public async Task<List<Employee>> StaffImage()
        {
            return await NotResigned().Include(i => i.EmployeeImage).Where(s => s.EmployeeImage!.Count > 0).ToListAsync();
        }

        public override async Task<Employee> GetById(string id)
        {
            return await NotResigned().Where(e => e.Id == id).FirstAsync();
        }

        public async Task<List<Employee>> Filter(EmployeeParameter paramiter)
        {
            IQueryable<Employee> query = NotResigned();
            if (!String.IsNullOrEmpty(paramiter.IdStaff))
            {
                query = query.Where<Employee>(e => e.Id.Trim().Contains(paramiter.IdStaff.Trim()));
            }
            if (!String.IsNullOrEmpty(paramiter.Name))
            {
                query = query.Where<Employee>(e => e.Name.ToLower().Contains(paramiter.Name.Trim().ToLower()));
            }
            // SQL Server 2005 ใช้ไม่ได้ 
            // var ss = await PaginatedList<Employee>.CreateAsync(query.AsNoTracking(), paramiter.page, paramiter.PageSize);
            return await query.Include(e => e.EmployeeImage).AsNoTracking().ToListAsync();
        }
    }
}