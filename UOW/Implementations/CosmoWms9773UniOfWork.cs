using WebApi.Data.CosmoIm9773;
using WebApi.Data.CosmoWms9773.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class CosmoWms9773UniOfWork : ICosmoWms9773UniOfWork
    {
        private readonly CosmoIm9773 _dbContext;

        public IPoInformationRepository PoInformationRepository { get; set; }

        public CosmoWms9773UniOfWork(CosmoIm9773 dbContext, IPoInformationRepository iPoInformationRepository)
        {
            _dbContext = dbContext;
            PoInformationRepository = iPoInformationRepository;
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