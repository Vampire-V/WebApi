using AutoMapper;
using WebApi.Models.SASO;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class SASOService : ISASOService
    {
        private readonly IS4UnitOfWork _s4UnitOfWork;
        public readonly IMapper _mapper;
        public SASOService(IMapper mapper, IS4UnitOfWork s4UnitOfWork)
        {
            _mapper = mapper;
            _s4UnitOfWork = s4UnitOfWork;
        }

        public List<SASOView> GetSASO(DateTime pbdate, DateTime pcdate)
        {
            return _mapper.Map<List<SASOView>>(_s4UnitOfWork.SASORepository.GetSASO(pbdate, pcdate));
        }
    }
}