namespace WebApi.Models.GRGI
{
    public class GrGiPlanImport
    {
        public string Plant { get; set; } = string.Empty;
        public string Mrp { get; set; } = string.Empty;
        public string PlanDate { get; set; } = string.Empty;
        public string PlanType { get; set; } = string.Empty;
        public decimal MonthTarget { get; set; }
        public decimal DayTarget { get; set; }
    }
}