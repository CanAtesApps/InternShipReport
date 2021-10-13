using System;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models
{
    public class Log
    {
        [Key]
        public int logID { get; set; }
        public string logDetails { get; set; }
        public DateTime logTime { get; set; }
        public bool logSuccess{get; set;}
        public string ipAdress { get; set; }
    }
}