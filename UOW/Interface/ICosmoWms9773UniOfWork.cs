using WebApi.Data.CosmoWms9773.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface ICosmoWms9773UniOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IPoInformationRepository PoInformationRepository { get; set; }
    }
}