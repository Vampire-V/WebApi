using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.EntityTypeConfig;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Data.CosmoIm9773.EntityTypeConfig;

namespace WebApi.Data.CosmoIm9773
{
    public class CosmoIm9773 : DbContext
    {
        public DbSet<FGProductionOrder> FGProductionOrder => Set<FGProductionOrder>();
        public DbSet<SFGProductionOrder> SFGProductionOrder => Set<SFGProductionOrder>();
        public DbSet<FGOffline> FGOffline => Set<FGOffline>();
        public DbSet<SFGOffline> SFGOffline => Set<SFGOffline>();
        public DbSet<OfflineSummarize> OfflineSummarize => Set<OfflineSummarize>();
        public CosmoIm9773(DbContextOptions<CosmoIm9773> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("CosmoIm9773");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new FGProductionOrderConfig().Configure(modelBuilder.Entity<FGProductionOrder>());
            new SFGProductionOrderConfig().Configure(modelBuilder.Entity<SFGProductionOrder>());
            new FGOfflineConfig().Configure(modelBuilder.Entity<FGOffline>());
            new SFGOfflineConfig().Configure(modelBuilder.Entity<SFGOffline>());
            new OfflineSummarizeConfig().Configure(modelBuilder.Entity<OfflineSummarize>());
        }
    }
}