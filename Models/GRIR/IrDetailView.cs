namespace WebApi.Models.GRIR
{
    public class IrDetailView
    {
        public string? KeyIndex { get; set; }
        public string? Assignment { get; set; }
        public string? CompanyCode { get; set; }
        public string? DocumentNo { get; set; }
        public string? Year { get; set; }
        public string? LineItem { get; set; }
        public string? DebitCredit { get; set; }
        public string? TaxCode { get; set; }
        public decimal IrAmountInLc { get; set; }
        public string? LocalCurrency { get; set; }
        public decimal IrAmountInDc { get; set; }
        public string? DocumentCurrency { get; set; }
        public string? GlTransactionType { get; set; }
        public string? GlAcct { get; set; }
        public string? Material { get; set; }
        public string? Plant { get; set; }
        public decimal IrQty { get; set; }
        public string? Unit { get; set; }
        public string? PurchasingDocument { get; set; }
        public string? Item { get; set; }
        public string? ValueString { get; set; }
        public string? ProfitCenter { get; set; }
        public string? ReferenceKeyThree { get; set; }
        public string? ObjectKey { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? DocumentType { get; set; }
        public string? OffsetAcct { get; set; }
        public string? OffsetAcctType { get; set; }
        public string? OffsetAcctGl { get; set; }
        public string? ClearingDocument { get; set; }
    }
}