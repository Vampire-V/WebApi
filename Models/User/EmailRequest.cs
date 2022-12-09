using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class EmailRequest
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}