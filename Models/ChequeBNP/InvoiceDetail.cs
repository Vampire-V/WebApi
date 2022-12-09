namespace WebApi.Models.ChequeBNP
{
    public class InvoiceDetail
    {
        public string? RecordIdentifier {get; set;}
        public string? InvoiceNumber {get; set;}
        public string? PoNumber {get; set;}
        public string? InvoiceDate {get; set;}
        public string? IvPoIvDate { get{return $"{InvoiceNumber}_{PoNumber}_{InvoiceDate}";} }
        public decimal Amount {get; set;}
        public string? Description {get; set;}
        public decimal VatAmount {get; set;}
        public string DocumentNumber {get; set;} = null!;
        
    }
}