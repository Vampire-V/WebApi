using AutoMapper;
using WebApi.Data.Monitoring.Entities;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class GRGIService : IGRGIService
    {
        private readonly IMonitoringUnitOfWork _monitoringUnitOfWork;
        public readonly IMapper _mapper;
        public GRGIService(IMapper mapper, IMonitoringUnitOfWork monitoringUnitOfWork)
        {
            _mapper = mapper;
            _monitoringUnitOfWork = monitoringUnitOfWork;
        }

        public async Task<IEnumerable<GrGiPlan>> GetGRGIPlan()
        {
            return await _monitoringUnitOfWork.GRGIPlanRepository.GetAll();
        }

        public async Task CreateRang(IEnumerable<GrGiPlan> entity)
        {
            await _monitoringUnitOfWork.GRGIPlanRepository.AddRange(entity);
            await _monitoringUnitOfWork.SaveAsync();
        }

        public async Task AddOrUpdateRange(List<GrGiPlan> items)
        {
            foreach (var item in items)
            {
                var ss = _monitoringUnitOfWork.GRGIPlanRepository.Find(g => item.Plant == g.Plant && item.Mrp == g.Mrp && item.PlanDate == g.PlanDate && item.PlanType == g.PlanType).FirstOrDefault();
                if (ss is null)
                {
                    await _monitoringUnitOfWork.GRGIPlanRepository.Add(item);
                    await _monitoringUnitOfWork.SaveAsync();
                }else
                {
                    ss.DayTarget = item.DayTarget;
                    ss.MonthTarget = item.MonthTarget;
                    await _monitoringUnitOfWork.SaveAsync();
                }
            }
        }
    }
}