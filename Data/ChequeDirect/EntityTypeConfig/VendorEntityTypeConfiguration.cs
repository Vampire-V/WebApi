using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.ChequeDirect.Entities;

namespace WebApi.Data.ChequeDirect.EntityTypeConfig
{
    public class VendorEntityTypeConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(v => v.VendorCode).HasName("VendorCode");
            builder.Property(v => v.TaxId).IsRequired().HasColumnName("Tax ID");
            builder.Property(v => v.VendorCode).IsRequired();
            builder.Property(v => v.Name).IsRequired().HasColumnName("Name");
            builder.Property(v => v.Address).HasColumnName("Address");
            builder.Property(v => v.Fax).HasColumnName("Fax");
            builder.Property(v => v.Tel).HasColumnName("Tel");
            builder.Property(v => v.PND).HasColumnName("PND");
            builder.Property(v => v.TaxIdVendor1).HasColumnName("TaxIDVendor1");
            builder.Property(v => v.TaxIdVendor2).HasColumnName("TaxIDVendor2");
            builder.Property(v => v.TaxIdVendor3).HasColumnName("TaxIDVendor3");
            builder.Property(v => v.VATRegisNo).HasColumnName("VATRegisNo");
            builder.ToTable("Vendor").HasQueryFilter(v => !v.VendorCode.Contains("V"));
        }
    }
}