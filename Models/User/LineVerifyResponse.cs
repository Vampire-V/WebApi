namespace WebApi.Models.User
{
    public class LineVerifyResponse
    {
        public string? Scope { get; set; }
        public string? ClientId { get; set; }
        public long ExpiresIn { get; set; }
    }
}