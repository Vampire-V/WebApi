using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class ApAgingPackageConfig : IEntityTypeConfiguration<ApAgingPackage>
    {
        public void Configure(EntityTypeBuilder<ApAgingPackage> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.VendorCode).HasColumnName("vendor_code");
            builder.Property(item => item.VendorName).HasColumnName("vendor_name");
            builder.Property(item => item.TotalBalance).HasColumnName("total_balance");
            builder.Property(item => item.BusinessDescription).HasColumnName("business_description");
            builder.Property(item => item.Range1).HasColumnName("range_1");
            builder.Property(item => item.Range2).HasColumnName("range_2");
            builder.Property(item => item.Range3).HasColumnName("range_3");
            builder.Property(item => item.Range4).HasColumnName("range_4");
            builder.Property(item => item.Range5).HasColumnName("range_5");
        }
    }
}