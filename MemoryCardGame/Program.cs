using System;

namespace MemoryCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables and declerations
            Random rnd = new Random();
            int randomInt;
            bool game = true;
            int input1,input2;
            int run1i, run1j;
            int run2i, run2j;
            int fill = 1;
            int moveNumber = 0;
            int pairFound  = 0;
            //16 0 TO 15 RANDOM
            string[] cards = {"A", "B", "C", "D" , "E", "F", "G", "H" , "A", "B", "C", "D" , "E", "F", "G", "H"};
            string[,] gameBoardShow = new string[4,4];
            string[,] gameBoardData = new string[4,4];;
            
            //fills arrays
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    RastgeleIntAlma:
                    randomInt = rnd.Next(16);
                    if(cards[randomInt].Equals("0"))
                        goto RastgeleIntAlma;
                    gameBoardData[i,j] = cards[randomInt];
                    cards[randomInt] = "0";
                    gameBoardShow[i,j] = (fill++)+"";
                }
            }
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while(game)
            {
                if(pairFound== 8)
                {
                    watch.Stop();
                    Console.WriteLine("Oyun bitti");
                    Console.WriteLine("Toplam adım sayısı : " + moveNumber);
                    Console.WriteLine($"Oyun süresi: {watch.ElapsedMilliseconds * 1.66666667 /100000} dakika");
                    Environment.Exit(0);
                }
                //print board
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if(i<2 )
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else if ( i == 2 && j == 0)
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else if(!int.TryParse(gameBoardShow[i,j],out int tmp))
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else
                            Console.Write("| "+gameBoardShow[i,j] + " | ");
                        
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------");
                
                //inputs and control
                Input1:
                Console.WriteLine("Lutfen 1. kartı seciniz >> ");
                if (!int.TryParse(Console.ReadLine(), out input1))
                {
                    Console.WriteLine("Lütfen geçeri bir kart girin");
                    goto Input1;
                }
                if (input1 <= 0 || input1 >16)
                {
                    Console.WriteLine("Lütfen geçeri bir kart girin");
                    goto Input1;
                }
                Input2:
                Console.WriteLine("Lutfen 2. kartı seciniz >> ");
                if (!int.TryParse(Console.ReadLine(), out input2))
                {
                    Console.WriteLine("Lütfen geçeri bir kart girin");
                    goto Input2;
                }
                if (input2 <= 0 || input2 >16)
                {
                    Console.WriteLine("Lütfen geçeri bir kart girin");
                    goto Input2;
                }
                
                moveNumber++;

                if(input1 % 4 != 0)
                {
                    run1i = input1/4;
                    run1j = input1%4-1;
                }
                else
                {
                    run1i=input1/4 -1;
                    run1j=3;
                }

                if(input2 % 4 != 0)
                {
                    run2i = input2/4;
                    run2j = input2%4-1;
                }
                else
                {
                    run2i=input2/4 -1;
                    run2j=3;
                }
                
                gameBoardShow[run1i,run1j] = gameBoardData[run1i,run1j];
                gameBoardShow[run2i,run2j] = gameBoardData[run2i,run2j];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if(i<2 )
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else if ( i == 2 && j == 0)
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else if(!int.TryParse(gameBoardShow[i,j],out int tmp))
                            Console.Write("| "+gameBoardShow[i,j] + "  | ");
                        else
                            Console.Write("| "+gameBoardShow[i,j] + " | ");
                        
                    }
                    Console.WriteLine();
                }
                if(!gameBoardData[run1i,run1j].Equals(gameBoardData[run2i,run2j]))
                {
                    gameBoardShow[run1i,run1j] = input1+"";
                    gameBoardShow[run2i,run2j] = input2+"";
                }
                else
                    pairFound++;                
                Console.WriteLine("--------------------------------");

            }
        }
    }
}
