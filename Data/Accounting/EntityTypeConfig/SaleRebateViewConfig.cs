using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{ 
    public class SaleRebateViewConfig : IEntityTypeConfiguration<SaleRebateView>
    {
        public void Configure(EntityTypeBuilder<SaleRebateView> builder)
        {
            builder.HasNoKey();
            builder.Property(s => s.Month).HasColumnName("Month");
            builder.Property(s => s.RFQty).HasColumnName("RFQty");
            builder.Property(s => s.ACQty).HasColumnName("ACQty");
            builder.Property(s => s.TotalQty).HasColumnName("TotalQty");
            builder.Property(s => s.RFAmt).HasColumnName("RFAmt");
            builder.Property(s => s.ACAmt).HasColumnName("ACAmt");
            builder.Property(s => s.TotalAmt).HasColumnName("TotalAmt");
            builder.Property(s => s.RFPromotionPercent).HasColumnName("RFPromotionPercent");
            builder.Property(s => s.RFPromotionAmt).HasColumnName("RFPromotionAmt");
            builder.Property(s => s.ACPromotionPercent).HasColumnName("ACPromotionPercent");
            builder.Property(s => s.ACPromotionAmt).HasColumnName("ACPromotionAmt");
            builder.Property(s => s.TotalPromotionAmt).HasColumnName("TotalPromotionAmt");
            builder.Property(s => s.RFRoyaltyPercent).HasColumnName("RFRoyaltyPercent");
            builder.Property(s => s.RFRoyaltyAmt).HasColumnName("RFRoyaltyAmt");
            builder.ToView("sale_rebate_view");
        }
    
    }
}