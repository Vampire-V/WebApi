using System.Threading.Tasks;
using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Data.UserContext.Repositories.Interfaces
{
    public interface IApiCallBackRepository : IScopedService, IBaseRepository<ApiCallBack>
    {
        Task<ApiCallBack> GetCallBackByUrl(string url);
    }
}