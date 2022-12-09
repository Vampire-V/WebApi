using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.S4.Entities
{
    public class COGI
    {
        public string Plant {get; set;} = null!;
        public string ReservNo {get; set;} = null!;
        public string OrderNo {get; set;} = null!;
        public string Material {get; set;} = null!;
        public string Location {get; set;} = null!;
        public Decimal Quantity {get; set;}
        public string Unit {get; set;} = null!;
        public string MovementType {get; set;} = null!;
        public string MessageNo {get; set;} = null!;
        public string MessageType {get; set;} = null!;
        public string ErrorMessage {get; set;} = null!;
        public string Mrp {get; set;} = null!;
        public DateTime PostingDate {get; set;}
        public string RowId {get; set;} = null!;
        public DateTime TimeStamp {get; set;}
    }
}