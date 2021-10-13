using System;

namespace NotHesapla
{
    class Program
    {
        static void Main(string[] args)
        {
            //veriables
            bool loop = true;
            string input;
            double score;
            double percentege  ;

            while(loop)
            {
                Console.WriteLine("H,h -> Calculate Grade\nE,e -> Exit");
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "h":
                       
                        int howMany;
                        bool validInput = false;
                        string tmpS;
                        Ders tmp;
                        bool validPercentageInput = false;
                        percentege = 0;
                        //Course name cannot be empty
                        Console.WriteLine("Enter course name");
                        tmpS = Console.ReadLine();
                        while (tmpS.Equals(""))
                        {
                            Console.WriteLine("Name cannot be empty. Enter Again!");
                            tmpS = tmpS = Console.ReadLine();
                        }
                        tmp = new Ders(tmpS);

                        //getting howmany score will be required
                        Console.WriteLine("How many scores will be entered ");
                        while (!int.TryParse(Console.ReadLine(), out howMany))
                        {
                            Console.WriteLine("Enter only number");
                        }

                        while (!validInput)
                        { 
                            //getting score input
                            for(int i = 0; i<howMany; i++)
                            {
                                Console.WriteLine("Enter Score(" + (i+1) + ")");
                                while (!double.TryParse(Console.ReadLine(), out score))
                                {
                                    Console.WriteLine("Enter only number");
                                }
                                
                                //checking  if score is in between 0-100 
                                if (score > 100 || score < 0)
                                {
                                    Console.WriteLine("Score cannot be higher than 100 or lower than 0");
                                    i--;
                                    continue;
                                }
                                while (!validPercentageInput)
                                {
                                    Console.WriteLine("Enter score percentage(" + (i + 1) + ") without \"%\" " + tmp.printRemainingPercentege());
                                    while (!double.TryParse(Console.ReadLine(), out percentege))
                                    {
                                        Console.WriteLine("Enter only number");
                                    }

                                    //checking  if percentage is in between 0-100 
                                    if (percentege > 100 || percentege < 0 || percentege > tmp.remainingPercentege())
                                    {
                                        Console.WriteLine("Percentage cannot be higher than 100 or lower than 0 or remaining percentege");
                                        continue;
                                    }
                                    validPercentageInput = true;
                                }
                                tmp.add(score, percentege);
                                validPercentageInput = false;
                            }

                            //checking if given percentages add up to 100
                            if (!tmp.validPercentegeInput())
                            {
                                Console.WriteLine("Given percentages does not add up to 100 ! Enter again");
                                tmp.clearList();
                            }
                            else
                                validInput = true;
                        }

                        Console.WriteLine(tmp.harfnotu() + " " + tmp.passFail());
                        break;
                    case "e":
                        Console.WriteLine("Exiting");
                        Environment.Exit(0);
                        break;
                   
                    //invalid menu input
                    default:
                        Console.WriteLine("Invalid menu input");
                        break;
                }
            }
        }
    }
}
