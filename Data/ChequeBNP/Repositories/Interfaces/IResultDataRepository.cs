using WebApi.Data.ChequeBNP.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.ChequeBNP.Repositories.Interfaces
{
    public interface IResultDataRepository : IScopedService, IBaseRepository<ResultData>
    {
    }
}