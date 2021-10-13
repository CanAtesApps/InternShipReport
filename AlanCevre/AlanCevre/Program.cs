using System;

namespace AlanCevre
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu;
            string hesaplama;
            while(true)
            {
                Console.WriteLine("1 -> Hesaplama\n2 -> Çıkış");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        //etiket
                        hesap:
                        hesaplama = "";
                        Console.WriteLine("1 -> Alan\n2 -> Çevre");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                hesaplama = "alan";
                                break;
                            case "2":
                                hesaplama = "çevre";
                                break;
                            default:
                                Console.WriteLine("Hatalı hesaplama tipi secimi");
                                goto hesap;
                        }
                        secim:
                        Console.WriteLine("1-> Ucgen\n2-> Kare\n3-> Dikdortgen\n4-> Daire");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                Ucgen sekil;
                            ucgentipi:
                                Console.WriteLine("Ucgen tipi");
                                Console.WriteLine("1-> Eşkenar\n2-> İkizkenar\n3-> Çeşitkenar");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                    eskenar:
                                        Console.WriteLine("Eşkenar ucgen kenar uzunlugu girin");
                                        double tmp;
                                        if (!double.TryParse(Console.ReadLine(), out tmp))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto eskenar;
                                        }
                                        if (tmp <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto eskenar;
                                        }
                                        sekil = new Ucgen(tmp, tmp, tmp);
                                        if (hesaplama.Equals("alan"))
                                        {
                                            Console.WriteLine("Eşkenar ücgenin alanı : " + sekil.alanHesapla());
                                        }
                                        else
                                            Console.WriteLine("Eşkenar ücgenin cevresi : " + sekil.cevreHesapla());
                                        break;
                                    case "2":
                                        double kenar1, kenar2;
                                    kenarAlma1:
                                        Console.WriteLine("İkizkenar ucgenin eşit kenar uzunluklarını girin");
                                        if (!double.TryParse(Console.ReadLine(), out kenar1))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto kenarAlma1;
                                        }
                                        if (kenar1 <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto kenarAlma1;
                                        }
                                    kenarAlma2:
                                        Console.WriteLine("İkizkenar üçgenin eşit olmayan kenar uzunlugunu girin");
                                        if (!double.TryParse(Console.ReadLine(), out kenar2))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto kenarAlma2;
                                        }
                                        if (kenar2 <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto kenarAlma2;
                                        }
                                        if (kenar1 * 2 <= kenar2)
                                        {
                                            Console.WriteLine("Geçersiz üçgen!  tekrar girin");
                                            goto kenarAlma1;
                                        }
                                        sekil = new Ucgen(kenar1, kenar1, kenar2);
                                        if (hesaplama.Equals("alan"))
                                        {
                                            Console.WriteLine("İkizkenar ücgenin alanı : " + sekil.alanHesapla());
                                        }
                                        else
                                            Console.WriteLine("ikizkenar ücgenin cevresi : " + sekil.cevreHesapla());
                                        break;
                                    case "3":
                                        double ceskenar1, ceskenar2, ceskenar3;
                                    ceskenarAlma1:
                                        Console.WriteLine("Çeşitkenar ucgenin 1. kenar uzunluğunu girin");
                                        if (!double.TryParse(Console.ReadLine(), out ceskenar1))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto ceskenarAlma1;
                                        }
                                        if (ceskenar1 <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto ceskenarAlma1;
                                        }
                                    ceskenarAlma2:
                                        Console.WriteLine("Çeşitkenar üçgenin 2. kenar uzunlugunu girin");
                                        if (!double.TryParse(Console.ReadLine(), out ceskenar2))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto ceskenarAlma2;
                                        }
                                        if (ceskenar2 <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto ceskenarAlma2;
                                        }
                                    ceskenarAlma3:
                                        Console.WriteLine("Çeşitkenar üçgenin 3. kenar uzunlugunu girin");
                                        if (!double.TryParse(Console.ReadLine(), out ceskenar3))
                                        {
                                            Console.WriteLine("Sayı girin");
                                            goto ceskenarAlma3;
                                        }
                                        if (ceskenar3 <= 0)
                                        {
                                            Console.WriteLine("0 dan kücük ve 0 olamaz");
                                            goto ceskenarAlma3;
                                        }
                                        if (ceskenar1 + ceskenar2 <= ceskenar3 || ceskenar1 + ceskenar3 <= ceskenar2 || ceskenar2 + ceskenar3 <= ceskenar1)
                                        {
                                            Console.WriteLine("Geçersiz üçgen!  tekrar girin");
                                            goto ceskenarAlma1;
                                        }
                                        else
                                            sekil = new Ucgen(ceskenar1, ceskenar1, ceskenar2);
                                        if (hesaplama.Equals("alan"))
                                        {
                                            Console.WriteLine("Çeşitkenar ücgenin alanı : " + sekil.alanHesapla());
                                        }
                                        else
                                            Console.WriteLine("Çeşitkenar ücgenin cevresi : " + sekil.cevreHesapla());
                                        break;
                                    default:
                                        Console.WriteLine("Hatalı ucgen tipi secimi");
                                        goto ucgentipi;
                                }
                                break;
                            case "2":
                                double karekenar;
                            kareKenar:
                                Console.WriteLine("Karenin kenar uzunlugu girin");
                                if (!double.TryParse(Console.ReadLine(), out karekenar))
                                {
                                    Console.WriteLine("Sayı girin");
                                    goto kareKenar;
                                }
                                if (karekenar <= 0)
                                {
                                    Console.WriteLine("0 dan kücük ve 0 olamaz");
                                    goto kareKenar;
                                }
                                Kare kar = new Kare(karekenar);
                                if (hesaplama.Equals("alan"))
                                {
                                    Console.WriteLine("Karenin alanı : " + kar.alanHesapla());
                                }
                                else
                                    Console.WriteLine("Karenin cevresi : " + kar.cevreHesapla());
                                break;
                            case "3":
                                double dikkenar1, dikkenar2;
                                dikKenar1:
                                Console.WriteLine("Dikdörtgenin 1. kenar uzunlugu girin");
                                if (!double.TryParse(Console.ReadLine(), out dikkenar1))
                                {
                                    Console.WriteLine("Sayı girin");
                                    goto dikKenar1;
                                }
                                if (dikkenar1 <= 0)
                                {
                                    Console.WriteLine("0 dan kücük ve 0 olamaz");
                                    goto dikKenar1;
                                }
                                dikKenar2:
                                Console.WriteLine("Dikdörtgenin 2. kenar uzunlugu girin");
                                if (!double.TryParse(Console.ReadLine(), out dikkenar2))
                                {
                                    Console.WriteLine("Sayı girin");
                                    goto dikKenar2;
                                }
                                if (dikkenar2 <= 0)
                                {
                                    Console.WriteLine("0 dan kücük ve 0 olamaz");
                                    goto dikKenar2;
                                }
                                Dikdortgen dort = new Dikdortgen(dikkenar1, dikkenar2);
                                if (hesaplama.Equals("alan"))
                                {
                                    Console.WriteLine("Dikdortgenin alanı : " + dort.alanHesapla());
                                }
                                else
                                    Console.WriteLine("Dikdortgenin cevresi : " + dort.cevreHesapla());
                                break;
                            case "4":
                                double yaricap;
                            yariCap:
                                Console.WriteLine("dairenin yaricap uzunlugu girin");
                                if (!double.TryParse(Console.ReadLine(), out yaricap))
                                {
                                    Console.WriteLine("Sayı girin");
                                    goto yariCap;
                                }
                                if (yaricap <= 0)
                                {
                                    Console.WriteLine("0 dan kücük ve 0 olamaz");
                                    goto yariCap;
                                }
                                Daire dar = new Daire(yaricap);
                                if (hesaplama.Equals("alan"))
                                {
                                    Console.WriteLine("Dairenin alanı : " + dar.alanHesapla());
                                }
                                else
                                    Console.WriteLine("Dairenin cevresi : " + dar.cevreHesapla());
                                break;
                            default:
                                Console.WriteLine("Hatalı çokgen secimi");
                                goto secim;
                        }
                        break;
                    case "2":
                        Console.WriteLine("Çıkış yapıldı.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Hatalı ana menu girişi");
                        break;
                }
            }

        }
    }
}
