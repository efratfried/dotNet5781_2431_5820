//efrat fried 322552431
//tamat packter 212355820

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace dotNet_01_5781_2431_5820
{
    class Program
    {
        static Random rand = new Random(DateTime.Now.Millisecond);
        static void Main(string[] args)
        {
            Console.WriteLine("welcome yo our bus control system");
            Console.WriteLine("please press 1 to add a bus to the BusLiist");
            Console.WriteLine("please press 2 to choose a bus for the drive.");
            Console.WriteLine("please press 3 to choose treatment or refuel gas the bus.");
            Console.WriteLine("please press 4 to Print the data of all the busses.");
            Console.WriteLine("please press 0 tO exit.");
            int Num;
            bool Succes;
            DateTime Start = new DateTime();
            List<Bus> Busses = new List<Bus>();
            do
            {
                Console.WriteLine("Enter your choice :");

                while (!int.TryParse(Console.ReadLine(), out Num))
                {
                    Console.WriteLine("wrong number!!! enter again:");
                }
                BChoice Ch = (BChoice)Num;
                Bus Bus1 = new Bus();


                    switch (Ch)
                    {
                        //to add a new vehicle by checking the validation
                        case BChoice.b:
                        bool Flag= Bus1.AddBus(Busses,Start);//if able to add
                       if (Flag)//add it to the busses list
                            {
                            Busses.Add(Bus1);
                            Console.WriteLine("Added");
                            }
                        else
                        {
                            Console.WriteLine("Bus was'nt Added");
                        }
                        break;

                        //choosing a bus to drive
                        case BChoice.c:

                            string License;
                            if (!Busses.Any())
                            {
                                Console.WriteLine("There are no busses yet");
                            }
                            else
                            {
                                Console.WriteLine("Please enter the bus's license number:");
                                License = Console.ReadLine();
                                if ((License.Length != 7) && (License.Length != 8))//Performs a check to see if the input is correct.
                                {
                                    Console.WriteLine("ERROR");
                                }
                                else
                                {
                                    {

                                        Succes = false;
                                        DateTime CurrentTime = DateTime.Now;
                                        foreach (Bus i in Busses)//Go through each bus on the list to see if the selected bus can make the trip.
                                        {
                                            if (i.getLicenseNum() == License)
                                            {
                                                Succes = true;
                                                int Km = rand.Next(1200);//How many kilometers will the trip be (up to 1200).
                                                if ((i.getkmToTritment() < 20000) && (i.getfuel() - Km >= 0) && (i.getlastTritment() >= CurrentTime.AddYears(-1)))//Go through each bus on the list to see if the selected bus can make the trip
                                                {
                                                    i.setkmToTritment(i.getkmToTritment() + Km);
                                                    i.setfuel(i.getfuel() - Km);
                                                    i.settotalKm(i.gettotalKm() + Km);
                                                    Console.WriteLine("The drive is possible");
                                                    Console.WriteLine("The amount of Km is " + Km);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The drive is impossible");//If the bus cannot travel it prints an appropriate message
                                                }
                                            }

                                        }
                                        if (Succes == false)
                                        {
                                            Console.WriteLine("The license number was'nt found");//If the bus does not exist it prints an appropriate message.
                                        }
                                    }
                                }
                            }
                            break;

                        //checking if there is ant necessary to refuel gas or to treatment the vehicle.
                        case BChoice.d:

                            Console.WriteLine("Enter the bus's license number:");
                            License = Console.ReadLine();
                            while ((License.Length != 7) && (License.Length != 8))//Checks if the license number is correct
                            {
                                Console.WriteLine("ERROR");
                                License = Console.ReadLine();
                            }
                            Console.WriteLine
                    (@"Do you want to refoul gas or to do treatment?
        To refoul press 1,
        To treatment press 2.");

                            while (!int.TryParse(Console.ReadLine(), out Num))
                            { Console.WriteLine("wrong number!!! Please try again:"); }
                            Succes = false;
                            foreach (Bus i in Busses)//Looking for the requested bus.
                            {
                                if (i.getLicenseNum() == License)
                                {
                                    Succes = true;
                                    if (Num == 1)//If you ask for refueling, refuel.
                                    {
                                        i.setfuel(1200);
                                        Console.WriteLine("refouling was performed");
                                    }

                                    if (Num == 2)//If you ask for treatment, do the treatment.
                                    {
                                        DateTime currentTime1 = DateTime.Now;
                                        i.setkmToTritment(0);
                                        i.setlastTritment(currentTime1);
                                        Console.WriteLine("The treatment was performed");
                                    }
                                    if (Num != 1 && Num != 2)
                                    {
                                        Console.WriteLine("ERROR");
                                    }
                                }

                            }
                            if (Succes == false)
                            {
                                Console.WriteLine("The bus was'nt found");
                            }
                            break;

                        //printing the data of the vehicle
                        case BChoice.e:
                            foreach (Bus i in Busses)//Prints the license numbers of all buses
                            {
                                string last;
                                string first;
                                string middle;
                                int totalKm;
                                totalKm = i.gettotalKm();
                                if (((i.getLicenseNum()).Length) == 7)//If the license number is 7 digits
                                {

                                    first = i.getLicenseNum().Substring(0, 2);
                                    middle = i.getLicenseNum().Substring(2, 3);
                                    last = i.getLicenseNum().Substring(5, 2);
                                    Console.WriteLine("{0}-{1}-{2}", first, middle, last);
                                    Console.WriteLine(totalKm);
                                }
                                else //If the license number is 8 digits
                                {


                                    first = i.getLicenseNum().Substring(0, 3);
                                    middle = i.getLicenseNum().Substring(3, 2);
                                    last = i.getLicenseNum().Substring(5, 3);
                                    Console.WriteLine("{0}-{1}-{2}", first, middle, last);
                                    Console.WriteLine(totalKm);
                                }
                            }
                            break;
                    }
            }while (Num != 0) ;  
        }
    }
}
