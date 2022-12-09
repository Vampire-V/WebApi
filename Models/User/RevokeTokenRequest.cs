using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class RevokeTokenRequest
    {
        [Required]
        public string Token { get; set; } = null!;
    }
}