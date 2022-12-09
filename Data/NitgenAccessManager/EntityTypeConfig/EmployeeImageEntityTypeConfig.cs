using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.NitgenAccessManager.Entities;

namespace WebApi.Data.NitgenAccessManager.EntityTypeConfig
{
    public class EmployeeImageEntityTypeConfig : IEntityTypeConfiguration<EmployeeImage>
    {
        public void Configure(EntityTypeBuilder<EmployeeImage> builder)
        {
            // builder.HasNoKey();
            builder.HasKey( e => e.Id).HasName("id");
            builder.Property(e => e.EmployeeNo).HasColumnName("employee");
            builder.Property(e => e.Url).HasColumnName("url");
            builder.Property(e => e.FileName).HasColumnName("file_name");
            builder.Property(e => e.Descriptor).HasColumnName("descriptor");
            builder.Property(e => e.Path).HasColumnName("path");
            builder.HasOne<Employee>(s => s.Employee)
            .WithMany(g => g.EmployeeImage);
            // .HasForeignKey(s => s.EmployeeNo);
            builder.ToTable("TAO_employee_image");
        }
    }
}