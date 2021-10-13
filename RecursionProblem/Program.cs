using System;
using System.Collections;
using System.Collections.Generic;

namespace problem
{
    public class Yol
    {
        public int id { get; set; }
        public int startx { get; set; }
        public int endx { get; set; }
        public int starty { get; set; }
        public int endy { get; set; } 
        public Yol (int id , int xstart , int ystart,int xend , int yend)
        {
            this.id = id;
            startx = xstart;
            endx = xend;
            starty = ystart;
            endy = yend;
        }
        public Yol ()
        {
        }

        //selected end = sagdakinin start
        public bool ayniYondeMiSagaGit(Yol selected)
        {
            return (this.startx == selected.endx && this.starty == selected.endy);
        }

        //selected end = sagdakinin end
        public bool tersYondeMiSagaGit(Yol selected)
        {
            return (this.endx == selected.endx && this.endy == selected.endy);
        }

        //selected start = soldakinin end
        public bool ayniYondeMiSolaGit(Yol selected)
        {
            return (this.endx == selected.startx && this.endy == selected.starty);
        }

        //selected start = soldakinin start
        public bool tersYondeMiSolaGit(Yol selected)
        {
            return (this.startx == selected.startx && this.starty == selected.starty);
        }

        // this in start veya end == selected end
        public bool sagdaVarmi(Yol selected)
        {
            bool checkStart = this.startx == selected.endx && this.starty == selected.endy ;
            bool checkEnd   = this.endx   == selected.endx && this.endy   == selected.endy ;
            bool checkID    = this.id     != selected.id;

            return (checkStart || checkEnd) && checkID ;
        }

        //this in start veya end  === selected start  
        public bool soldaVarmi(Yol selected)
        {
            bool checkStart = this.startx == selected.startx && this.starty == selected.starty ;
            bool checkEnd   = this.endx   == selected.startx && this.endy   == selected.starty ;
            bool checkID    = this.id     != selected.id;

            return (checkStart || checkEnd) && checkID ;
        }
    }
    public class Program
    {
        public Yol sagaGit(List<Yol> source,List<Yol> output,Yol selected)
        {
            Yol tmp = new Yol();
            foreach (var item in source)
            {
                if(item.sagdaVarmi(selected))
                    {
                    if( item.ayniYondeMiSagaGit(selected))
                    {
                        tmp.id = item.id;
                        tmp.startx = item.startx;
                        tmp.starty = item.starty;
                        tmp.endx = item.endx;
                        tmp.endy = item.endy;
                        return tmp;
                        
                    }
                    else if(item.tersYondeMiSagaGit(selected))
                    {
                        output.Add(item);
                        tmp.id = item.id;
                        tmp.startx = item.endx;
                        tmp.starty = item.endy;
                        tmp.endx = item.startx;
                        tmp.endy = item.starty;
                        return tmp;
                    }
                }
            }
            
            return null;

        }
        public Yol solaGit(List<Yol> source,List<Yol> output,Yol selected)
        {
            Yol tmp = new Yol();
            foreach (var item in source)
            {
                if( item.soldaVarmi(selected) )
                {
                    if( item.ayniYondeMiSolaGit(selected))
                    {
                        tmp.id = item.id;
                        tmp.startx = item.startx;
                        tmp.starty = item.starty;
                        tmp.endx = item.endx;
                        tmp.endy = item.endy;
                        return tmp;
                        
                    }
                    else if( item.tersYondeMiSolaGit(selected) )
                    {
                        output.Add(item);
                        tmp.id = item.id;
                        tmp.startx = item.endx;
                        tmp.starty = item.endy;
                        tmp.endx = item.startx;
                        tmp.endy = item.starty;
                        return tmp;
                    }
                }
            }
            
            return null;

        }
        public void tersYollar(List<Yol> source,List<Yol> output,Yol selected)
        {
           
            //sol recursion
            //sag recursion
            solRecursive(source,output,selected);
            sagRecursive(source,output,selected);

        }
        public void solRecursive(List<Yol> source,List<Yol> output,Yol selected)
        {
            Yol tmp = solaGit(source,output,selected);
            if(tmp == null )
                return;
            else
                solRecursive(source,output,tmp);

        }
        public void sagRecursive(List<Yol> source,List<Yol> output,Yol selected)
        {
            Yol tmp = sagaGit(source,output,selected);
            if(tmp == null )
                return;
            else
                sagRecursive(source,output,tmp);

        }
        public static void Main(string[] args)
        {
            Program a = new Program();
            Console.WriteLine("Hello World!");
            List<Yol> noktalar = new List<Yol>();
            List<Yol> output = new List<Yol>();
            // id  - startx- endx - start y - end y
            noktalar.Add(new Yol(1,0,1,3,5));
            noktalar.Add(new Yol(2,0,1,5,7));
            noktalar.Add(new Yol(3,3,0,5,7));
            noktalar.Add(new Yol(4,6,5,3,0));
            noktalar.Add(new Yol(5,6,5,7,8));
            
            noktalar.Add(new Yol(6,32,2,3,5));
            noktalar.Add(new Yol(7,32,2,46,-11));

            Yol selectedYol = new Yol(2,0,1,5,7);

            a.tersYollar(noktalar,output,selectedYol);
            foreach (var item in output)
            {
                Console.WriteLine(item.id);
            }
        }
    }
}
