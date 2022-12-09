using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Vendor
{
    // SQL Server 2005
    [Table("Vendor")]
    public class VendorUpdate
    {
        [Column("Tax ID")]
        [Required(ErrorMessage = "Tax ID is required")]
        public string TaxId { get; set; } = null!;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        [Column("Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        [Column("Address")]
        public string Address { get; set; } = null!;
        [Column("Fax")]
        public string? Fax { get; set; }
        [Column("Tel")]
        public string? Tel { get; set; }
        [Column("PND")]
        public string? PND { get; set; }
        [Column("TaxIDVendor1")]
        public string? TaxIdVendor1 { get; set; }
        [Column("TaxIDVendor2")]
        public string? TaxIdVendor2 { get; set; }
        [Column("TaxIDVendor3")]
        public string? TaxIdVendor3 { get; set; }
        [Column("VATRegisNo")]
        public string? VATRegisNo { get; set; }
    }
}