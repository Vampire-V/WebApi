namespace WebApi.Models.ChequeBNP.Example
{
    public class CdtTrfTxInf
    {
        public PmtId PmtId { get; set; } = null!;
        public Amt Amt { get; set; } = null!;
        public CdtrAgt CdtrAgt { get; set; } = null!;
        public Cdtr Cdtr { get; set; } = null!;
        public CdtrAcct CdtrAcct { get; set; } = null!;
        public Purp Purp { get; set; } = null!;
        public RltdRmtInf RltdRmtInf { get; set; } = null!;
        // public RmtInf[] RmtInf { get; set; } = null!;
    }
}