
namespace WebApi.Data.ChequeDirect.Entities
{
    // SQL Server 2005
    public class Vendor
    {
        // [Required(ErrorMessage = "Tax ID is required")]
        public string TaxId { get; set; } = null!;
        // [Required(ErrorMessage = "VendorCode is required")]
        public string VendorCode { get; set; } = null!;
        // [Required(ErrorMessage = "Name is required")]
        // [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; } = null!;
        // [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        public string? Fax { get; set; }
        public string? Tel { get; set; }
        public string? PND { get; set; }
        public string? TaxIdVendor1 { get; set; }
        public string? TaxIdVendor2 { get; set; }
        public string? TaxIdVendor3 { get; set; }
        public string? VATRegisNo { get; set; }
    }
}