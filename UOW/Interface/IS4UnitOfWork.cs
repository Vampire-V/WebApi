using WebApi.Data.S4.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IS4UnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        ICOGIRepository COGIRepository { get; set; }
        ISASORepository SASORepository { get; set; }
    }
}