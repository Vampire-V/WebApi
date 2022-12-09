using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Data.UserContext.EntityTypeConfig
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id).HasName("id");
            builder.Property(u => u.Username).IsRequired().HasColumnName("username");
            builder.Property(u => u.VerifyCode).HasColumnName("verify_code");
            builder.Property(u => u.Email).IsRequired().HasColumnName("email");
            builder.Property(u => u.Resigned).IsRequired().HasColumnName("resigned").HasDefaultValue(false);
            builder.Property(u => u.ExpiredCode).HasColumnName("expired_code");
            builder.Property(u => u.LineId).HasColumnName("line_id");
            builder.HasMany<UserTranslations>(g => g.NameLanguages);
            builder.ToTable("users");
            // .HasQueryFilter(u => u.Resigned == false);
        }
    }
}