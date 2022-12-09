using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.NitgenAccessManager.Entities;

namespace WebApi.Data.NitgenAccessManager.EntityTypeConfig
{
    public class EmployeeEntityTypeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.IndexKey).HasColumnName("IndexKey");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.ExpDate).HasColumnName("expDate").HasColumnType("datetime");
            builder.HasMany<EmployeeImage>(g => g.EmployeeImage).WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeNo);
            builder.ToTable("NGAC_USERINFO");
        }
    }
}