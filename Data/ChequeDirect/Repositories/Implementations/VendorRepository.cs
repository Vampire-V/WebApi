using Microsoft.EntityFrameworkCore;
using WebApi.Models.Vendor;
using WebApi.Data.ChequeDirect.Repositories.Interfaces;
using WebApi.Data.ChequeDirect.Entities;

namespace WebApi.Data.ChequeDirect.Repositories.Implementations
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {

        public VendorRepository(ChequeDirect db) : base(db)
        {
        }
        public async Task<List<Vendor>> GetVendorsFilterAsync(VendorParameter vendorParameter)
        {
            IQueryable<Vendor> query = this._dbcontext.Vendor.AsQueryable(); //.ToArray().Skip(1).Take(10);

            if (vendorParameter.Name != null)
            {
                query = query.Where(v => v.Name.Contains(vendorParameter.Name));
            }
            if (vendorParameter.VendorCode != null)
            {
                query = query.Where(v => v.VendorCode.Contains(vendorParameter.VendorCode));
            }
            if (vendorParameter.Fax != null)
            {
                query = query.Where(v => v.Fax == vendorParameter.Fax);
            }
            if (vendorParameter.TaxId != null)
            {
                query = query.Where(v => v.TaxId == vendorParameter.TaxId);
            }
            var results = await query.ToListAsync();
            return results;
        }
    }
}