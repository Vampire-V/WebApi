using Microsoft.EntityFrameworkCore;
using WebApi.Data.S4.Repositories.Interfaces;
using WebApi.Data.S4.Entities;

namespace WebApi.Data.S4.Repositories.Implementations
{
    public class SASORepository : BaseRepository<SASO>, ISASORepository
    {
        public SASORepository(S4 db):base(db)
        {
        }
        public List<SASO> GetSASO(DateTime pbdate, DateTime pcdate)
        {
            return this._dbcontext.SASOTable
                    .FromSqlRaw("exec spSAComparePivot '" + pbdate.ToString("yyyyMMdd") + "','" + pcdate.ToString("yyyyMMdd") + "'")
                    .ToList();
        }
    }
}