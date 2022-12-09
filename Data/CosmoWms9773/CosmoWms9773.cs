using Microsoft.EntityFrameworkCore;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Data.CosmoWms9773.EntityTypeConfig;

namespace WebApi.Data.CosmoWms9773
{
    public class CosmoWms9773 : DbContext
    {
        public DbSet<PoInformation> PoInformation => Set<PoInformation>();
        public CosmoWms9773(DbContextOptions<CosmoWms9773> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("CosmoWms9773");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PoInformationConfig().Configure(modelBuilder.Entity<PoInformation>());
        }
    }
}