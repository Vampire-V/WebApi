using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class TaxReportViewConfig : IEntityTypeConfiguration<TaxReportView>
    {
        public void Configure(EntityTypeBuilder<TaxReportView> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.AssignmentNumber).HasColumnName("assignment_number");
            builder.Property(item => item.DocumentNo).HasColumnName("document_no");
            builder.Property(item => item.InvoiceDate).HasColumnName("invoice_date");
            builder.Property(item => item.InvoiceNo).HasColumnName("invoice_no");
            builder.Property(item => item.VendorCode).HasColumnName("vendor_code");
            builder.Property(item => item.VendorName).HasColumnName("vendor_name");
            builder.Property(item => item.TaxId).HasColumnName("tax_id");
            builder.Property(item => item.HeadOfficeId).HasColumnName("head_office_id");
            builder.Property(item => item.BranchId).HasColumnName("branch_id");
            builder.Property(item => item.TaxBase).HasColumnName("tax_base");
            builder.Property(item => item.VatAmount).HasColumnName("vat_amount");
            builder.Property(item => item.DebitCredit).HasColumnName("debit_credit");
            builder.Property(item => item.Assignment).HasColumnName("assignment");
            builder.Property(item => item.OffsetAcct).HasColumnName("offset_acct");
            builder.Property(item => item.PostingDate).HasColumnName("posting_date");
            builder.ToView("tax_report_view");
        }
    }
}