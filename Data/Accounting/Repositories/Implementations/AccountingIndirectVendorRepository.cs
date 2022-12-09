using Microsoft.EntityFrameworkCore;
using WebApi.Data.Accounting.Repositories.Interfaces;
using WebApi.Data.Accounting.Entities;
using WebApi.Models.AccountingIndirectVendor;

namespace WebApi.Data.Accounting.Repositories.Implementations
{
    public class AccountingIndirectVendorRepository : BaseRepository<AccountingIndirectVendor>, IAccountingIndirectVendorRepository
    {
        public AccountingIndirectVendorRepository(Accounting db):base(db)
        {
        }

        public async Task<List<AccountingIndirectVendor>> GetIndirectVendorsFilterAsync(IndirectVendorParameter indirectVendorParameter)
        {
            IQueryable<AccountingIndirectVendor> query = this._dbcontext.AccountingIndirectVendor.AsQueryable(); //.ToArray().Skip(1).Take(10);

            if (indirectVendorParameter.VendorCode != null)
            {
                query = query.Where(v => v.VendorCode == indirectVendorParameter.VendorCode);
            }
            if (indirectVendorParameter.VendorName != null)
            {
                query = query.Where(v => v.VendorName.Contains(indirectVendorParameter.VendorName));
            }
            if (indirectVendorParameter.TaxId != null)
            {
                query = query.Where(v => v.TaxId == indirectVendorParameter.TaxId);
            }

            var results = await query.ToListAsync();
            
            return results;
        }
        
    }
}