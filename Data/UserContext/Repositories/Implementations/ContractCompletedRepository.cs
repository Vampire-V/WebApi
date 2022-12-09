using Microsoft.EntityFrameworkCore;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Data.UserContext.Repositories.Interfaces;
using WebApi.Middleware.Exceptions;
using WebApi.Extensions;

namespace WebApi.Data.UserContext.Repositories.Implementations
{
    public class ContractCompletedRepository : BaseRepository<ContractCompleted>, IContractCompletedRepository
    {
        public ContractCompletedRepository(UserContext db) : base(db)
        {
        }

        public async Task<List<ContractCompleted>> NinetyDaysExpire()
        {
            DateTime dateNow = DateTimeSystem.Utc(DateTime.UtcNow);
            DateTime dateMake = DateTimeSystem.ToDateTime("2022-12-10");
            var s = dateMake.Subtract(dateNow);
            // List<ContractCompleted> items = await _dbcontext.ContractCompleteds.Where(c => c.ExpiryDate.).ToListAsync();
            // return items;
            throw new System.NotImplementedException();
        }

        public Task<List<ContractCompleted>> SixtyDaysExpire()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ContractCompleted>> ThirtyDaysExpire()
        {
            throw new System.NotImplementedException();
        }
    }
}