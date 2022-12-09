using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class GrIrSummaryViewConfig : IEntityTypeConfiguration<GrIrSummaryView>
    {
        public void Configure(EntityTypeBuilder<GrIrSummaryView> builder)
        {
            builder.HasKey(key => new { key.Plant, key.PurchaseType });
            builder.Property(v => v.Plant).HasColumnName("plant");
            builder.Property(v => v.PurchaseType).HasColumnName("purchase_type");
            builder.Property(v => v.PurchaseTypeDesc).HasColumnName("purchase_type_desc");
            builder.Property(v => v.TotalBalance).HasColumnName("total_balance");
            builder.Property(v => v.Range1).HasColumnName("range_1");
            builder.Property(v => v.Range2).HasColumnName("range_2");
            builder.Property(v => v.Range3).HasColumnName("range_3");
            builder.Property(v => v.Range4).HasColumnName("range_4");
            builder.Property(v => v.Range5).HasColumnName("range_5");
            builder.Property(v => v.Range6).HasColumnName("range_6");

            builder.ToView("grir_summary_view");
        }
    }
}