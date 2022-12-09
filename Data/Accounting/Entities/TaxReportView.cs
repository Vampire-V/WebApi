namespace WebApi.Data.Accounting.Entities
{
    public class TaxReportView
    {
        public Int64 AssignmentNumber { get; set; }
        public string? DocumentNo { get; set; }
        public string? InvoiceDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? TaxId { get; set; }
        public string? HeadOfficeId { get; set; }
        public string? BranchId { get; set; }
        public Decimal TaxBase { get; set; }
        public Decimal VatAmount { get; set; }
        public string? DebitCredit { get; set; }
        public string? Assignment { get; set; }
        public string? OffsetAcct { get; set; }
        public DateTime PostingDate { get; set; }
    }
}