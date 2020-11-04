// efrat fried 322552431
//tamat packter 212355820

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5781_2431_5820
{
    public enum BChoice { a, b, c, d, e };
    class Program
    {
        static Random rand = new Random(DateTime.Now.Millisecond);
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your choice :");
            Console.WriteLine("please press 0 to add a bus to the BusLiist");
            Console.WriteLine("please press 1 to choose a bus for the drive.");
            Console.WriteLine("please press 2 to choose treatment or refuel gas the bus.");
            Console.WriteLine("please press 3 to Print the data of all the busses.");
            Console.WriteLine("please press 4 tO exit.");

            int num;

            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("wrong number!!! enter again:");
            }
            BChoice ch = (BChoice)num;
            DateTime start = new DateTime();
            List<Bus> busses = new List<Bus>();

            while (ch != BChoice.e) //If the selection is equal to 4 exit the loop.
            {
                Bus bus1 = new Bus();


                switch (ch)
                {
                    //to add a new vehicle
                    case BChoice.a:

                        bool succes = false;
                        while (succes == false)  // Performs a check to see if the input is correct.
                        {
                            Console.WriteLine("Please enter the start date of the work");
                            succes = DateTime.TryParse(Console.ReadLine(), out start);
                            if (succes == false)
                            {
                                Console.WriteLine("ERROR");
                            }

                        }
                        bus1.setbeganToWork(start);

                        Console.WriteLine("Please enter the bus's license number:");
                        if (((bus1.getbeganToWork()).Year) > 2018 || ((bus1.getbeganToWork()).Year) == 2018)//How many numbers should a license number have

                        {
                            string str = Console.ReadLine();
                            while (str.Length != 8)
                            {
                                Console.WriteLine("ERROR");
                                str = Console.ReadLine();
                            }
                            bus1.setLicenseNum(str);
                        }
                        else
                        {
                            string str = Console.ReadLine();
                            while (str.Length != 7)
                            {
                                Console.WriteLine("ERROR");
                                str = Console.ReadLine();
                            }
                            bus1.setLicenseNum(str);
                        }
                        busses.Add(bus1);
                        Console.WriteLine("Added");
                        break;
                
                    //choosing a bus to drive
                    case BChoice.b:
                        string license;
                        Console.WriteLine("Please enter the bus's license number:");
                        license = Console.ReadLine();
                        while ((license.Length != 7) && (license.Length != 8))//Performs a check to see if the input is correct.
                        {

                            Console.WriteLine("ERROR");
                            license = Console.ReadLine();
                        }
                }
            }