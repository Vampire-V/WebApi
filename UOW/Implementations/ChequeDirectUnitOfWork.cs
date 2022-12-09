using WebApi.Data.ChequeDirect;
using WebApi.Data.ChequeDirect.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class ChequeDirectUnitOfWork : IChequeDirectUnitOfWork
    {
        private readonly ChequeDirect _dbContext;
        public IVendorRepository VendorRepository { get; set; }

        public ChequeDirectUnitOfWork(ChequeDirect dbContext, IVendorRepository vendorRepository)
        {
            _dbContext = dbContext;
            VendorRepository = vendorRepository;
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