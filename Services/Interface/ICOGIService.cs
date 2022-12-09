using WebApi.Models.COGI;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface ICOGIService : IScopedService
    {
        List<COGIView> GetCOGI(DateTime TimeStamp);
        byte[] GetCOGIForExcel(DateTime TimeStamp);
    }
}