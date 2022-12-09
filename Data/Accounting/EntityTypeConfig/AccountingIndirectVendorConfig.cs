using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class AccountingIndirectVendorConfig  : IEntityTypeConfiguration<AccountingIndirectVendor>
    {
        public void Configure(EntityTypeBuilder<AccountingIndirectVendor> builder)
        {
            builder.HasKey(v => v.VendorCode);
            builder.Property(v => v.VendorCode).HasColumnName("vendor_code");
            builder.Property(v => v.VendorName).HasColumnName("vendor_name");
            builder.Property(v => v.TaxId).HasColumnName("tax_id");
            builder.Property(v => v.HeadOfficeId).HasColumnName("head_office_id");
            builder.Property(v => v.BranchId).HasColumnName("branch_id");
            builder.ToTable("indirect_vendor");
        }
    }
}