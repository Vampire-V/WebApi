using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.CosmoIm9773.Entities;

namespace WebApi.Data.CosmoIm9773.EntityTypeConfig
{
    public class FGProductionOrderConfig : IEntityTypeConfiguration<FGProductionOrder>
    {
        public void Configure(EntityTypeBuilder<FGProductionOrder> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.Plant).HasColumnName("Plant");
            builder.Property(item => item.OrderNo).HasColumnName("Order No.");
            builder.Property(item => item.StartDate).HasColumnName("Start Date");
            builder.Property(item => item.Material).HasColumnName("Material");
            builder.Property(item => item.Description).HasColumnName("Description");
            builder.Property(item => item.RequireQty).HasColumnName("Require Qty");
            builder.Property(item => item.ProductQty).HasColumnName("Product Qty");
            builder.Property(item => item.DifferentQty).HasColumnName("Different Qty");
            builder.Property(item => item.OrderType).HasColumnName("Order Type");
            builder.Property(item => item.SendSapQty).HasColumnName("Send SAP Qty");
            builder.Property(item => item.Unit).HasColumnName("Unit");
            builder.Property(item => item.CreateDate).HasColumnName("Create Date");
            builder.Property(item => item.ProductionVersion).HasColumnName("Production Version");
            builder.Property(item => item.LineCode).HasColumnName("Line Code");
        }
    }
}