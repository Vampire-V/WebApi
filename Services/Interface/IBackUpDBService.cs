using WebApi.Services.Base;

namespace WebApi.Services.Interface{
    public interface IBackUpDBService : IScopedService
    {
        void BackupMysql();
    }
}