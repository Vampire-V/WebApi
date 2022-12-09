using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.CosmoIm9773.Entities;

namespace WebApi.Data.CosmoIm9773.EntityTypeConfig
{
    public class SFGOfflineConfig : IEntityTypeConfiguration<SFGOffline>
    {
        public void Configure(EntityTypeBuilder<SFGOffline> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.Barcode).HasColumnName("barcode");
            builder.Property(item => item.Material).HasColumnName("material");
            builder.Property(item => item.Description).HasColumnName("description");
            builder.Property(item => item.OrderNo).HasColumnName("order_no");
            builder.Property(item => item.Qty).HasColumnName("qty");
            builder.Property(item => item.Unit).HasColumnName("unit");
            builder.Property(item => item.ProdVersion).HasColumnName("prod_version");
            builder.Property(item => item.OfflineBy).HasColumnName("offline_by");
            builder.Property(item => item.OfflineDate).HasColumnName("offline_date");
            builder.Property(item => item.Plant).HasColumnName("plant");
            builder.Property(item => item.LineCode).HasColumnName("line_code");
        }
    }
}