using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Accounting.Entities;

namespace WebApi.Data.Accounting.EntityTypeConfig
{
    public class GrIrDetailViewConfig : IEntityTypeConfiguration<GrIrDetailView>
    {
        public void Configure(EntityTypeBuilder<GrIrDetailView> builder)
        {
            builder.HasNoKey();
            builder.Property(v => v.Assingment).HasColumnName("assignment");
            builder.Property(v => v.PurchasingDocument).HasColumnName("purchasing_document");
            builder.Property(v => v.VendorCode).HasColumnName("vendor_code");
            builder.HasOne<AccountingVendor>(v => v.Vendors).WithMany().HasForeignKey(c => c.VendorCode);
            builder.Property(v => v.Plant).HasColumnName("plant");
            builder.Property(v => v.PurchaseTypeDesc).HasColumnName("purchase_type_desc");
            builder.Property(v => v.GlAcct).HasColumnName("gl_acct");
            builder.Property(v => v.Reference).HasColumnName("reference");
            builder.Property(v => v.DocumentNo).HasColumnName("document_no");
            builder.Property(v => v.DocumentHeaderText).HasColumnName("document_header_text");
            builder.Property(v => v.BusinessArea).HasColumnName("business_area");
            builder.Property(v => v.DocumentType).HasColumnName("document_type");
            builder.Property(v => v.YearMonth).HasColumnName("year_month");
            builder.Property(v => v.PostingDate).HasColumnName("posting_date");
            builder.Property(v => v.DocumentDate).HasColumnName("document_date");
            builder.Property(v => v.DebitCredit).HasColumnName("debit_credit");
            builder.Property(v => v.Quantity).HasColumnName("quantity");
            builder.Property(v => v.AmountLc).HasColumnName("amount_lc");
            builder.Property(v => v.LocalCurrency).HasColumnName("local_currency");
            builder.Property(v => v.AmountDc).HasColumnName("amount_dc");
            builder.Property(v => v.DocumentCurrency).HasColumnName("document_currency");
            builder.Property(v => v.ClearingDocument).HasColumnName("clearing_document");
            builder.Property(v => v.ProfitCenter).HasColumnName("profit_center");
            builder.Property(v => v.OffsetAcct).HasColumnName("offset_acct");
            builder.Property(v => v.Text).HasColumnName("text");
            builder.Property(v => v.ObjectKey).HasColumnName("object_key");
            builder.Property(v => v.ReferenceKeyThree).HasColumnName("reference_key_three");
            builder.Property(v => v.DateDiff).HasColumnName("date_diff");
            builder.Property(v => v.Aging).HasColumnName("aging");
            builder.ToView("grir_detail_view");
        }
    }
}