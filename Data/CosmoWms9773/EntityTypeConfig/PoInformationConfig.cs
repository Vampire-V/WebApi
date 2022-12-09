using Microsoft.EntityFrameworkCore;
using WebApi.Data.CosmoIm9773.Entities;

namespace WebApi.Data.CosmoWms9773.EntityTypeConfig
{
    public class PoInformationConfig : IEntityTypeConfiguration<PoInformation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PoInformation> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.PoNo).HasColumnName("PO_NO");
            builder.Property(item => item.Item).HasColumnName("ITEM");
            builder.Property(item => item.PoType).HasColumnName("PO_TYPE");
            builder.Property(item => item.TypeDesc).HasColumnName("TYPE_DESC");
            builder.Property(item => item.Material).HasColumnName("MATERIAL");
            builder.Property(item => item.Material).HasColumnName("DESCRIPTION");
            builder.Property(item => item.Unit).HasColumnName("UNIT");
            builder.Property(item => item.RequestQty).HasColumnName("REQUEST_QTY");
            builder.Property(item => item.ReceiptQty).HasColumnName("RECEIPT_QTY");
            builder.Property(item => item.DifferenceQty).HasColumnName("DIFFERENCE_QTY");
            builder.Property(item => item.CreateQty).HasColumnName("CREATE_QTY");
            builder.Property(item => item.ReceiptState).HasColumnName("RECEIPT_STATE");
            builder.Property(item => item.DeliveryDate).HasColumnName("DELIVERY_DATE");
            builder.Property(item => item.Plant).HasColumnName("PLANT");
            builder.Property(item => item.SupplierId).HasColumnName("SUPPLIER_ID");
            builder.Property(item => item.SupplierName).HasColumnName("SUPPLIER_NAME");
            builder.Property(item => item.Price).HasColumnName("PRICE");
            builder.Property(item => item.Currency).HasColumnName("CURRENCY");
            builder.Property(item => item.ItemType).HasColumnName("ITEM_TYPE");
            builder.Property(item => item.CreateDate).HasColumnName("CREATE_DATE");
            builder.Property(item => item.LastUpdate).HasColumnName("LAST_UPDATE");
        }
    }
}