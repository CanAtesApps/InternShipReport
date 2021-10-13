using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanCevre
{
    public class Dikdortgen : ICalculate
    {
        public double kenar1 { get; set; }
        public double kenar2 { get; set; }

        public Dikdortgen(double kenar1,double kenar2)
        {
            this.kenar1 = kenar1;
            this.kenar2 = kenar2;
        }

        public double alanHesapla()  => kenar1 * kenar2;

        public double cevreHesapla() =>( (kenar1 * 2) + ( kenar2 * 2) );

    }
}
