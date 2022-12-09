using Microsoft.EntityFrameworkCore;
using WebApi.Data.ChequeDirect.EntityTypeConfig;
using WebApi.Data.ChequeDirect.Entities;

namespace WebApi.Data.ChequeDirect
{
    public class ChequeDirect : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Vendor> Vendor => Set<Vendor>();
        public ChequeDirect(DbContextOptions<ChequeDirect> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("ChequeDirect");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new VendorEntityTypeConfiguration().Configure(modelBuilder.Entity<Vendor>());
        }
    }
}