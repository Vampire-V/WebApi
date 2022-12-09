using Microsoft.EntityFrameworkCore;
using WebApi.Data.S4.Repositories.Interfaces;
using WebApi.Data.S4.Entities;

namespace WebApi.Data.S4.Repositories.Implementations
{
    public class COGIRepository : BaseRepository<COGI>, ICOGIRepository
    {
        public COGIRepository(S4 db):base(db)
        {
        }
        public List<COGI> GetCOGI(DateTime TimeStamp)
        {
                var COGIList = this._dbcontext.COGITable
                    .FromSqlRaw("SELECT * FROM [S4].[dbo].[AFFW] WHERE convert(varchar, [TIME_STAMP], 112) = '" + TimeStamp.ToString("yyyyMMdd") + "'")
                    .ToList();
                return COGIList;
        }
    }
}