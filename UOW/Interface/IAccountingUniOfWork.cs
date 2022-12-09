using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Services.Base;

namespace WebApi.UOW.Interface
{
    public interface IAccountingUniOfWork : IScopedService
    {
        Task<int> SaveAsync();
        IAccountingIndirectVendorRepository AccountingIndirectVendorRepository { get; set; }
        ITaxReportRepository TaxReportRepository { get; set; }
        IGrIrReportRepository GrIrReportRepository { get; set; }
        ISaleRebateRepository SaleRebateRepository { get; set; }
        IApAgingRepository ApAgingRepository { get; set; }
    }
}