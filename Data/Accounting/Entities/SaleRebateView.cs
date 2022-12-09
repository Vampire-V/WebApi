namespace WebApi.Data.Accounting.Entities
{
    public class SaleRebateView
    {
        public string? Month { get; set; }
        public Decimal RFQty { get; set; }
        public Decimal ACQty { get; set; }
        public Decimal TotalQty { get; set; }
        public Decimal RFAmt { get; set; }
        public Decimal ACAmt { get; set; }
        public Decimal TotalAmt { get; set; }
        public string? RFPromotionPercent { get; set; }
        public Decimal RFPromotionAmt { get; set; }
        public string? ACPromotionPercent { get; set; }
        public Decimal ACPromotionAmt { get; set; }
        public Decimal TotalPromotionAmt { get; set; }
        public string? RFRoyaltyPercent { get; set; }
        public Decimal RFRoyaltyAmt { get; set; }
    }
}