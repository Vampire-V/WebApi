using Microsoft.EntityFrameworkCore;
using WebApi.Data.NitgenAccessManager.EntityTypeConfig;
using WebApi.Data.NitgenAccessManager.Entities;

namespace WebApi.Data.NitgenAccessManager
{
    public class NitgenAccessManager : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Employee> EmployeeTable => Set<Employee>();
        public DbSet<EmployeeImage> EmployeeImage => Set<EmployeeImage>();
        public DbSet<AuthLog> AuthLogs => Set<AuthLog>();
        public NitgenAccessManager(DbContextOptions<NitgenAccessManager> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("NitgenAccessManager");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EmployeeEntityTypeConfig().Configure(modelBuilder.Entity<Employee>());
            new EmployeeImageEntityTypeConfig().Configure(modelBuilder.Entity<EmployeeImage>());
            new AuthLogEntityTypeConfig().Configure(modelBuilder.Entity<AuthLog>());
        }
    }
}