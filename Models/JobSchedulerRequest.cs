using System.ComponentModel.DataAnnotations;
using WebApi.Data.Hangfire.Enum;

namespace WebApi.Models
{
    public class JobSchedulerRequest
    {
        [Required]
        public JobType Type { get; set; }
        [Required]
        public string CronFormat { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
    }
}