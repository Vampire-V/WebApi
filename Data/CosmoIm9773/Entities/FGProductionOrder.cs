namespace WebApi.Data.CosmoIm9773.Entities
{
    public class FGProductionOrder
    {
        public string? Plant { get; set; }
        public string? OrderNo { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; }
        public int? RequireQty { get; set; }
        public int? ProductQty { get; set; }
        public int? DifferentQty { get; set; }
        public string? OrderType { get; set; }
        public int? SendSapQty { get; set; }
        public string? Unit { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ProductionVersion { get; set; }
        public string? LineCode { get; set; }
    }
}