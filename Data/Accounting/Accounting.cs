using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.EntityTypeConfig;

namespace WebApi.Data.Accounting
{
    public class Accounting : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<AccountingVendor> AccountingVendor => Set<AccountingVendor>();
        public DbSet<AccountingIndirectVendor> AccountingIndirectVendor => Set<AccountingIndirectVendor>();

        public DbSet<TaxReportView> TaxReportViews => Set<TaxReportView>();

        public DbSet<GrIrDetailView> GrIrDetailView => Set<GrIrDetailView>();
        public DbSet<GrIrPlantView> GrIrPlantView => Set<GrIrPlantView>();
        public DbSet<GrIrSummaryView> GrIrSummaryView => Set<GrIrSummaryView>();

        public DbSet<SaleRebateView> SaleRebateView => Set<SaleRebateView>();
        public DbSet<SaleRebateDetail> SaleRebateDetail => Set<SaleRebateDetail>();

        public DbSet<ApAgingDetail> ApAgingDetail => Set<ApAgingDetail>();
        public DbSet<ApAgingPackage> ApAgingPackage => Set<ApAgingPackage>();
        public DbSet<ApAgingPbc> ApAgingPbc => Set<ApAgingPbc>();

        public Accounting(DbContextOptions<Accounting> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("Accounting");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),connection => connection.CommandTimeout(300))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AccountingVendorConfig().Configure(modelBuilder.Entity<AccountingVendor>());

            // Indirect Vendor
            new AccountingIndirectVendorConfig().Configure(modelBuilder.Entity<AccountingIndirectVendor>());

            // Tax Report
            new TaxReportViewConfig().Configure(modelBuilder.Entity<TaxReportView>());

            // GRIR Report
            new GrIrDetailViewConfig().Configure(modelBuilder.Entity<GrIrDetailView>());
            new GrIrPlantViewConfig().Configure(modelBuilder.Entity<GrIrPlantView>());
            new GrIrSummaryViewConfig().Configure(modelBuilder.Entity<GrIrSummaryView>());

            // Sale Rebate Report
            new SaleRebateViewConfig().Configure(modelBuilder.Entity<SaleRebateView>());
            new SaleRebateDetailConfig().Configure(modelBuilder.Entity<SaleRebateDetail>());

            // AP Aging Report
            new ApAgingDetailConfig().Configure(modelBuilder.Entity<ApAgingDetail>());
            new ApAgingPackageConfig().Configure(modelBuilder.Entity<ApAgingPackage>());
            new ApAgingPbcConfig().Configure(modelBuilder.Entity<ApAgingPbc>());
        }
    }
}