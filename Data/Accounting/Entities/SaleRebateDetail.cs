namespace WebApi.Data.Accounting.Entities
{
    public class SaleRebateDetail
    {
        public int Index { get; set; }
        public string ProfSeg { get; set; } = null!;
        public string? Payer { get; set; }
        public string? BillDoc { get; set; }
        public string? BillItem { get; set; }
        public string? Return { get; set; }
        public int BillQty { get; set; }
        public string? BillUnit { get; set; }
        public int BillQtySKU { get; set; }
        public Decimal NetValue { get; set; }
        public string? RefDoc { get; set; }
        public string? RefItem { get; set; }
        public string? SalesDoc { get; set; }
        public string? SalesItem { get; set; }
        public string? Material { get; set; }
        public string? ItemDescription { get; set; }
        public string? MatlGroup { get; set; }
        public string? ItemCategory { get; set; }
        public string? ShippingPoint { get; set; }
        public string? Plant { get; set; }
        public string? AssignmentGroup { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public TimeSpan CreatedTime { get; set; }
        public string? StorageLocation { get; set; }
        public Decimal Cost { get; set; }
        public string? ProfitCenter { get; set; }
        public Decimal TaxAmount { get; set; }
        public string? BillType { get; set; }
        public string? SaleOrg { get; set; }
        public DateTime BillingDate { get; set; }
    }
}