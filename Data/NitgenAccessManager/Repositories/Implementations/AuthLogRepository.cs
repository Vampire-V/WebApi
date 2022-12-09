using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Data.NitgenAccessManager.Repositories.Interfaces;

namespace WebApi.Data.NitgenAccessManager.Repositories.Implementations
{
    public class AuthLogRepository : BaseRepository<AuthLog>, IAuthLogRepository
    {
        public AuthLogRepository(NitgenAccessManager db) : base(db)
        {
        }
    }
}