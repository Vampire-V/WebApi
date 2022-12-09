namespace WebApi.Models.AccountingIndirectVendor
{
    public class IndirectVendorParameter
    {
        private string? vendorCode;
        private string? vendorName;
        private string? taxId;

        public string? VendorCode { get => vendorCode; set => vendorCode = value?.Trim(); }
        public string? VendorName { get => vendorName; set => vendorName = value?.Trim(); }
        public string? TaxId { get => taxId; set => taxId = value?.Trim(); }
    }
}