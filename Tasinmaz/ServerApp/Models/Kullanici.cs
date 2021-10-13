using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models
{
    public class Kullanici
    {
        [Key]
        public int UserID { get; set; }
        public string Ad  { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }       
        //rol true -> admin false -> normal kullanici
        public bool Rol { get; set; }
        public bool isActive { get; set; }
    }
}