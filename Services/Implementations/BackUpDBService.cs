using WebApi.Services.Interface;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebApi.Config;
using System.Net;
using System.IO.Compression;
using WebApi.Extensions;
using FluentFTP;
using MySql.Data.MySqlClient;

namespace WebApi.Services.Implementations
{
    public class BackUpDBService : IBackUpDBService
    {
        private readonly IOptions<MailConfig> _mailConfig;
        private readonly IOptions<FtpSettings> _ftpSetting;
        private readonly IOptions<FolderBackUp> _folderBackUp;
        private SmtpClient _smtpClient;
        private readonly IFileService _fileService;
        private readonly ILogger<BackUpDBService> _logger;
        protected readonly IConfiguration _configuration;
        public BackUpDBService(IOptions<MailConfig> mailConfig, ILogger<BackUpDBService> logger, IOptions<FtpSettings> ftpSetting, IOptions<FolderBackUp> folderBackUp, IConfiguration configuration, IFileService fileService)
        {
            _logger = logger;
            _mailConfig = mailConfig;
            _ftpSetting = ftpSetting;

            _smtpClient = new SmtpClient(_mailConfig.Value.Host, _mailConfig.Value.Port);

            if (_mailConfig.Value.UseDefaultCredentials)
            {
                // _smtpClient.UseDefaultCredentials = _mailConfig.Value.UseDefaultCredentials;
                _smtpClient.Credentials = new NetworkCredential(_mailConfig.Value.Username, _mailConfig.Value.Password);
                _smtpClient.EnableSsl = _mailConfig.Value.EnableSsl;
            }
            _folderBackUp = folderBackUp;
            _configuration = configuration;
            _fileService = fileService;
        }
        
        public void BackupMysql()
        {
            string constring = $"{_configuration.GetConnectionString("UserContext")};Connect Timeout=30";
            DateTime time = DateTimeSystem.Utc(DateTime.UtcNow);
            string file = @$"wwwroot/Backup/{time.ToString("yyyyMMddHHmmss")}.sql";

            WatchDog.WatchLogger.Log($"BackupMysql Start....");
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }
            WatchDog.WatchLogger.Log($"BackupMysql Success....");
            _fileService.FtpToBackUp();
        }
    }
}