namespace WebApi.Models.Vendor
{
    public class VendorParameter : QueryStringParameters
    {
        public string? VendorCode { get; set; }
        public string? Name { get; set; }
        public string? Fax { get; set; }
        public string? TaxId { get; set; }
    }
}