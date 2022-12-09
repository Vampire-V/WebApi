using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Data.UserContext.EntityTypeConfig
{
    public class UserTranslationsEntityTypeConfiguration : IEntityTypeConfiguration<UserTranslations>
    {
        public void Configure(EntityTypeBuilder<UserTranslations> builder)
        {
            builder.HasKey(u => u.Id).HasName("id");
            builder.Property(u => u.UserId).IsRequired().HasColumnName("user_id");
            builder.Property(u => u.Locale).IsRequired().HasColumnName("locale");
            builder.Property(u => u.Name).IsRequired().HasColumnName("name");
            // builder.HasOne<User>(s => s.UserId);
            // .WithMany(g => g.);
            builder.ToTable("user_translations");
            // .HasQueryFilter(u => u.Resigned == false);
        }
    }
}