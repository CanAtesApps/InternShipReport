using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    public class Tasinmaz
    {
        [Key]
        public int TasinmazID { get; set; }
        public int CityID { get; set; }
        public int CountyID { get; set; }
        
        [ForeignKey("Mahalle")]
        public int MahalleID { get; set; }
        public virtual Mahalle Mahalle { get; set; }
        public int Ada { get; set; }
        public int Parsel { get; set; }
        public string Nitelik { get; set; }
        public string Adres { get; set; }
        public bool isActive { get; set; }
    }
}