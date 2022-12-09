using WebApi.Services.Base;

namespace WebApi.Services.Interface{
    public interface IFileService : IScopedService
    {
        void BackupHaierKPI();
        void FtpToBackUp();
    }
}