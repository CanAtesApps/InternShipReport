using System;
using System.Collections;
using System.Collections.Generic;

namespace Pascal
{
    class Program
    {
        static void Main(string[] args)
        {
            //en alttaki yarısı kadar bosluk
            //sıra
            //max bosluk
            //max bosluk--
            int max;
        input:
            Console.WriteLine("Pascal ucgeni kac satır olucak : ");
            if(!int.TryParse(Console.ReadLine(), out max))
            {
                Console.WriteLine("Sayi girin");
                goto input;
            }
            if(max <=0)
            {
                Console.WriteLine("0 dan büyük bir sayi girin");
                goto input;
            }
            int bosluk = max-1;
            for (int i = 0; i < max; i++)
            {
                int baslangıc = 1;
                for (int j = 0; j < (bosluk  ); j++)
                {
                    Console.Write("  ");
                }
                for (int k = 0; k <= i; k++)
                {
                    Console.Write("  "+baslangıc+" ");
                    baslangıc = baslangıc * (i - k) / (k + 1);
                }
                for (int j = 0; j < bosluk; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine();
                bosluk--;
            }
        }
    }
}
