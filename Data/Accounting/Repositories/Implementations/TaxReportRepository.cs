using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.TaxReportBSEG;

namespace WebApi.Data.Accounting.Repositories.Implementations
{
    public class TaxReportRepository : BaseRepository<TaxReportView>, ITaxReportRepository
    {
        public TaxReportRepository(Accounting db):base(db)
        {
        }

        public async Task<List<TaxReportView>> GetTaxReportFilterAsync(TaxReportParameter taxReportParameter)
        {
            IQueryable<TaxReportView> query = this._dbcontext.TaxReportViews.AsQueryable();
            
            if (taxReportParameter != null)
            {
                query = query.Where( r => r.PostingDate >= taxReportParameter.BDate && r.PostingDate <= taxReportParameter.CDate);
            }
            
            var results = await query.ToListAsync();

            return results;

            // return query.Count() > 1 ? await query.ToListAsync() : new List<TaxReportView>(); 
        }
        
    }
}