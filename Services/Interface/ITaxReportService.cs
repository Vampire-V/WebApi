using WebApi.Data.Accounting.Entities;
using WebApi.Models.TaxReportBSEG;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface ITaxReportService : IScopedService
    {
         Task<List<TaxReportView>> GetTaxReport(TaxReportParameter taxReportParameter); 
         Task<byte[]> GetTaxReportForExcel(TaxReportParameter taxReportParameter);
    }
}