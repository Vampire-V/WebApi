using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Data.UserContext.EntityTypeConfig
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // builder.HasKey(u => u.UserId).HasName("user_id");
            builder.HasKey(u => u.Token).HasName("token");
            builder.Property(u => u.UserId).HasColumnName("user_id");
            // builder.Property(u => u.Token).HasColumnName("token");
            // builder.Property(u => u.RefreshToken).HasColumnName("refresh_token");
            builder.Property(u => u.Created).HasColumnName("created_at");
            builder.Property(u => u.Expires).HasColumnName("expiration_at");
            // builder.Ignore(u => u.IsActive);
            // builder.Property(u => u.UserId).HasColumnName("user_id");
            // builder.Property(u => u.IsInvalidated).HasColumnName("is_invalidated");
            // builder.HasOne<User>(ur => ur.User)
            //         .WithMany(user => user.RefreshToken)
            //         .HasForeignKey(ur => ur.UserId);
            // builder.HasNoKey();
            builder.ToTable("refresh_token");
        }
    }
}