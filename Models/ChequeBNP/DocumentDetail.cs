namespace WebApi.Models.ChequeBNP
{
    public class DocumentDetail
    {
        public string? RecordType {get; set;}
        public string? CompanyId {get; set;}
        public string? PoNumber {get; set;}
        public string? CreditSequenceNumber {get; set;}
        public string? ProductCode {get; set;}
        public string? BeneficiaryAccountNumber {get; set;}
        public string? ValueDate {get; set;}
        public string? ValueTime {get; set;}
        public string? CreditCurrency {get; set;}
        public string DocumentNumber {get; set;} = null!;
        public string? PreAdviceDate {get; set;}
        public string? DeliveryMethod { get; set; }
        public string? DispatchTo { get; set; }
        public string? ChequeDepositRequired { get; set; }
        public string? CopyIdCardPresent { get; set; }
        public string? WHTPresent { get; set; }
        public string? InvoiceDetailsPresent { get; set; }
        public string? VATPresen { get; set; }
        public string? ReceiptPresent { get; set; }
        public string? CreditAdviceRequired { get; set; }
        public string? ChequeDrawnOnLocation { get; set; }
        public string? DispatchToCode { get; set; }
        public string? WHTFormType { get; set; }
        public string? WHTSerialNo {get;set;}
        public string? WHTBookNo { get; set; }
        public string? WHTRunningNo { get; set; }
        public string? BahtNetPaymentTypeCode { get; set; }
        public string? BOTServiceTypeOfPayment { get; set; }
        public Int32 NoOfWHTDetails { get; set; }
        public decimal TotalWHTAmount { get; set; }
        public Int32 NoOfInvoiceDetails { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public string? PayeeChargeCode { get; set; }
        public decimal PaymentNetamount  { get; set; } //(Credit amount)
        public string? WHTPayType { get; set; }
        public string? WHTRemark { get; set; }
        public string? WHTDeductDate { get; set; }
        public string? ReceivingBankCode { get; set; }
        public string? ReceivingBranchCode { get; set; }
        public string? WHTSignatory { get; set; }
        public string? ServiceCode { get; set; }
        public string? BeneficiaryCode { get; set; }
        public string? PayeeIdCard { get; set; }
        public string? PayeeName { get; set; }
        public string? PayeeAddress1 { get; set; }
        public string? PayeeAddress2 { get; set; }
        public string? PayeeAddress3 { get; set; }
        public string? PayeeAddress4 { get; set; }
        public string? DispatchAddress1 { get; set; }
        public string? DispatchAddress2 { get; set; }
        public string? DispatchAddress3 { get; set; }
        public string? DispatchAddress4 { get; set; }
        public string? PayeeTaxID { get; set; }
        public string? PayeeFaxNumber { get; set; }
        public string? PayeeMobilePhoneNumber { get; set; }
        public string? PayeeEmailAddress { get; set; }
        public string? CollectorIdType { get; set; }
        public string? CollectorId { get; set; }
        public string? RequiredReturnDocuments { get; set; }
        public string? OthersRequiredReturnDocuments { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
        public List<InvoiceDetail>? Invoices { get; set; }
    }
}