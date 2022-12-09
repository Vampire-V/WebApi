using AutoMapper;
using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Extensions;
using WebApi.Models.Employee;
using WebApi.Services.Interface;
using WebApi.UOW.Interface;

namespace WebApi.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly INitgenAccessManagerUnitOfWork _nitgenAccessManagerUnitOfWork;
        public readonly IMapper _mapper;
        public EmployeeService(IMapper mapper, INitgenAccessManagerUnitOfWork nitgenAccessManagerUnitOfWork)
        {
            _mapper = mapper;
            _nitgenAccessManagerUnitOfWork = nitgenAccessManagerUnitOfWork;
        }

        public async Task<List<EmployeeView>> GetEmployees()
        {
            var items = await _nitgenAccessManagerUnitOfWork.EmployeeRepository.GetAll();
            return _mapper.Map<List<EmployeeView>>(items);
        }

        public async Task<List<Employee>> GetStaffAndImage()
        {
            return await _nitgenAccessManagerUnitOfWork.EmployeeRepository.StaffImage();
        }

        public async Task CreateStaffImage(string employeeNo, string url,string path, string fileName, string detectFace)
        {
            EmployeeImage entity = new EmployeeImage
            {
                EmployeeNo = employeeNo,
                Url = url,
                FileName = fileName,
                Descriptor = detectFace,
                Path = path
            };
            await _nitgenAccessManagerUnitOfWork.EmployeeImageRepository.Add(entity);
            await _nitgenAccessManagerUnitOfWork.SaveAsync();
        }

        public async Task StaffScanFace(StaffScan staffScan)
        {
            try
            {
                var staff = await _nitgenAccessManagerUnitOfWork.EmployeeRepository.GetById(staffScan.EmployeeNo);
                AuthLog entity = new AuthLog
                {
                    UserIdIndex = staff.IndexKey,
                    UserId = staffScan.EmployeeNo,
                    TransactionTime = staffScan.TransactionTime
                };
                await _nitgenAccessManagerUnitOfWork.AuthLogRepository.Add(entity);
                await _nitgenAccessManagerUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException?.Message);
            }
        }

        public async Task<List<Employee>> Filter(EmployeeParameter parameter)
        {
            try
            {
                return await _nitgenAccessManagerUnitOfWork.EmployeeRepository.Filter(parameter);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.InnerException?.Message);
            }

        }

        public async Task<bool> RemoveStaffImage(int id)
        {
            try
            {
                var entity = await _nitgenAccessManagerUnitOfWork.EmployeeImageRepository.GetById(id);
                if (entity is null || entity.Path is null)
                {
                    return false;
                }
                ManageFiles.Remove(entity.Path);
                _nitgenAccessManagerUnitOfWork.EmployeeImageRepository.Remove(entity);
                await _nitgenAccessManagerUnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException?.Message);
            }

        }
    }
}