using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Monitoring.Entities;

namespace WebApi.Data.Monitoring.EntityTypeConfig
{
    public class GRGIPlanEntityTypeConfiguration : IEntityTypeConfiguration<GrGiPlan>
    {
        public void Configure(EntityTypeBuilder<GrGiPlan> builder)
        {
            builder.HasKey(gr => gr.Id).HasName("id");
            builder.Property(gr => gr.Plant).IsRequired().HasColumnName("plant");
            builder.Property(gr => gr.Mrp).IsRequired().HasColumnName("mrp");
            builder.Property(gr => gr.PlanDate).IsRequired().HasColumnName("plan_date");
            builder.Property(gr => gr.PlanType).IsRequired().HasColumnName("plan_type");
            builder.Property(gr => gr.MonthTarget).IsRequired().HasColumnName("month_target");
            builder.Property(gr => gr.DayTarget).IsRequired().HasColumnName("day_target");
            builder.ToTable("tbAlert_GRGIPlan_Monthly");
        }
    }
}