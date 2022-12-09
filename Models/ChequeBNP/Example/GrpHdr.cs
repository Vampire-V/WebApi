namespace WebApi.Models.ChequeBNP.Example
{
    public class GrpHdr
    {
        public string MsgId { get; set; } = string.Empty;
        public string CreDtTm { get; set; } = string.Empty;
        public string NbOfTxs { get; set; } = string.Empty;
        public decimal CtrlSum { get; set; }
        public InitgPty InitgPty {get;set;} = null!;
    }
}