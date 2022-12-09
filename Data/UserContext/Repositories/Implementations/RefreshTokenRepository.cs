using Microsoft.EntityFrameworkCore;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Data.UserContext.Repositories.Interfaces;

namespace WebApi.Data.UserContext.Repositories.Implementations
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(UserContext db) : base(db)
        {
        }

        public async Task<RefreshToken> GetToken(string token)
        {
            return await _dbcontext.RefreshToken.Where(r => r.Token.Equals(token)).FirstAsync();
        }

        public async Task MakeToken(RefreshToken entity)
        {
            await _dbcontext.AddAsync(entity);
        }

        public async Task RemoveByUser(int id)
        {
            IEnumerable<RefreshToken> items = await _dbcontext.RefreshToken.Where(r => r.UserId == id).ToListAsync();
            if (items.Count() > 0)
                _dbcontext.RemoveRange(items);
        }
    }
}