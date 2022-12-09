using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class ApAgingPbcConfig : IEntityTypeConfiguration<ApAgingPbc>
    {
        public void Configure(EntityTypeBuilder<ApAgingPbc> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.VendorCode).HasColumnName("vendor_code");
            builder.Property(item => item.VendorName).HasColumnName("vendor_name");
            builder.Property(item => item.BusinessDescription).HasColumnName("business_description");
            builder.Property(item => item.AmountDc).HasColumnName("amount_dc");
            builder.Property(item => item.DocumentCurrency).HasColumnName("document_currency");
            builder.Property(item => item.AmountLc).HasColumnName("amount_lc");
            builder.Property(item => item.LocalCurrency).HasColumnName("local_currency");
            builder.Property(item => item.Range1).HasColumnName("range_1");
            builder.Property(item => item.Range2).HasColumnName("range_2");
            builder.Property(item => item.Range3).HasColumnName("range_3");
            builder.Property(item => item.Range4).HasColumnName("range_4");
            builder.Property(item => item.Range5).HasColumnName("range_5");
            builder.Property(item => item.Range6).HasColumnName("range_6");
            builder.Property(item => item.Range7).HasColumnName("range_7");
            builder.Property(item => item.Range8).HasColumnName("range_8");
            builder.Property(item => item.Range9).HasColumnName("range_9");
        }
    }
}