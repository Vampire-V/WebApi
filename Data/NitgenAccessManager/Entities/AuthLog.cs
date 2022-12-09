
using WebApi.Extensions;

namespace WebApi.Data.NitgenAccessManager.Entities
{
    // SQL Server 2005
    public class AuthLog
    {
        public long IndexKey { get; set; }
        public long UserIdIndex { get; set; }
        public DateTime TransactionTime { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TerminalId { get; set; } = 13;
        public int AuthType { get; set; } = 128;
        public int AuthResult { get; set; }// = 0;
        public int FunctionKey { get; set; }// = 0;
        public DateTime ServerRecordTime { get; set; } = DateTimeSystem.Utc(DateTime.UtcNow);
        public int Reserved { get; set; }// = 0;
        public int LogType { get; set; } = 1;
        public int TempValue { get; set; }// = 0;
        public int MinIndex { get; set; }// = 0;
        public string? PairUserId { get; set; }
        public int? PairAuthType { get; set; } = null;
        public int? PairAuthResult { get; set; } = null;
    }
}