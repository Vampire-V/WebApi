using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class ApAgingDetailConfig : IEntityTypeConfiguration<ApAgingDetail>
    {
        public void Configure(EntityTypeBuilder<ApAgingDetail> builder)
        {
            builder.HasNoKey();
            builder.Property(item => item.GlAcct).HasColumnName("gl_acct");
            builder.Property(item => item.Gl).HasColumnName("gl");
            builder.Property(item => item.GlName).HasColumnName("gl_name");
            builder.Property(item => item.Year).HasColumnName("year");
            builder.Property(item => item.Period).HasColumnName("period");
            builder.Property(item => item.VendorCode).HasColumnName("vendor_code");
            builder.Property(item => item.VendorName).HasColumnName("vendor_name");
            builder.Property(item => item.SpecialGl).HasColumnName("special_gl");
            builder.Property(item => item.DocumentHeaderText).HasColumnName("document_header_text");
            builder.Property(item => item.DocumentType).HasColumnName("document_type");
            builder.Property(item => item.Reference).HasColumnName("reference");
            builder.Property(item => item.DocumentNo).HasColumnName("document_no");
            builder.Property(item => item.DocumentDate).HasColumnName("document_date");
            builder.Property(item => item.PayTerms).HasColumnName("pay_terms");
            builder.Property(item => item.DayOne).HasColumnName("day_one");
            builder.Property(item => item.PostingDate).HasColumnName("posting_date");
            builder.Property(item => item.NetDueDate).HasColumnName("net_due_date");
            builder.Property(item => item.MonthDue).HasColumnName("month_due").HasColumnType("decimal(18,2)"); // Test
            builder.Property(item => item.PbcType).HasColumnName("pbc_type");
            builder.Property(item => item.Pbc).HasColumnName("pbc");
            builder.Property(item => item.PackageType).HasColumnName("package_type");
            builder.Property(item => item.ExchangeRate).HasColumnName("exchange_rate");
            builder.Property(item => item.AmountDc).HasColumnName("amount_dc");
            builder.Property(item => item.DocumentCurrency).HasColumnName("document_currency");
            builder.Property(item => item.RateBot).HasColumnName("rate_bot");
            builder.Property(item => item.AmountLc).HasColumnName("amount_lc");
            builder.Property(item => item.AmountAfterAdjRate).HasColumnName("amount_after_adj_rate");
            builder.Property(item => item.AmountAdjRate).HasColumnName("amount_adj_rate");
            builder.Property(item => item.LocalCurrency).HasColumnName("local_currency");
            builder.Property(item => item.Text).HasColumnName("text");
            builder.Property(item => item.Assignment).HasColumnName("assignment");
            builder.Property(item => item.ClearingDocument).HasColumnName("clearing_document");
            builder.Property(item => item.ClearingDate).HasColumnName("clearing_date");
            builder.Property(item => item.UserName).HasColumnName("user_name");
            builder.Property(item => item.AccountType).HasColumnName("account_type");
            builder.Property(item => item.DebitCredit).HasColumnName("debit_credit");
            builder.Property(item => item.Vat).HasColumnName("vat");
            builder.Property(item => item.SpecialGlAssignment).HasColumnName("special_gl_assignment");
        }
    }
}