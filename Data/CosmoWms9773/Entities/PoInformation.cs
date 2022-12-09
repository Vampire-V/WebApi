namespace WebApi.Data.CosmoIm9773.Entities
{
    public class PoInformation
    {
        public string? PoNo { get; set; }
        public string? Item { get; set; }
        public string? PoType { get; set; }
        public string? TypeDesc { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public Decimal? RequestQty { get; set; }
        public Decimal? ReceiptQty { get; set; }
        public Decimal? DifferenceQty { get; set; }
        public Decimal? CreateQty { get; set; }
        public string? ReceiptState { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Plant { get; set; }
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public Decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? ItemType { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}