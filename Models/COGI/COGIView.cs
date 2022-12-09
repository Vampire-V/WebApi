namespace WebApi.Models.COGI
{
    public class COGIView
    {
        public string? Plant {get; set;}
        public string? ReservNo {get; set;}
        public string? OrderNo {get; set;}
        public string? Material {get; set;}
        public string? Location {get; set;}
        public Decimal? Quantity {get; set;}
        public string? Unit {get; set;}
        public string? MovementType {get; set;}
        public string? MessageNo {get; set;}
        public string? MessageType {get; set;}
        public string? ErrorMessage {get; set;}
        public string? Mrp {get; set;}
        public DateTime? PostingDate {get; set;}
        public string? RowId {get; set;}
        public DateTime? TimeStamp {get; set;}
    }
}