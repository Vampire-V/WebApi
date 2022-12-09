using WebApi.Data.Accounting;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.UOW.Interface;

namespace WebApi.UOW.Implementations
{
    public class AccountingUniOfWork : IAccountingUniOfWork
    {
        private readonly Accounting _dbContext;
        public IAccountingIndirectVendorRepository AccountingIndirectVendorRepository { get; set; }
        public ITaxReportRepository TaxReportRepository { get; set;}
        public IGrIrReportRepository GrIrReportRepository { get; set; }
        public ISaleRebateRepository SaleRebateRepository { get; set; }
        public IApAgingRepository ApAgingRepository { get; set; }


        public AccountingUniOfWork(
            Accounting dbContext,
            IAccountingIndirectVendorRepository iAccountingIndirectVendorRepository,
            ITaxReportRepository iTaxReportRepository, 
            IGrIrReportRepository iGrIrReportRepository,
            ISaleRebateRepository iSaleRebateRepository,
            IApAgingRepository iApAgingRepository
        )
        {
            _dbContext = dbContext;
            AccountingIndirectVendorRepository = iAccountingIndirectVendorRepository;
            TaxReportRepository = iTaxReportRepository;
            GrIrReportRepository = iGrIrReportRepository;
            SaleRebateRepository = iSaleRebateRepository;
            ApAgingRepository = iApAgingRepository;
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