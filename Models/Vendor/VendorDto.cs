namespace WebApi.Models.Vendor
{
    public class VendorDto
    {
        public string? TaxId { get; set; }
        public string VendorCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Fax { get; set; }
        public string? Tel { get; set; }
        public string? PND { get; set; }
        public string? TaxIDVendor1 { get; set; }
        public string? TaxIDVendor2 { get; set; }
        public string? TaxIDVendor3 { get; set; }
        public string? VATRegisNo { get; set; }
    }
}