namespace WebApi.Models.User
{
    public class UserParameter : QueryStringParameters
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}