namespace WebApi.Models.AccountingIndirectVendor
{
    public class IndirectVendorView
    {
        public string VendorCode { get; set; } = string.Empty;
        public string? VendorName { get; set; }
        public string? TaxId { get; set; }
        public string? HeadOfficeId { get; set; }
        public string? BranchId { get; set; }
    }
}