using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Accounting.Entities;
using WebApi.Services.Base;

namespace WebApi.Data.Accounting.Repositories.Interfaces
{
    public interface IApAgingRepository : IScopedService
    {
        Task<List<ApAgingDetail>> GetApAgingDetailFilterAsync(DateTime checkDate, DateTime checkRate);
        Task<List<ApAgingPackage>> GetApAgingPackageFilterAsync(DateTime checkDate, DateTime checkRate);
        Task<List<ApAgingPbc>> GetApAgingPbcFilterAsync(DateTime checkDate, DateTime checkRate, string CheckPbc);
    }
}