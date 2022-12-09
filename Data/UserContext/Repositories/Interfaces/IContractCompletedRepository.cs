using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Data.UserContext.Repositories.Interfaces
{
    public interface IContractCompletedRepository : IScopedService, IBaseRepository<ContractCompleted>
    {
        Task<List<ContractCompleted>> NinetyDaysExpire();
        Task<List<ContractCompleted>> SixtyDaysExpire();
        Task<List<ContractCompleted>> ThirtyDaysExpire();
    }
}