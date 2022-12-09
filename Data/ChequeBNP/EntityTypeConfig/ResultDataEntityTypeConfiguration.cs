using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.ChequeBNP.Entities;

namespace WebApi.Data.ChequeBNP.EntityTypeConfig
{
    public class ResultDataEntityTypeConfiguration : IEntityTypeConfiguration<ResultData>
    {
        public void Configure(EntityTypeBuilder<ResultData> builder)
        {
            builder.HasKey(v => v.Id).HasName("IDData");
            builder.Property(v => v.Id).IsRequired().HasColumnName("IDData");
            builder.Property(v => v.DocumentNo).IsRequired().HasColumnName("DocumentNo");
            builder.Property(v => v.Result).IsRequired().HasColumnName("ResultData");
            builder.ToTable("tbResultData");
        }
    }
}