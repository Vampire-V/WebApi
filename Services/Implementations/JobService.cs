using WebApi.Services.Interface;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebApi.Config;
using System.Net;
using System.IO.Compression;
using WebApi.Extensions;
using FluentFTP;
using MySql.Data.MySqlClient;
using Hangfire;

namespace WebApi.Services.Implementations
{
    public class JobService : IJobService
    {
        public JobService()
        {
        }

        public void FireAndForgetJob()
        {
            Console.WriteLine("Hello from a Fire and Forget job!");
        }

        public void ReccuringJob(string name, string cron)
        {
            RecurringJob.AddOrUpdate<IBackUpDBService>(name, b => b.BackupMysql(), cron, DateTimeSystem.TimeZone);
            Console.WriteLine("Hello from a Scheduled job!");
        }

        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }

        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }

        public void HaierKpiJob(string name, string cron)
        {
            RecurringJob.AddOrUpdate<IFileService>(name, f => f.BackupHaierKPI(), cron, DateTimeSystem.TimeZone);
            Console.WriteLine("Hello from a Scheduled job!");
        }
    }
}