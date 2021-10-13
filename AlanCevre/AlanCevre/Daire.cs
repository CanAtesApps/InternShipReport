using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanCevre
{
    public class Daire : ICalculate
    {
        public double yaricap { get; set; }

        public Daire(double yaricap)
        {
            this.yaricap = yaricap;
        }

        public double alanHesapla()  => Math.PI * yaricap * yaricap;

        public double cevreHesapla() => 2*Math.PI*yaricap;


    }
}
