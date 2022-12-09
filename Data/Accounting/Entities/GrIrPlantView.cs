namespace WebApi.Data.Accounting.Entities
{
    public class GrIrPlantView
    {
        public string? Plant { get; set; }
        public string? PurchaseType { get; set; }
        public string? PurchaseTypeDesc { get; set; }
        public string? PurchasingDocument { get; set; }
        public string? VendorCode { get; set; }
        public virtual AccountingVendor? Vendors { get; set; }
        public Decimal? TotalBalance { get; set; }
        public Decimal? Range1 { get; set; }
        public Decimal? Range2 { get; set; }
        public Decimal? Range3 { get; set; }
        public Decimal? Range4 { get; set; }
        public Decimal? Range5 { get; set; }
        public Decimal? Range6 { get; set; }
    }
}