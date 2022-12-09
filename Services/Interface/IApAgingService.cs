using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Accounting.Entities;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IApAgingService : IScopedService
    {
        Task<List<ApAgingDetail>> GetApAgingDetailFilterAsync(DateTime checkDate, DateTime checkRate);
        Task<List<ApAgingPackage>> GetApAgingPackageFilterAsync(DateTime checkDate, DateTime checkRate);
        Task<List<ApAgingPbc>> GetApAgingPbcFilterAsync(DateTime checkDate, DateTime checkRate, string CheckPbc);
        Task<byte[]> GetApAgingReportForExcel(DateTime checkDate, DateTime checkRate);
    }
}