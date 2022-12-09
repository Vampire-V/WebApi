using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class RefreshTokenRequest
    {
        [Required]
        public string Token { get; set; } = null!;
    }
}