using Microsoft.EntityFrameworkCore;
using WebApi.Data.S4.Entities;
using WebApi.Data.S4.EntityTypeConfig;

namespace WebApi.Data.S4
{
    public class S4 : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<SASO> SASOTable => Set<SASO>();
        public DbSet<COGI> COGITable => Set<COGI>();
        public S4(DbContextOptions<S4> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("S4");
            optionsBuilder.UseSqlServer(connectionString,sqlServerOptionsAction => sqlServerOptionsAction.CommandTimeout(120)).EnableDetailedErrors().LogTo(Console.WriteLine, LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SASO>().ToTable("SASO").HasNoKey();
            // modelBuilder.Entity<COGI>().ToTable("AFFW").HasNoKey();
            new COGIEntityTypeConfig().Configure(modelBuilder.Entity<COGI>());
        }
    }
}