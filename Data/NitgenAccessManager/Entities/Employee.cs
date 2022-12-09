using WebApi.Extensions;

namespace WebApi.Data.NitgenAccessManager.Entities
{
    public class Employee
    {
        public long IndexKey { get; }
        public string Id {get; set;} = null!;
        public string Name {get; set;} = null!;
        public DateTime ExpDate { get; }
        public virtual List<EmployeeImage>? EmployeeImage { get; set; }
    }
}