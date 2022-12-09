using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Entities;
using WebApi.Data.Accounting.Repositories.Interfaces;

namespace WebApi.Data.Accounting.Repositories.Implementations
{
    public class ApAgingRepository : BaseRepository<ApAgingPackage>, IApAgingRepository
    {
        public ApAgingRepository(Accounting db) : base(db)
        {
        }

        public async Task<List<ApAgingDetail>> GetApAgingDetailFilterAsync(DateTime checkDate, DateTime checkRate)
        {
            return await this._dbcontext.ApAgingDetail.FromSqlRaw<ApAgingDetail>($"CALL ap_detail_test('{checkDate.ToString("yyyy-MM-dd")}','{checkRate.ToString("yyyy-MM-dd")}');").ToListAsync();
        }
        public async Task<List<ApAgingPackage>> GetApAgingPackageFilterAsync(DateTime checkDate, DateTime checkRate)
        {
            return await this._dbcontext.ApAgingPackage.FromSqlRaw<ApAgingPackage>($"CALL ap_package_test('{checkDate.ToString("yyyy-MM-dd")}','{checkRate.ToString("yyyy-MM-dd")}');").ToListAsync();
        }
        public async Task<List<ApAgingPbc>> GetApAgingPbcFilterAsync(DateTime checkDate, DateTime checkRate, string checkPbc)
        {
            return await this._dbcontext.ApAgingPbc.FromSqlRaw<ApAgingPbc>($"CALL ap_pbc{checkPbc}_test('{checkDate.ToString("yyyy-MM-dd")}','{checkRate.ToString("yyyy-MM-dd")}');").ToListAsync();
        }
    }
}