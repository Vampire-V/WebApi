using Microsoft.EntityFrameworkCore;
using WebApi.Models.User;
using WebApi.Data.UserContext.Entities;
using WebApi.Data.UserContext.Repositories.Interfaces;
using WebApi.Middleware.Exceptions;

namespace WebApi.Data.UserContext.Repositories.Implementations
{
    public class ApiCallBackRepository : BaseRepository<ApiCallBack>, IApiCallBackRepository
    {
        public ApiCallBackRepository(UserContext db) : base(db)
        {
        }

        public async Task<ApiCallBack> GetCallBackByUrl(string url)
        {
            try
            {
                var model = await _dbcontext.ApiCallBack.Where(d => d.Url == url).FirstOrDefaultAsync();
                if (model is null)
                {
                    throw new BadRequestException("url callback not found...");
                }
               return model;
            }
            catch (Exception ex)
            {
                
                throw new BadRequestException(ex.Message);
            }
        }
    }
}