namespace WebApi.Models.Vendor
{
    public class VendorView
    {
        public string TaxId { get; set; } = string.Empty;
        public string VendorCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Fax { get; set; }
        public string? Tel { get; set; }
        public string? PND { get; set; }
        public string? TaxIdVendor1 { get; set; }
        public string? TaxIdVendor2 { get; set; }
        public string? TaxIdVendor3 { get; set; }
        public string? VATRegisNo { get; set; }
    }
}