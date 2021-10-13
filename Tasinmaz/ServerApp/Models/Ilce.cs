using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    public class Ilce
    {
        [Key]
        public int CountyID { get; set; }
       
        [ForeignKey("Il")]
        public int CityID { get; set; }
        public virtual Il Il { get; set; }
        public string CountyName { get; set; }
    }
}