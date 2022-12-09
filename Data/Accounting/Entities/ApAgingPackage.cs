namespace WebApi.Data.Accounting.Entities
{

    public class ApAgingPackage
    {
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public Decimal TotalBalance { get; set; }
        public string? BusinessDescription { get; set; }
        public Decimal Range1 { get; set; }
        public Decimal Range2 { get; set; }
        public Decimal Range3 { get; set; }
        public Decimal Range4 { get; set; }
        public Decimal Range5 { get; set; }
    }
}