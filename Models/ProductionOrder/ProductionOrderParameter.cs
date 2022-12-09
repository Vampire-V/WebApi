namespace WebApi.Models.ProductionOrder
{
    public class ProductionOrderParameter
    {
        public string? OrderNo { get; set; }
        public string? LineCode { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; }
    }
}