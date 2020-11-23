//efrat fried
//tamar packter

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dotNet_02_5781_2431_5820.git
{
    class Program
    {


        static void Main(string[] args)
        {
            List<BusLine> lineBuses = new List<BusLine>();  //list of all the busses's lines.
           List<BusStop> busStops = new List<BusStop>();  //list of all the exist stations

            Console.WriteLine("Welcome to our bus's control system .");
            Console.WriteLine("Please choose your request :");
            Console.WriteLine("Please press 1 to add a new bus line or a new bus station. ");
            Console.WriteLine("Please press 2 to delete a bus line or a bus station.");
            Console.WriteLine("Please press 3 to search for the station's lines. ");
            Console.WriteLine("Please press 4 to print all the exist line in the system or for printing all the pass by stations & lines. ");
            Console.WriteLine("Please press 0 to exit the system.");

            int num;

            BusLine Line1 = new BusLine();
            BusStop station1 = new BusStop();
            do
            {
                Console.WriteLine("Enter your choice :");

                while (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("wrong number!!! enter again:");
                }

                Options ch = (Options)num;
                switch (ch)
                {
                    case Options.add:

                        Console.WriteLine("If you want to add a new bus's line press 1");  //to add a new bus line.
                        Console.WriteLine("If you want to add a new bus's station press 0");//to add a new bus stop
                        bool flag;
                        string ans = Console.ReadLine();

                        flag = System.Convert.ToBoolean(ans);
                        if (!flag)
                        {
                            Console.WriteLine("You want to add a new bus's station");
                            string StatioNum;
                            StatioNum = Console.ReadLine();//get the station's num code 
                            station1.setStationNum(StatioNum);
                            while (!(ValidStation(List < BusStop > station1)))
                            {
                                Console.WriteLine("Do you want to try again?");
                                Console.WriteLine("If you do press 1");
                                string answer = Console.ReadLine();
                                StatioNum = Console.ReadLine();
                                station1.CodeStation(StatioNum);
                                //station1.setStationNum(StatioNum);
                            }
                            string Latitude;
                            Latitude = Console.ReadLine();
                            station1.setStationLocation(Latitude);

                            string Longitude;
                            Longitude = Console.ReadLine();
                            station1.setStationLocation(Longitude);

                            string Adress;
                            Adress = Console.ReadLine();
                            station1.setStationAdress(Adress);

                            Console.WriteLine("Added");
                        }

                        else
                        {
                            Console.WriteLine("You want to add a new bus's line");

                            string NumLine;
                            NumLine = Console.ReadLine();
                            Line1.setLineNum(NumLine);

                            string StartSt;
                            StartSt = Console.ReadLine();
                            Line1.setStartStation(StartSt);

                            string FinishSt;
                            FinishSt = Console.ReadLine();
                            Line1.setFinishStation(FinishSt);

                            string Area;
                            Area = Console.ReadLine();
                            Line1.setLineArea(Area);

                            Console.WriteLine("Added");
                        }
                        break;

                    case Options.delete:

                        Console.WriteLine("If you want to delete a bus's line press 1");
                        Console.WriteLine("If you want to delete a bus's station press 0");
                        bool flag;
                        string ans = Console.ReadLine();

                        flag = System.Convert.ToBoolean(ans);
                        if (!flag)
                        {
                            Console.WriteLine("you want to delete a bus's station);
                            while (!(ValidStation(List < BusStop > station1)))
                            {
                                Console.WriteLine("Do you want to try again?");
                                Console.WriteLine("If you do press 1");
                                string answer = Console.ReadLine();
                                StatioNum = Console.ReadLine();
                                station1.CodeStation(StatioNum);
                                //station1.setStationNum(StatioNum);
                            }
                            ans=RemoveStop(StatioNum);
                        }
                        else
                        {
                            Console.WriteLine("you want to delete a bus's line);
                        }
                        break;

                    case Options.search:
                        Console.WriteLine("If you want to search for the lines which passing the station press 1");
                        Console.WriteLine("If you want to search for option of driving between 2 stations press 0");
                          bool flag;
                        string ans = Console.ReadLine();

                        flag = System.Convert.ToBoolean(ans);
                        if (!flag)
                        {
                            Console.WriteLine("you want to print the driving path between two stations);
                        }
                        else
                        {
                            Console.WriteLine("you want to search for the passing by lines in a certain station);
                        }
                        break;

                    case Options.print:
                        Console.WriteLine("If you want to print all the existing lines press 1");
                        Console.WriteLine("If you want to print all the stations & the lines passing them press 0");
                         bool flag;
                        string ans = Console.ReadLine();

                        flag = System.Convert.ToBoolean(ans);
                        if (!flag)
                        {
                            Console.WriteLine("you want to print all the stations & the lines passing them);
                        }
                        else
                        {
                            Console.WriteLine("you want to print all the existing lines");
                        }
                        break;

                    case Options.exit:
                        Console.WriteLine("Have a nice day");
                        break;
                }

            } while (num != 0);
        }
    }
}
