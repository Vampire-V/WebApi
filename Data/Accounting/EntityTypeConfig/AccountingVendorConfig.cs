using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class AccountingVendorConfig : IEntityTypeConfiguration<AccountingVendor>
    {
        public void Configure(EntityTypeBuilder<AccountingVendor> builder)
        {
            
            builder.HasKey(r => new { r.VendorCode});
            builder.Property(r => r.VendorCode).HasColumnName("vendor_code");
            builder.Property(r => r.VendorName).HasColumnName("vendor_name");
            builder.ToTable("vendor");
        }
    }
}