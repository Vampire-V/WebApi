using WebApi.Extensions;

namespace WebApi.Data.UserContext.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        // public string? RefreshToken { get; set; }
        // public bool? IsActive { get; set; }
    }
}