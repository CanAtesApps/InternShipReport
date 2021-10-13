using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanCevre
{
    public class Ucgen : ICalculate
    {
        public double kenar1 { get; set; }
        public double kenar2 { get; set; }
        public double kenar3 { get; set; }

        public Ucgen(double kenar1,double kenar2,double kenar3)
        {
            this.kenar1 = kenar1;
            this.kenar2 = kenar2;
            this.kenar3 = kenar3;
        }

        public double alanHesapla()  => Math.Sqrt((cevreHesapla() / 2) * ((cevreHesapla() / 2) - kenar1) * ((cevreHesapla() / 2) - kenar2) * ((cevreHesapla() / 2) - kenar3));

        public double cevreHesapla() => kenar1+kenar2+kenar3;
       


    }
}
