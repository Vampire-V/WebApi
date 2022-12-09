namespace WebApi.Data.CosmoIm9773.Entities
{
    public class OfflineSummarize
    {
        public string? OrderNo { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public DateTime? OfflineDate { get; set; }
        public string? SapFlag { get; set; }
        public string? SapMessage { get; set; }
        public DateTime? SapUpTime { get; set; }
        public int? SapUpNum { get; set; }
        public string? Plant { get; set; }
    }
}