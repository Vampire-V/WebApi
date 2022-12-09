namespace WebApi.Models.ChequeBNP.Example
{
    public class FinInstnId
    {
        public string BIC { get; set; } = string.Empty;
        public string Nm { get; set; } = string.Empty;
        public PstlAdr PstlAdr { get; set; } = null!;
    }
}