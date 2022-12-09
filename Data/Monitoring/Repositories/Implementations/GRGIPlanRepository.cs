
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Monitoring.Repositories.Interfaces;
using WebApi.Data.Monitoring.Entities;

namespace WebApi.Data.Monitoring.Repositories.Implementations
{
    public class GRGIPlanRepository : BaseRepository<GrGiPlan>,IGRGIPlanRepository
    {
        public GRGIPlanRepository(Monitoring dbcontext): base(dbcontext)
        {
        }
    }
}