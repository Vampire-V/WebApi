namespace WebApi.Models.ChequeBNP
{
    public class Header
    {
        public string? RecordType {get; set;}
        public string? CompanyId {get; set;}
        public string? CompanyTaxId {get; set;}
        public string? CompanyAccount {get; set;}
        public string? CustomerBatchReference {get; set;}
        public string? BatchBroadcastMessage {get; set;}
        public string? FileDate {get; set;}
        public string? FileTimestamp {get; set;}
        public List<DocumentDetail>? Documents { get; set; }
    }
}