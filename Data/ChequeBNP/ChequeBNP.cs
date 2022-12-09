using Microsoft.EntityFrameworkCore;
using WebApi.Data.ChequeBNP.EntityTypeConfig;
using WebApi.Data.ChequeBNP.Entities;

namespace WebApi.Data.ChequeBNP
{
    public class ChequeBNP : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<ResultData> ResultData => Set<ResultData>();
        public ChequeBNP(DbContextOptions<ChequeBNP> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("ChequeBNP");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ResultDataEntityTypeConfiguration().Configure(modelBuilder.Entity<ResultData>());
        }
    }
}