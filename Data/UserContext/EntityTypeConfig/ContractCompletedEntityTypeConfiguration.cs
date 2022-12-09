using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Data.UserContext.EntityTypeConfig
{
    public class ContractCompletedEntityTypeConfiguration : IEntityTypeConfiguration<ContractCompleted>
    {
        public void Configure(EntityTypeBuilder<ContractCompleted> builder)
        {
            builder.HasKey(u => u.Id).HasName("id");
            builder.Property(u => u.ContractId).HasColumnName("contract_id");
            builder.Property(u => u.AgreementType).IsRequired().HasColumnName("agreement_type");
            builder.Property(u => u.AgreementSubType).HasColumnName("agreement_sub_type");
            builder.Property(u => u.ContractNo).HasColumnName("contract_no");
            builder.Property(u => u.Counterparty).HasColumnName("counterparty");
            builder.Property(u => u.Owner).HasColumnName("owner");
            builder.Property(u => u.StartDate).HasColumnName("start_at");
            builder.Property(u => u.ExpiryDate).HasColumnName("expiry_at");
            builder.Property(u => u.OverDue).HasColumnName("overdue");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at");
            builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
            // builder.HasMany<UserTranslations>(g => g.NameLanguages);
            builder.ToTable("legal_contract_completed");
            // builder.HasQueryFilter(u => u.ExpiryDate);
        }
    }
}