using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Models.GRIR;

namespace WebApi.Data.Accounting.Repositories.Implementations
{
    public class GrIrReportRepository : BaseRepository<GrIrDetailView>, IGrIrReportRepository
    {
        public GrIrReportRepository(Accounting db) : base(db)
        {
        }

        // public async Task<List<GrIrReport>> GetGrIrReportFilterAsync(GrIrReportParameter GrIrReportParameter)
        // {
        //     IQueryable<GrIrReport> query = this._dbcontext.GrIrReport.AsQueryable();

        //     if (GrIrReportParameter.Plant != null)
        //     {
        //         query = query.Where( r => r.Plant == GrIrReportParameter.Plant);
        //     }
        //     if (GrIrReportParameter.Assignment != null)
        //     {
        //         query = query.Where( r => r.Assingment == GrIrReportParameter.Assignment);
        //     }
        //     if (GrIrReportParameter.PurchasingDocument != null)
        //     {
        //         query = query.Where( r => r.PurchasingDocument == GrIrReportParameter.PurchasingDocument);
        //     }

        //     query = query
        //     .Include(i => i.GrIrObjectKey)
        //     .Include(i => i.PoType)
        //     .Include(i => i.Vendor_VendorCode)
        //     .Include(i => i.Vendor_OffsetAcct)
        //     .OrderBy(i => i.Plant).ThenBy(i => i.Assingment)
        //     .DefaultIfEmpty();
        //     var results = await query.ToListAsync();
        //     // return results.Count() > 1 ? await results.ToListAsync() : new List<GrIrReport>();
        //     return results;
        // }

        public async Task<List<GrIrDetailView>> GetGrIrDetailViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            IQueryable<GrIrDetailView> query = this._dbcontext.GrIrDetailView.AsQueryable();
            if (GrIrReportParameter.Plant != null)
            {
                query = query.Where(r => r.Plant == GrIrReportParameter.Plant);
            }
            if (GrIrReportParameter.PurchaseTypeDesc != null)
            {
                query = query.Where(r => r.PurchaseTypeDesc == GrIrReportParameter.PurchaseTypeDesc);
            }
            if (GrIrReportParameter.PurchasingDocument != null)
            {
                query = query.Where(r => r.PurchasingDocument == GrIrReportParameter.PurchasingDocument);
            }
            if (GrIrReportParameter.VendorCode != null)
            {
                query = query.Where(r => r.VendorCode == GrIrReportParameter.VendorCode);
            }
            if (GrIrReportParameter.VendorName != null)
            {
                query = query.Where(r => r.Vendors!.VendorName == GrIrReportParameter.VendorName);
            }
            var results = await query
            .Include(i => i.Vendors)
            .DefaultIfEmpty<GrIrDetailView>()
            .ToListAsync();

            return results.First() is null ? new List<GrIrDetailView>() : results; 
        }

        public async Task<List<GrIrPlantView>> GetGrIrPlantViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            IQueryable<GrIrPlantView> query = this._dbcontext.GrIrPlantView.AsQueryable();
            if (GrIrReportParameter.Plant != null)
            {
                query = query.Where(r => r.Plant == GrIrReportParameter.Plant);
            }
            if (GrIrReportParameter.PurchaseTypeDesc != null)
            {
                query = query.Where(r => r.PurchaseTypeDesc!.Contains(GrIrReportParameter.PurchaseTypeDesc));
            }
            if (GrIrReportParameter.PurchasingDocument != null)
            {
                query = query.Where(r => r.PurchasingDocument == GrIrReportParameter.PurchasingDocument);
            }
            if (GrIrReportParameter.VendorCode != null)
            {
                query = query.Where(r => r.VendorCode == GrIrReportParameter.VendorCode);
            }
            if (GrIrReportParameter.VendorName != null)
            {
                query = query.Where(r => r.Vendors!.VendorName.Contains(GrIrReportParameter.VendorName));
            }

            var results = await query
            .Include(i => i.Vendors)
            .DefaultIfEmpty<GrIrPlantView>()
            .ToListAsync();

            return results.First() is null ? new List<GrIrPlantView>() : results; 
        }

        public async Task<List<GrIrSummaryView>> GetGrIrSummaryViewFilterAsync(GrIrReportParameter GrIrReportParameter)
        {
            IQueryable<GrIrSummaryView> query = this._dbcontext.GrIrSummaryView.AsQueryable();
            if (GrIrReportParameter.Plant != null)
            {
                query = query.Where(r => r.Plant == GrIrReportParameter.Plant);
            }
            if (GrIrReportParameter.PurchaseTypeDesc != null)
            {
                query = query.Where(r => r.PurchaseTypeDesc == GrIrReportParameter.PurchaseTypeDesc);
            }

            var results = await query.ToListAsync();
            return results;
        }
    }
}