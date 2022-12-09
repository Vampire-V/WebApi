using WebApi.Models.SASO;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface ISASOService : IScopedService
    {
        List<SASOView> GetSASO(DateTime pbdate, DateTime pcdate);
    }
}