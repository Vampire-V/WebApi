using WebApi.Services.Base;

namespace WebApi.Services.Interface{
    public interface IMailService : IScopedService
    {
        Task MailVerifyCode(string mailTo,string verifyCode);
        void Test(string mailTo,string text);
        void ReflectionMethod();
    }
}