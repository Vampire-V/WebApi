namespace WebApi.Models.User
{
    public class LineTokenRequest
    {
        public string GrantType { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}