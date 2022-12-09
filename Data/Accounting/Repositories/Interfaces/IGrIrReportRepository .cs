using WebApi.Data.Accounting.Entities;
using WebApi.Models.GRIR;
using WebApi.Services.Base;

namespace WebApi.Data.Accounting.Repositories.Interfaces
{
    public interface IGrIrReportRepository : IScopedService
    {
        Task<List<GrIrDetailView>> GetGrIrDetailViewFilterAsync(GrIrReportParameter GrIrReportParameter);
        Task<List<GrIrPlantView>> GetGrIrPlantViewFilterAsync(GrIrReportParameter GrIrReportParameter);
        Task<List<GrIrSummaryView>> GetGrIrSummaryViewFilterAsync(GrIrReportParameter GrIrReportParameter);
    }
}