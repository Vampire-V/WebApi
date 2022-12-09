using System.ComponentModel.DataAnnotations;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Models.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public List<UserTranslations>? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool Resigned { get; set; }
    }
}