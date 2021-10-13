using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    public class Mahalle
    {
        [Key]
        public int AreaID { get; set; }
       
        [ForeignKey("Ilce")]
        public int CountyID { get; set; }
        public virtual Ilce Ilce { get; set; }
        public string AreaName { get; set; }
    }
}