using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.CosmoIm9773.Entities;
using WebApi.Models.ProductionOrder;
using WebApi.Services.Base;

namespace WebApi.Data.CosmoWms9773.Repositories.Interfaces
{
    public interface IPoInformationRepository : IScopedService
    {
        Task<List<PoInformation>> GetPoInformationFilterAsync(PoInformationParameter Parameter);
    }
}