namespace WebApi.Config
{
    public class MailConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string MailForm { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}