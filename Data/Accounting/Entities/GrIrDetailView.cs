namespace WebApi.Data.Accounting.Entities
{
    public class GrIrDetailView
    {
        public string? Assingment { get; set; }
        public string? PurchasingDocument { get; set; }
        public string? VendorCode { get; set; }
        public virtual AccountingVendor? Vendors { get; set; }
        public string? Plant { get; set; }
        public string? PurchaseTypeDesc { get; set; }
        public string? GlAcct { get; set; }
        public string? Reference { get; set; }
        public string? DocumentNo { get; set; }
        public string? DocumentHeaderText { get; set; }
        public string? BusinessArea { get; set; }
        public string? DocumentType { get; set; }
        public string? YearMonth { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? DebitCredit { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal AmountLc { get; set; }
        public string? LocalCurrency { get; set; }
        public Decimal AmountDc { get; set; }
        public string? DocumentCurrency { get; set; }
        public string? ClearingDocument { get; set; }
        public string? ProfitCenter { get; set; }
        public string? OffsetAcct { get; set; }
        public string? Text { get; set; }
        public string? ObjectKey { get; set; }
        public string? ReferenceKeyThree { get; set; }
        public int DateDiff { get; set; }
        public string? Aging { get; set; }
    }
}