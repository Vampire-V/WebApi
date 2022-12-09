namespace WebApi.Data.UserContext.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? VerifyCode { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Resigned { get; set; }
        public DateTime? ExpiredCode { get; set; }
        public string? LineId { get; set; }
        public virtual List<UserTranslations>? NameLanguages { get; set; }
        // public List<UserRefreshToken>? UserRefreshToken { get; set; }
    }
}