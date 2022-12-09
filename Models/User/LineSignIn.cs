using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.User
{
    public class LineSignIn
    {
        [Required]
        public string EmployeeNo { get; set; } = null!;
        [Required]
        public string LineId { get; set; } = null!;
    }
}