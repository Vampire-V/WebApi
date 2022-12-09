using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IUserService : IScopedService
    {
        Task<List<User>> GetUsers(UserParameter parameter);
        Task<User> GetUser(int id);
        Task<User> UserByEmail(string email);
        Task<User> SetVerifyCodeByEmail(string email, string code);
        Task<User?> ByLineId(string id);
        Task<User?> ByEmployeeNumber(string code);
        Task SaveAsync();
    }
}