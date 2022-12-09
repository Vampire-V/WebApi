using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Data.UserContext.Repositories.Interfaces
{
    public interface IUserRepository : IScopedService, IBaseRepository<User>
    {
        Task<List<User>> GetUsersFilter(UserParameter vendorParameter);
        Task<User> ByEmail(string email);
        Task UpdateAsync(User entity);
        Task<User?> WithLineToken(string token);
        Task<User?> ByEmpCode(string code);
    }
}