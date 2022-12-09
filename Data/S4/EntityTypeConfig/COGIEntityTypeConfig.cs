using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.S4.Entities;

namespace WebApi.Data.S4.EntityTypeConfig
{
    public class COGIEntityTypeConfig  : IEntityTypeConfiguration<COGI>
    {
        public void Configure(EntityTypeBuilder<COGI> builder)
        {
            builder.Property(c => c.Plant).HasColumnName("PLANT");
            builder.Property(c => c.ReservNo).HasColumnName("RESERV_NO");
            builder.Property(c => c.OrderNo).HasColumnName("ORDER_NO");
            builder.Property(c => c.Material).HasColumnName("MATERIAL");
            builder.Property(c => c.Location).HasColumnName("LOCATION");
            builder.Property(c => c.Quantity).HasColumnName("QUANTITY");
            builder.Property(c => c.Unit).HasColumnName("UNIT");
            builder.Property(c => c.MovementType).HasColumnName("MOVEMENT_TYPE");
            builder.Property(c => c.MessageNo).HasColumnName("MESSAGE_NO");
            builder.Property(c => c.MessageType).HasColumnName("MESSAGE_TYPE");
            builder.Property(c => c.ErrorMessage).HasColumnName("ERROR_MESSAGE");
            builder.Property(c => c.Mrp).HasColumnName("MRP");
            builder.Property(c => c.PostingDate).HasColumnName("POSTING_DATE");
            builder.Property(c => c.RowId).HasColumnName("ROW_ID");
            builder.Property(c => c.TimeStamp).HasColumnName("TIME_STAMP");
            builder.ToTable("AFFW").HasNoKey();
        }
    }
}