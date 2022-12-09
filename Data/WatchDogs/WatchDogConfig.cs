using WatchDog;
using WatchDog.src.Enums;

namespace WebApi.Data.WatchDogs
{
    public static class WatchDogConfig
    {
        public static void RegisterWatchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(options =>
            {
                options.IsAutoClear = false;
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Quarterly;
                options.SetExternalDbConnString = configuration.GetConnectionString("WatchDog");
                options.SqlDriverOption = WatchDogSqlDriverEnum.MySql;
            });
        }
    }
}