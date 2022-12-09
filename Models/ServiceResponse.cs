namespace WebApi.Models
{
    public class ServiceResponse<T>
    {
        public T? Item { get; set; }

        public bool Success { get; set; } = true;

        public string? Message { get; set; }
    }
}