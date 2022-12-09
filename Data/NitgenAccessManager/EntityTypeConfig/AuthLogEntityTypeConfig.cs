using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.NitgenAccessManager.Entities;

namespace WebApi.Data.NitgenAccessManager.EntityTypeConfig
{
    public class AuthLogEntityTypeConfig : IEntityTypeConfiguration<AuthLog>
    {
        public void Configure(EntityTypeBuilder<AuthLog> builder)
        {
            builder.HasKey(e => e.IndexKey);
            builder.Property(e => e.IndexKey).HasColumnName("IndexKey");
            builder.Property(e => e.UserIdIndex).HasColumnName("UserIDIndex");
            builder.Property(e => e.TransactionTime).HasColumnName("TransactionTime").HasColumnType("datetime");
            builder.Property(e => e.UserId).HasColumnName("UserID");
            builder.Property(e => e.TerminalId).HasColumnName("TerminalID");
            builder.Property(e => e.AuthType).HasColumnName("AuthType");
            builder.Property(e => e.AuthResult).HasColumnName("AuthResult");
            builder.Property(e => e.FunctionKey).HasColumnName("FunctionKey");
            builder.Property(e => e.ServerRecordTime).HasColumnName("ServerRecordTime").HasColumnType("datetime");
            builder.Property(e => e.Reserved).HasColumnName("Reserved");
            builder.Property(e => e.LogType).HasColumnName("LogType");
            builder.Property(e => e.TempValue).HasColumnName("TempValue");
            builder.Property(e => e.MinIndex).HasColumnName("MinIndex");
            builder.Property(e => e.PairUserId).HasColumnName("PairUserID");
            builder.Property(e => e.PairAuthType).HasColumnName("PairAuthType");
            builder.Property(e => e.PairAuthResult).HasColumnName("PairAuthResult");
            builder.ToTable("NGAC_AUTHLOG");
            // builder.ToTable("TAO_ngac_authlog_copy");
        }
    }
}