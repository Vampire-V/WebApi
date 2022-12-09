using WebApi.Data.Monitoring.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.Monitoring.Repositories.Interfaces
{
    public interface IGRGIPlanRepository : IScopedService, IBaseRepository<GrGiPlan>
    {
        // Task CreateRangAsync(IEnumerable<GrGiPlan> entity);
    }
}