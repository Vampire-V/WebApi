using AutoMapper;
using WebApi.Services.Interface;
using WebApi.Models.User;
using WebApi.Extensions;
using WebApi.Data.UserContext.Entities;
using WebApi.UOW.Interface;
using Microsoft.Extensions.Options;
using WebApi.Config;
using Newtonsoft.Json;
using WebApi.Middleware.Exceptions;

namespace WebApi.Services.Implementations
{
    public class LineService : ILineService
    {
        private readonly IUserContextUnitOfWork _userContextUnitOfWork;
        public readonly IMapper _mapper;
        private readonly IOptions<LineDev> _lineDev;
        private readonly ILogger<MailService> _logger;
        public LineService(IMapper mapper, ILogger<MailService> logger, IUserContextUnitOfWork userContextUnitOfWork, IOptions<LineDev> lineDev)
        {
            _mapper = mapper;
            _logger = logger;
            _userContextUnitOfWork = userContextUnitOfWork;
            _lineDev = lineDev;
        }

        public async Task<LineAccessTokenResponse> AccessToken(LineCallBackParameter param)
        {
            HttpClient client = new HttpClient();
            var data = new[]
            {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", param.Code),
                    new KeyValuePair<string, string>("redirect_uri", param.Urlfrom),
                    new KeyValuePair<string, string>("client_id", _lineDev.Value.ChannelID),
                    new KeyValuePair<string, string>("client_secret", _lineDev.Value.ChannelSecret)
                };
            HttpResponseMessage response = client.PostAsync("https://api.line.me/oauth2/v2.1/token", new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LineAccessTokenResponse>(responseBody);
            if (result is null)
            {
                throw new BadRequestException("DeserializeObject Error : Line access token response..");
            }
            return result;
        }

        public Task RefreshAccessToken()
        {
            throw new BadRequestException("Error oauth line token is null....");
        }

        public async Task<LineUserProfile> Profile(string token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage resp = client.GetAsync("https://api.line.me/v2/profile").GetAwaiter().GetResult();
                resp.EnsureSuccessStatusCode();
                string respBody = await resp.Content.ReadAsStringAsync();
                var userProfile = JsonConvert.DeserializeObject<LineUserProfile>(respBody);
                if (userProfile is null)
                {
                    throw new BadRequestException("Error oauth line token is null....");
                }
                return userProfile;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        }

        public async Task<LineVerifyResponse?> Verify(string token)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(String.Format("https://api.line.me/oauth2/v2.1/verify?access_token={0}", token));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            LineVerifyResponse? result = JsonConvert.DeserializeObject<LineVerifyResponse>(responseBody);
            return result;
        }

        public Task Revoke(string token)
        {
            throw new BadRequestException("Error oauth line token is null....");
        }

        public async Task<string> GenLink(string url)
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string state = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
                ApiCallBack model = await _userContextUnitOfWork.ApiCallBackRepository.GetCallBackByUrl(url);
                return $"https://access.line.me/oauth2/v2.1/authorize?response_type=code&client_id={_lineDev.Value.ChannelID}&redirect_uri={model.CallBack}&state={state}&scope=profile%20openid%20email";
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}