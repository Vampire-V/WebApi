using Hangfire.Annotations;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IJobService : IScopedService
    {
        void FireAndForgetJob();
        void ReccuringJob(string name, string cron);
        void DelayedJob();
        void ContinuationJob();
        void HaierKpiJob(string name, string cron);
    }
}