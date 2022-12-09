namespace WebApi.Data.NitgenAccessManager.Entities
{
    // SQL Server 2005
    public class EmployeeImage
    {
        public int Id { get; set; }
        public string? EmployeeNo { get; set; }
        public string? FileName { get; set; }
        public string? Url { get; set; }
        public string? Descriptor { get; set; }
        public string? Path { get; set; }

        public Employee? Employee { get; set; }
    }
}