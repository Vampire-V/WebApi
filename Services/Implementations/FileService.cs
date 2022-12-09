using WebApi.Services.Interface;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebApi.Config;
using System.Net;
using System.IO.Compression;
using WebApi.Extensions;
using FluentFTP;

namespace WebApi.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IOptions<FtpSettings> _ftpSetting;
        private readonly IOptions<FolderBackUp> _folderBackUp;
        public FileService(IOptions<MailConfig> mailConfig, ILogger<FileService> logger, IOptions<FtpSettings> ftpSetting, IOptions<FolderBackUp> folderBackUp)
        {
            _ftpSetting = ftpSetting;

            _folderBackUp = folderBackUp;
        }

        public void BackupHaierKPI()
        {
            var time = DateTimeSystem.Utc(DateTime.UtcNow);
            string fileName = $"kpi{time.ToString("yyyyMMddHHmmss")}.zip";

            string sourceDirectoryName = @$"{_folderBackUp.Value.Kpi}";
            string destinationArchiveFileName = @$"wwwroot/Backup/{fileName}";

            try
            {
                ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, System.IO.Compression.CompressionLevel.SmallestSize, true);

                //Create FTP Request.
                var client = new FtpClient(_ftpSetting.Value.Host, _ftpSetting.Value.Username, _ftpSetting.Value.Password);
                client.AutoConnect();
                // Delete files older than 7 days
                RemoveOldFtp(client, _ftpSetting.Value.RootKPI);
                // upload a file
                string name = Path.GetFileName(destinationArchiveFileName);
                client.UploadFile(destinationArchiveFileName, $"{_ftpSetting.Value.RootKPI}{fileName}");
                client.Disconnect();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public void FtpToBackUp()
        {
            WatchDog.WatchLogger.Log($"FtpToBackUp Start....");
            string destinationArchive = @"wwwroot/Backup";
            string[] filesLoc = Directory.GetFiles(destinationArchive);
            var client = new FtpClient(_ftpSetting.Value.Host, _ftpSetting.Value.Username, _ftpSetting.Value.Password);
            client.AutoConnect();
            // Delete files older than 7 days
            RemoveOldFtp(client, _ftpSetting.Value.RootKPI);

            // upload a file
            foreach (string file in filesLoc)
            {
                string fileName = Path.GetFileName(file);
                client.UploadFile(@$"{destinationArchive}/{fileName}", $"{_ftpSetting.Value.RootKPI}{fileName}");
            }
            client.Disconnect();
            WatchDog.WatchLogger.Log($"FtpToBackUp Success....");
        }

        private void RemoveOldFtp(FtpClient client, string rootPath)
        {
            FtpListItem[] items = client.GetListing(rootPath);
            // get a recursive listing of the files & folders in a specific folder
            foreach (var item in items)
            {
                switch (item.Type)
                {

                    case FtpObjectType.Directory:

                        // Console.WriteLine("Directory!  " + item.FullName);
                        // Console.WriteLine("Modified date:  " + await client.GetModifiedTime(item.FullName));

                        break;

                    case FtpObjectType.File:

                        var modifileDate = client.GetModifiedTime(item.FullName);
                        DateTime now = DateTimeSystem.Utc(DateTime.UtcNow);

                        if ((DateTimeSystem.Utc(modifileDate) - now).TotalDays <= -7)
                        {
                            client.DeleteFile(item.FullName);
                        }
                        // Console.WriteLine("File!  " + item.FullName);
                        // Console.WriteLine("File size:  " + await client.GetFileSize(item.FullName));
                        // Console.WriteLine("Modified date:  " + await client.GetModifiedTime(item.FullName));
                        // Console.WriteLine("Chmod:  " + await client.GetChmod(item.FullName));

                        break;

                    case FtpObjectType.Link:
                        break;
                }
            }
        }

    }
}