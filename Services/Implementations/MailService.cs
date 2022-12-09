using WebApi.Services.Interface;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebApi.Config;
using System.Net;

namespace WebApi.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailConfig> _mailConfig;
        private SmtpClient _smtpClient;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailConfig> mailConfig, ILogger<MailService> logger)
        {
            _mailConfig = mailConfig;
            _smtpClient = new SmtpClient(_mailConfig.Value.Host, _mailConfig.Value.Port);
            
            if (_mailConfig.Value.UseDefaultCredentials)
            {
                // _smtpClient.UseDefaultCredentials = _mailConfig.Value.UseDefaultCredentials;
                _smtpClient.Credentials = new NetworkCredential(_mailConfig.Value.Username,_mailConfig.Value.Password);
                _smtpClient.EnableSsl = _mailConfig.Value.EnableSsl;
            }
            _logger = logger;
        }

        public async Task MailVerifyCode(string mailTo, string verifyCode)
        {
            // Specify the message content.
            // var test = Path.Combine(@"..\Template\Owners.txt");
            var resultBody = OneTimePasswordBody(verifyCode);
            MailMessage message = new MailMessage(new MailAddress(_mailConfig.Value.MailForm), new MailAddress(mailTo));
            message.IsBodyHtml = true;
            message.Body = resultBody;
            // $"รหัส OTP สำหรับใช้งาน Application ของคุณคือ {verifyCode} มีอายุ 10 นาที";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Application OTP";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            try
            {
                await _smtpClient.SendMailAsync(message);
                message.Dispose();
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"Smtp send email : {ex.Message}");
                throw ex;
            }
        }

        private string OneTimePasswordBody(string verifyCode)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(@"wwwroot\Template\otp.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{verifyCode}", verifyCode);
            return body;
        }

        public void Test(string mailTo,string text)
        {
            // Specify the message content.
            MailMessage message = new MailMessage(_mailConfig.Value.MailForm, mailTo);
            message.IsBodyHtml = true;
            message.Body = OneTimePasswordBody(text);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Application Schedule Test";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            try
            {
                _smtpClient.Send(message);
                message.Dispose();
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"Smtp test send email : {ex.Message}");
                throw ex;
            }
        }

        public void ReflectionMethod()
        {
            Console.WriteLine("kak bird!");
        }
    }
}