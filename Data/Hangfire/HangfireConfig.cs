using Hangfire;
using Hangfire.SqlServer;

namespace WebApi.Data.Hangfire
{
    public static class HangfireConfig
    {
        public static void RegisterHangfire(this IServiceCollection services,IConfiguration configuration)
        {
            // var config = configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            // string connect = config.GetConnectionString("Hangfire");
            services.AddHangfireServer(options => options.Queues = new[] { "alpha", "beta", "default" })
                    .AddHangfire(x => x
                    .UseNLogLogProvider()
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    })
                );
        }
    }
}