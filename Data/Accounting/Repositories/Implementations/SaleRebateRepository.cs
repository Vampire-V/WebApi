using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Models.SaleRebate;

namespace WebApi.Data.Accounting.Repositories.Implementations
{
    public class SaleRebateRepository : BaseRepository<SaleRebateView>, ISaleRebateRepository
    {
        public SaleRebateRepository(Accounting db) : base(db)
        {
        }

        public async Task<List<SaleRebateView>> GetSaleRebateViewFilterAsync(SaleRebateParameter SaleRebateParameter)
        {
            IQueryable<SaleRebateView> query = this._dbcontext.SaleRebateView.AsQueryable();

            if (SaleRebateParameter.Year != null)
            {
                query = query.Where(v => v.Month!.Substring(0,4) == SaleRebateParameter.Year);
            }
            if (SaleRebateParameter.Month != null)
            {
                query = query.Where(v => v.Month!.Substring(4,2) == SaleRebateParameter.Month);
            }

            var results = await query.ToListAsync();

            return results;

            // var Data = await _dbcontext.SaleRebateView
            //     .FromSqlRaw($"SELECT * FROM accounting.sale_rebate_view t WHERE t.Month LIKE '{SaleRebateParameter.Year}%'")
            //     .ToListAsync();
            // return Data;
        }

        public async Task<List<SaleRebateDetail>> GetSaleRebateDetailFilterAsync(SaleRebateParameter SaleRebateParameter)
        {
            IQueryable<SaleRebateDetail> query = this._dbcontext.SaleRebateDetail.AsQueryable();

            if (SaleRebateParameter.Year != null)
            {
                query = query.Where(v => v.BillingDate.ToString().Substring(0,4) == SaleRebateParameter.Year);
            }
            if (SaleRebateParameter.Month != null)
            {
                query = query.Where(v => v.BillingDate.ToString().Substring(5,2) == SaleRebateParameter.Month);
            }

            var results = await query.ToListAsync();

            return results;
        }
    }
}