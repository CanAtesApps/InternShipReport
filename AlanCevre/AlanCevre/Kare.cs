using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanCevre
{
    public class Kare : ICalculate
    {
        public double kenar1 { get; set; }
        
        public Kare(double kenar1)
        {
            this.kenar1 = kenar1;
        }

        public double alanHesapla()  =>kenar1*kenar1;

        public double cevreHesapla() => kenar1 * 4;

    }
}
