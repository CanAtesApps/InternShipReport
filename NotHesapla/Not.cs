using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotHesapla
{
    public class Not
    {
        public double score { get; set; }
        public double percentage { get; set; }

        public Not(double score,double percentage)
        {
            this.score = score;
            this.percentage = percentage;
        }
    }
}
