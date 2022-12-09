namespace WebApi.Models.ChequeBNP.Example
{
    public class PmtInf
    {
        public string PmtInfId { get; set; } = string.Empty;
        public string PmtMtd { get; set; } = string.Empty;
        public bool BtchBookg { get; set; }
        public string NbOfTxs { get; set; } = string.Empty;
        public decimal CtrlSum { get; set; }
        public PmtTpInf PmtTpInf { get; set; } = null!;
        public string ReqdExctnDt { get; set; } = string.Empty;
        public Dbtr Dbtr { get; set; } = null!;
        public DbtrAcct DbtrAcct { get; set; } = null!;
        public DbtrAgt DbtrAgt { get; set; } = null!;
        public string ChrgBr { get; set; } = string.Empty;
        public List<CdtTrfTxInf> CdtTrfTxInf { get; set; } = null!;
    }
}