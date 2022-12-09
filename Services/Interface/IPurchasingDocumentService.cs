using WebApi.Data.Accounting.Entities;
using WebApi.Models.GRIR;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IPurchasingDocumentService : IScopedService
    {
         Task<List<GrIrDetailView>> GetGrIrDetailViewFilterAsync(GrIrReportParameter GrIrReportParameter);
         Task<List<GrIrPlantView>> GetGrIrPlantViewFilterAsync(GrIrReportParameter GrIrReportParameter);
         Task<List<GrIrSummaryView>> GetGrIrSummaryViewFilterAsync(GrIrReportParameter GrIrReportParameter);
         Task<byte[]> GetGrIrReportForExcel(GrIrReportParameter GrIrReportParameter);
    }
}