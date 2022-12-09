using WebApi.Data.S4.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.S4.Repositories.Interfaces
{
    public interface ICOGIRepository : IScopedService, IBaseRepository<COGI>
    {
        List<COGI> GetCOGI(DateTime TimeStamp);
    }
}