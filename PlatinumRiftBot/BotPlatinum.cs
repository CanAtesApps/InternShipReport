
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
 //Container class for storing hexa zone informations
 public class Zone
 {
    public int id {get;set;}
    public int platinumSource {get;set;}
    public int ownerID;
    public List<int> links {get;set;}
    public Zone(int id,int platinumSource)
    {
        this.id = id;
        this.platinumSource = platinumSource;
        links = new List<int>();
    }
 }

 //main class
class Player
{
    static void Main(string[] args)
    {
        Random rnd  = new Random();
        string[] inputs;
        List<Zone> allZones = new List<Zone>();
        
        //getting inputs for game settings
        inputs = Console.ReadLine().Split(' ');
        int playerCount = int.Parse(inputs[0]); // the amount of players (2 to 4)
        int myId = int.Parse(inputs[1]); // my player ID (0, 1, 2 or 3)
        int zoneCount = int.Parse(inputs[2]); // the amount of zones on the map
        int linkCount = int.Parse(inputs[3]); // the amount of links between all zones
        
        //Creating zones
        for (int i = 0; i < zoneCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int zoneId = int.Parse(inputs[0]); // this zone's ID (between 0 and zoneCount-1)
            int platinumSource = int.Parse(inputs[1]); // the amount of Platinum this zone can provide per game turn
            allZones.Add(new Zone(zoneId,platinumSource));
        }
        
        //Creating links
        for (int i = 0; i < linkCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int zone1 = int.Parse(inputs[0]);
            int zone2 = int.Parse(inputs[1]);
            foreach(Zone x in allZones)
            {
                if(x.id == zone1)
                {
                    x.links.Add(zone2);
                }
                if(x.id == zone2)
                {
                    x.links.Add(zone1);
                }
            }
        }

        
        List<int> myZones = new List<int>();
        int rndZone;
        // game loop
        while (true)
        {

            int platinum = int.Parse(Console.ReadLine()); // my available Platinum
            for (int i = 0; i < zoneCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int zId = int.Parse(inputs[0]); // this zone's ID
                int ownerId = int.Parse(inputs[1]); // the player who owns this zone (-1 otherwise)
                int podsP0 = int.Parse(inputs[2]); // player 0's PODs on this zone
                int podsP1 = int.Parse(inputs[3]); // player 1's PODs on this zone
                int podsP2 = int.Parse(inputs[4]); // player 2's PODs on this zone (always 0 for a two player game)
                int podsP3 = int.Parse(inputs[5]); // player 3's PODs on this zone (always 0 for a two or three player game)
                allZones[i].ownerID = ownerId;
            }
         
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // first line for movement commands, second line for POD purchase (see the protocol in the statement for details)
            
            if(platinum>20)
            {
               for(int i = 0 ;i<myZones.Count;i++)
                {
                    int rndmNext = myZones[i] ;
                    foreach(Zone x in allZones)
                    {
                        if(myZones[i] == x.id)
                        {
                            
                            rndmNext = x.links[rnd.Next(x.links.Count)];
                        }
                    }
                    Console.Write("1 " + myZones[i] + " " + (rndmNext) + " ");
                    myZones[i] = rndmNext;
                }
                Console.WriteLine();
                while(platinum > 0)
                {
                    random:
                    rndZone = rnd.Next(zoneCount);
                    foreach(Zone x in allZones)
                    {
                        if(x.id == rndZone)
                        {
                            if(x.id == 1)
                            {
                                goto random;
                            }
                            else
                                break;
                        }
                    }
                    Console.Write("1 "+rndZone+" ");
                    foreach(Zone x in allZones)
                    {
                        if(x.id == rndZone)
                        {
                            myZones.Add(x.id);
                        }
                    }
                    platinum -= 20;
                }
                Console.WriteLine();
            }
            else
            {
                for(int i = 0 ;i<myZones.Count;i++)
                {
                    int rndmNext = myZones[i] ;
                    foreach(Zone x in allZones)
                    {
                        if(myZones[i] == x.id)
                        {
                            
                            rndmNext = x.links[rnd.Next(x.links.Count)];
                        }
                    }
                    Console.Write("1 " + myZones[i] + " " + (rndmNext) + " ");
                    myZones[i] = rndmNext;
                }
                Console.WriteLine("\nWAIT");
            }
        }
    }
}
