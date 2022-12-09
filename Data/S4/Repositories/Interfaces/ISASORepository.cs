using WebApi.Data.S4.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.S4.Repositories.Interfaces
{
    public interface ISASORepository : IScopedService, IBaseRepository<SASO>
    {
        List<SASO> GetSASO(DateTime pbdate, DateTime pcdate);
        // DataTable GetSACompare(DateTime pbdate, DateTime pcdate);
    }
}