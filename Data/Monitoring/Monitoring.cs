using Microsoft.EntityFrameworkCore;
using WebApi.Data.Monitoring.Entities;
using WebApi.Data.Monitoring.EntityTypeConfig;

namespace WebApi.Data.Monitoring
{
    public class Monitoring : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<GrGiPlan> GrGiPlan => Set<GrGiPlan>();
        public Monitoring(DbContextOptions<Monitoring> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("Monitoring");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<GrGiPlan>().ToTable("tbAlert_GRGIPlan_Example");
            new GRGIPlanEntityTypeConfiguration().Configure(modelBuilder.Entity<GrGiPlan>());
        }
    }
}