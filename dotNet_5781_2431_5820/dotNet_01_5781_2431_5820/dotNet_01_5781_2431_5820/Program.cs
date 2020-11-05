//efrat fried 322552431
//tamat packter 212355820

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5781_2431_5820
{
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

                        int km = rand.Next(1200);//How many kilometers will the trip be (up to 1200).
                        Console.WriteLine("The amount of Km is " + km);
                        bool flag = false;
                        DateTime currentTime = DateTime.Now;
                        foreach (Bus i in busses)//Go through each bus on the list to see if the selected bus can make the trip.
                        {
                            if (i.getLicenseNum() == license)
                            {
                                flag = true;
                                if ((i.getkmToTratment() < 20000) && (i.getfuel() - km >= 0) && (i.getlastTratment() >= currentTime.AddYears(-1)))//Go through each bus on the list to see if the selected bus can make the trip
                                {

                                    i.setkmToTratment(i.getkmToTratment() + km);
                                    i.setfuel(i.getfuel() - km);
                                    i.settotalKm(i.gettotalKm() + km);
                                    Console.WriteLine("The drive is possible");
                                }
                                else
                                {
                                    Console.WriteLine("The drive is impossible");//If the bus cannot travel it prints an appropriate message

                                }
                            }

                        }
                        if (flag == false)
                        {
                            Console.WriteLine("The license number was'nt found");//If the bus does not exist it prints an appropriate message.
                        }
                        break;

                    //checking if there is ant necessary to refuel gas or to treatment the vehicle.
                    case BChoice.c:

                        Console.WriteLine("Enter the bus's license number:");
                        license = Console.ReadLine();
                        while ((license.Length != 7) && (license.Length != 8))//Checks if the license number is correct
                        {
                            Console.WriteLine("ERROR");
                            license = Console.ReadLine();
                        }
                        Console.WriteLine
                (@"Do you want to refoul gas or to do treatment?
        To refoul press 1,
        To treatment press 2.");

                        while (!int.TryParse(Console.ReadLine(), out num))
                        { Console.WriteLine("wrong number!!! Please try again:"); }
                        flag = false;
                        foreach (Bus i in busses)//Looking for the requested bus.
                        {
                            if (i.getLicenseNum() == license)
                            {
                                flag = true;
                                if (num == 1)//If you ask for refueling, refuel.
                                {
                                    i.setfuel(1200);
                                    Console.WriteLine("refouling was performed");
                                }

                                if (num == 2)//If you ask for treatment, do the treatment.
                                {
                                    DateTime currentTime1 = DateTime.Now;
                                    i.setkmToTratment(0);
                                    i.setlastTratment(currentTime1);
                                    Console.WriteLine("The treatment was performed");
                                }
                                if (num != 1 && num != 2)
                                {
                                    Console.WriteLine("ERROR");
                                }
                            }

                        }
                        if (flag == false)
                        {
                            Console.WriteLine("The bus was'nt found");
                        }
                        break;

                    //printing the data of the vehicle
                    case BChoice.d:
                        foreach (Bus i in busses)//Prints the license numbers of all buses
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

                while (!int.TryParse(Console.ReadLine(), out num))
                { Console.WriteLine("wrong number!!! enter again:"); }
                ch = (BChoice)num;

            }
        }

    }
}
