using WebApi.Data.ChequeBNP.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IChequeBNPUnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IResultDataRepository ResultDataRepository { get; set; }
    }
}