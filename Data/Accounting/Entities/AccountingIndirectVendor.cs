namespace WebApi.Data.Accounting.Entities
{
    public class AccountingIndirectVendor
    {
        public string VendorCode { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public string HeadOfficeId { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;

        // public virtual ICollection<TaxReportBSEG>? TaxReports { get; set; }
    }
}