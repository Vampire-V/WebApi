using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(4)]
        public string VerifyCode { get; set; } = null!;
    }
}