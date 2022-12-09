using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.UserContext.Entities;

namespace WebApi.Data.UserContext.EntityTypeConfig
{
    public class ApiCallBackEntityTypeConfiguration : IEntityTypeConfiguration<ApiCallBack>
    {
        public void Configure(EntityTypeBuilder<ApiCallBack> builder)
        {
            builder.HasKey(u => u.Id).HasName("id");
            builder.Property(u => u.Url).IsRequired().HasColumnName("url");
            builder.Property(u => u.CallBack).HasColumnName("callback");
            builder.ToTable("api_callback_line");
        }
    }
}