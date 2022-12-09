using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.CosmoIm9773.Entities;

namespace WebApi.Data.CosmoIm9773.EntityTypeConfig
{
    public class OfflineSummarizeConfig : IEntityTypeConfiguration<OfflineSummarize>
    {
        public void Configure(EntityTypeBuilder<OfflineSummarize> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.OrderNo).HasColumnName("order_no");
            builder.Property(item => item.Material).HasColumnName("material");
            builder.Property(item => item.Description).HasColumnName("description");
            builder.Property(item => item.Quantity).HasColumnName("quantity");
            builder.Property(item => item.Unit).HasColumnName("unit");
            builder.Property(item => item.OfflineDate).HasColumnName("offline_date");
            builder.Property(item => item.SapFlag).HasColumnName("sap_flag");
            builder.Property(item => item.SapMessage).HasColumnName("sap_message");
            builder.Property(item => item.SapUpTime).HasColumnName("sap_up_time");
            builder.Property(item => item.SapUpNum).HasColumnName("sap_up_num");
            builder.Property(item => item.Plant).HasColumnName("plant");
        }
    }
}