using WebApi.Data.UserContext.Entities;
using WebApi.Models.User;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface ILineService : IScopedService
    {
        Task<LineAccessTokenResponse> AccessToken(LineCallBackParameter param);
        Task RefreshAccessToken();
        Task<LineUserProfile> Profile(string token);
        Task<LineVerifyResponse?> Verify(string token);
        Task Revoke(string token);
        Task<string> GenLink(string url);
    }
}