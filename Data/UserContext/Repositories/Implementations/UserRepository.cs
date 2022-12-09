using Microsoft.EntityFrameworkCore;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Data.UserContext.Repositories.Interfaces;
using WebApi.Middleware.Exceptions;

namespace WebApi.Data.UserContext.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UserContext db) : base(db)
        {
        }

        public async Task<List<User>> GetUsersFilter(UserParameter parameter)
        {
            IQueryable<User> query = this._dbcontext.Users.Where(u => u.Resigned == false); //.ToArray().Skip(1).Take(10);

            if (!String.IsNullOrEmpty(parameter.Username))
            {
                query = query.Where(u => u.Username.Contains(parameter.Username));
            }
            if (!String.IsNullOrEmpty(parameter.Email))
            {
                query = query.Where(u => u.Email.Contains(parameter.Email));
            }
            var results = await query.ToListAsync();
            return results;
        }

        public async Task<User> ByEmail(string email)
        {
            var user = await _dbcontext.Users.Where(x => x.Email.ToLower() == email.ToLower()).Include(u => u.NameLanguages).FirstOrDefaultAsync();
            if (user is null)
            {
                throw new BadRequestException($"email not found.");
            }
            return user;
        }

        public async Task UpdateAsync(User entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<User?> WithLineToken(string token)
        {
            try
            {
                return await _dbcontext.Users.Where(u => u.LineId == token).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<User?> ByEmpCode(string code)
        {
             try
            {
                return await _dbcontext.Users.Where(u => u.Username == code).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }
        }
    }
}