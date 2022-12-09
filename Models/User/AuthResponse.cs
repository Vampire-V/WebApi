using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class AuthRsponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}