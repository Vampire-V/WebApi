using Microsoft.EntityFrameworkCore;
using WebApi.Data.UserContext.Entities;
using WebApi.Data.UserContext.EntityTypeConfig;

namespace WebApi.Data.UserContext
{
    public class UserContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
        public virtual DbSet<UserTranslations> UserTranslations => Set<UserTranslations>();
        public virtual DbSet<ApiCallBack> ApiCallBack => Set<ApiCallBack>();
        public virtual DbSet<ContractCompleted> ContractCompleteds => Set<ContractCompleted>();
        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("UserContext");
            // optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),connection => connection.CommandTimeout(300))
            .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new RefreshTokenEntityTypeConfiguration().Configure(modelBuilder.Entity<RefreshToken>());
            new UserTranslationsEntityTypeConfiguration().Configure(modelBuilder.Entity<UserTranslations>());
            new ApiCallBackEntityTypeConfiguration().Configure(modelBuilder.Entity<ApiCallBack>());
            new ContractCompletedEntityTypeConfiguration().Configure(modelBuilder.Entity<ContractCompleted>());
        }
    }
}