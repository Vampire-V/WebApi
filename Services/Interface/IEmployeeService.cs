using WebApi.Data.NitgenAccessManager.Entities;
using WebApi.Models.Employee;
using WebApi.Services.Base;

namespace WebApi.Services.Interface
{
    public interface IEmployeeService : IScopedService
    {
         Task<List<EmployeeView>> GetEmployees();
         Task<List<Employee>> GetStaffAndImage();
         Task CreateStaffImage(string employeeNo, string url, string path, string fileName, string detectFace);
         Task StaffScanFace(StaffScan staffScan);
         Task<List<Employee>> Filter(EmployeeParameter parameter);
         Task<bool> RemoveStaffImage(int id);
    }
}