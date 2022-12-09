using WebApi.Data.Accounting.Entities;
using WebApi.Models.TaxReportBSEG;
using WebApi.Services.Base;

namespace WebApi.Data.Accounting.Repositories.Interfaces
{
    public interface ITaxReportRepository : IScopedService
    {
        Task<List<TaxReportView>> GetTaxReportFilterAsync(TaxReportParameter taxReportParameter);
    }
}