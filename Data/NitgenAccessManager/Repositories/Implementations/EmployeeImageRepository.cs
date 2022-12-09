using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.NitgenAccessManager.Repositories.Interfaces;
using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Extensions;

namespace WebApi.Data.NitgenAccessManager.Repositories.Implementations
{
    public class EmployeeImageRepository : BaseRepository<EmployeeImage>, IEmployeeImageRepository
    {
        public EmployeeImageRepository(NitgenAccessManager db):base(db)
        {
        }
    }
}