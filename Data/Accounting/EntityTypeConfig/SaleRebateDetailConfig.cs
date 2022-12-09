using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class SaleRebateDetailConfig : IEntityTypeConfiguration<SaleRebateDetail>
    {
        public void Configure(EntityTypeBuilder<SaleRebateDetail> builder)
        {
            builder.HasKey(key => new { key.Index, key.ProfSeg });
            builder.Property(d => d.Index).HasColumnName("id");
            builder.Property(d => d.ProfSeg).HasColumnName("prof_seg");
            builder.Property(d => d.Payer).HasColumnName("payer");
            builder.Property(d => d.BillDoc).HasColumnName("bill_doc");
            builder.Property(d => d.BillItem).HasColumnName("bill_item");
            builder.Property(d => d.Return).HasColumnName("is_return");
            builder.Property(d => d.BillQty).HasColumnName("bill_qty");
            builder.Property(d => d.BillUnit).HasColumnName("bill_unit");
            builder.Property(d => d.BillQtySKU).HasColumnName("sku_qty");
            builder.Property(d => d.NetValue).HasColumnName("net_value");
            builder.Property(d => d.RefDoc).HasColumnName("ref_doc");
            builder.Property(d => d.RefItem).HasColumnName("ref_item");
            builder.Property(d => d.SalesDoc).HasColumnName("sales_doc");
            builder.Property(d => d.SalesItem).HasColumnName("sales_item");
            builder.Property(d => d.Material).HasColumnName("material");
            builder.Property(d => d.ItemDescription).HasColumnName("item_description");
            builder.Property(d => d.MatlGroup).HasColumnName("mat_group");
            builder.Property(d => d.ItemCategory).HasColumnName("item_category");
            builder.Property(d => d.ShippingPoint).HasColumnName("shipping_point");
            builder.Property(d => d.Plant).HasColumnName("plant");
            builder.Property(d => d.AssignmentGroup).HasColumnName("assign_group");
            builder.Property(d => d.CreatedBy).HasColumnName("created_by");
            builder.Property(d => d.CreatedOn).HasColumnName("created_on");
            builder.Property(d => d.CreatedTime).HasColumnName("created_time");
            builder.Property(d => d.StorageLocation).HasColumnName("location");
            builder.Property(d => d.Cost).HasColumnName("cost");
            builder.Property(d => d.ProfitCenter).HasColumnName("profit_center");
            builder.Property(d => d.TaxAmount).HasColumnName("tax_amount");
            builder.Property(d => d.BillType).HasColumnName("bill_type");
            builder.Property(d => d.SaleOrg).HasColumnName("sales_org");
            builder.Property(d => d.BillingDate).HasColumnName("billing_date");
            builder.ToTable("t_sale_rebate_info");
        }
    }
}