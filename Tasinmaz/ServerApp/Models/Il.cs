using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models
{
    public class Il
    {
        [Key]
        public int CityID  { get; set; } 
        public int CountryID { get; set; }
        public string CityName { get; set; }  
        public string PlateNo { get; set; }
        public string PhoneCode { get; set; }
    }
    
}