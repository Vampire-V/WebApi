namespace WebApi.Data.Accounting.Entities
{
    public class ApAgingPbc
    {
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? BusinessDescription { get; set; }
        public Decimal AmountDc { get; set; }
        public string? DocumentCurrency { get; set; }
        public Decimal AmountLc { get; set; }
        public string? LocalCurrency { get; set; }
        public Decimal Range1 { get; set; }
        public Decimal Range2 { get; set; }
        public Decimal Range3 { get; set; }
        public Decimal Range4 { get; set; }
        public Decimal Range5 { get; set; }
        public Decimal Range6 { get; set; }
        public Decimal Range7 { get; set; }
        public Decimal Range8 { get; set; }
        public Decimal Range9 { get; set; }
    }
}