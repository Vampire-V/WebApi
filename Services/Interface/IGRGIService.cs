using WebApi.Data.Monitoring.Entities;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IGRGIService : IScopedService
    {
        Task<IEnumerable<GrGiPlan>> GetGRGIPlan();

        Task CreateRang(IEnumerable<GrGiPlan> entity);

        Task AddOrUpdateRange(List<GrGiPlan> items);
    }
}