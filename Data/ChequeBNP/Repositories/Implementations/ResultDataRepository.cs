using WebApi.Data.ChequeBNP.Entities;
using WebApi.Data.ChequeBNP.Repositories.Interfaces;

namespace WebApi.Data.ChequeBNP.Repositories.Implementations
{
    public class ResultDataRepository : BaseRepository<ResultData>, IResultDataRepository
    {

        public ResultDataRepository(ChequeBNP db) : base(db)
        {
        }
    }
}