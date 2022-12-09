using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.AccountingIndirectVendor
{
    // SQL Server 2005
    [Table("TaxVendor")]
    public class IndirectVendorUpdate
    {
        private string vendorCode = string.Empty;
        private string vendorName = string.Empty;
        private string? taxId;
        private string? headOfficeId;
        private string? branchId;

        [Key]
        [Required(ErrorMessage = "Vendor Code is required")]
        [StringLength(20, ErrorMessage = "VendorName can't be longer than 20 characters")]
        [Column("VendorCode")]
        public string VendorCode { get => vendorCode; set => vendorCode = value.Trim(); }

        // [Required(ErrorMessage = "Vendor Name is required")]
        [StringLength(255, ErrorMessage = "VendorName can't be longer than 255 characters")]
        [Column("VendorName")]
        public string VendorName { get => vendorName; set => vendorName = value.Trim(); }

        // [Required(ErrorMessage = "Tax ID is required")]
        [StringLength(20, ErrorMessage = "TaxId can't be longer than 20 characters")]
        [Column("TaxId")]
        public string? TaxId { get => taxId; set => taxId = value?.Trim(); }

        [StringLength(5, ErrorMessage = "Head Office ID can't be longer than 5 characters")]
        [Column("HeadOfficeId")]
        public string? HeadOfficeId { get => headOfficeId; set => headOfficeId = value?.Trim(); }

        [StringLength(5, ErrorMessage = "Branch ID can't be longer than 5 characters")]
        [Column("BranchId")]
        public string? BranchId { get => branchId; set => branchId = value?.Trim(); }


        // [Key]
        // [Required(ErrorMessage = "Vendor Code is required")]
        // [StringLength(20, ErrorMessage = "VendorName can't be longer than 20 characters")]
        // [Column("VendorCode")]
        // public string VendorCode { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Vendor Name is required")]
        // [StringLength(255, ErrorMessage = "VendorName can't be longer than 255 characters")]
        // [Column("VendorName")]
        // public string VendorName { get; set; } = string.Empty;
        
        // [Required(ErrorMessage = "Tax ID is required")]
        // [StringLength(20, ErrorMessage = "TaxId can't be longer than 20 characters")]
        // [Column("TaxId")]
        // public string? TaxId { get; set; }

        // [StringLength(5, ErrorMessage = "Head Office ID can't be longer than 5 characters")]
        // [Column("HeadOfficeId")]
        // public string? HeadOfficeId { get; set; }

        // [StringLength(5, ErrorMessage = "Branch ID can't be longer than 5 characters")]
        // [Column("BranchId")]
        // public string? BranchId { get; set; }
    }
}