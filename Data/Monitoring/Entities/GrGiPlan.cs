using System.Globalization;

namespace WebApi.Data.Monitoring.Entities
{
    public class GrGiPlan
    {
        public int Id { get; set; }
        public string Plant { get; set; } = string.Empty;
        public string Mrp { get; set; } = string.Empty;
        public string PlanDate { get; set; } = string.Empty;
        public string PlanType { get; set; } = string.Empty;
        public decimal MonthTarget { get; set; }
        public decimal DayTarget { get; set; }

        // public List<UserRefreshToken>? UserRefreshToken { get; set; }
    }
}