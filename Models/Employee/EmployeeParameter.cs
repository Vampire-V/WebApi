namespace WebApi.Models.Employee
{
    public class EmployeeParameter : QueryStringParameters
    {
        public string IdStaff { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}