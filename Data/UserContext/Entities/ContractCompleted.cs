namespace WebApi.Data.UserContext.Entities
{
    public class ContractCompleted
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public string AgreementType { get; set; } = null!;
        public string? AgreementSubType { get; set; }
        public string? ContractNo { get; set; }
        public string Counterparty { get; set; } = null!;
        public int? Owner { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime OverDue { get; set; }
        public DateTime CreatedAt { get;}
        public DateTime UpdatedAt { get;}
        // public List<UserRefreshToken>? UserRefreshToken { get; set; }
    }
}