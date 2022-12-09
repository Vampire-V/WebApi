namespace WebApi.Data.Accounting.Entities
{
    public class ApAgingDetail
    {
        public string? GlAcct { get; set; }
        public string? Gl { get; set; }
        public string? GlName { get; set; }
        public string? Year { get; set; }
        public string? Period { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? SpecialGl { get; set; }
        public string? DocumentHeaderText { get; set; }
        public string? DocumentType { get; set; }
        public string? Reference { get; set; }
        public string? DocumentNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? PayTerms { get; set; }
        public string? DayOne { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime? NetDueDate { get; set; }
        public Decimal? MonthDue { get; set; }
        public string? PbcType { get; set; }
        public string? Pbc { get; set; }
        public string? PackageType { get; set; }
        public Decimal? ExchangeRate { get; set; }
        public Decimal AmountDc { get; set; }
        public string? DocumentCurrency { get; set; }
        public Decimal RateBot { get; set; }
        public Decimal AmountLc { get; set; }
        public Decimal AmountAfterAdjRate { get; set; }
        public Decimal AmountAdjRate { get; set; }
        public string? LocalCurrency { get; set; }
        public string? Text { get; set; }
        public string? Assignment { get; set; }
        public string? ClearingDocument { get; set; }
        public DateTime? ClearingDate { get; set; }
        public string? UserName { get; set; }
        public string? AccountType { get; set; }
        public string? DebitCredit { get; set; }
        public string? Vat { get; set; }
        public string? SpecialGlAssignment { get; set; }
    }
}