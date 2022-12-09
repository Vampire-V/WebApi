using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.S4.Entities
{
    [Table("SASO")]
    public class SASO
    {
        [Column("Plnt")]
        public string Plnt {get; set;} = null!;
        [Column("Customer")]
        public string Customer {get; set;} = null!;
        [Column("CustName")]
        public string? CustName {get; set;}
        [Column("Material")]
        public string Material {get; set;} = null!;
        [Column("MaterialDesc")]
        public string? MaterialDesc {get; set;}
        [Column("Total_B")]
        public Double TotalB {get; set;} 
        [Column("Total_C")]
        public Double TotalC {get; set;} 
        [Column("Total_Diff")]
        public Double TotalDiff {get; set;}
        [Column("Total_SO_B")]
        public Double TotalSOB {get; set;}
        [Column("Total_SO_C")]
        public Double TotalSOC {get; set;}
        [Column("Total_SO_Diff")]
        public Double TotalSODiff {get; set;}
    }
}