using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotHesapla
{
    public class Ders : IHarfNotuHesapla
    {
        //variables
        public string name { get; set; }

        private List<Not> scores;

        public bool pass { get; set; }

        public Ders(string name)
        {
            this.name = name;
            scores = new List<Not>();
        }
        //adds new score to the list
        public void add(double not,double percentege)
        {
            scores.Add(new Not(not,percentege));
        }
        
        //Calcutes total score from score and score percentage
        public double calculate(Not scr)
        {
            return scr.score * scr.percentage / 100;
        }

        //returns if pass or fail string
        public string passFail()
        {
            return (pass) ? "Pass" : "Fail";
        }
        
        //Calculate grade and determines if pass or fail
        public string harfnotu()
        {
            double total = 0;
            foreach (Not item in scores)
            {
                total += calculate(item);
            }
            if(90<=total && total <= 100)
            {
                pass = true;
                return "Your grade point average : "+ total +" Letter grade : AA";
            }
            else if (85 <= total && total < 90)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : BA";
            }
            else if (80 <= total && total < 85)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : BB";
            }
            else if (75 <= total && total < 80)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : CB";
            }
            else if (70 <= total && total < 75)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : CC";
            }
            else if (65 <= total && total < 70)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : DC";
            }
            else if (60 <= total && total < 65)
            {
                pass = true;
                return "Your grade point average : " + total + " Letter grade : DD";
            }
            else
            {
                pass = false;
                return "Your grade point average : " + total + " Letter grade : FF";
            }
        }
        //returns true if all given percenteges add up to 100
        public bool validPercentegeInput()
        {
            double tmp = 0;
            foreach (Not item in scores)
            {
                tmp += item.percentage;
            }
            return (tmp == 100) ? true : false;
        }
        public void clearList()
        {
            scores.Clear();
        }
        public string printRemainingPercentege()
        {
            double tmp = 0;
            foreach (Not item in scores)
            {
                tmp += item.percentage;
            }
            return "Reamining percentage : " + (100 - tmp);
        }
        public double remainingPercentege()
        {
            double tmp = 0;
            foreach (Not item in scores)
            {
                tmp += item.percentage;
            }
            return  (100 - tmp);
        }
    }
}
