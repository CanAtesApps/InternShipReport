using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotHesapla
{
    public interface IHarfNotuHesapla
    {

        public double calculate(Not score);
        public string harfnotu();
        public string passFail();
        public bool validPercentegeInput();
    }
}
