using WebApi.Data.ChequeDirect.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IChequeDirectUnitOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IVendorRepository VendorRepository { get; set; }
    }
}