using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace dotNet_02_5781_2431_5820
{
    class BusStop
    {
        BusStop(string code)//we need to do the valid check first !!!
        {//get a valid code from the 
            this.CodeStation = code;
            Random rnd1= new Random();
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            Random rnd4 = new Random();
            double rochav = (rnd1.NextDouble()+ rnd2.NextDouble())%2.4 +31;
            double orech = (rnd3.NextDouble() + rnd4.NextDouble()) %1.4 + 34.3;
            Console.WriteLine("please enter adress");
            this.BusStopLocation.SetLocation (orech , rochav);
        }
        public string CodeStation;
        Location BusStopLocation;

        class Location
        {
            private double Latitude;
            private double Longitude;
            private Adress adress;
            class Adress
            {
                public string City;
                public string Street;
                public int Num;

                public void PrintAdress()
                {
                    if(Street!=null)//if the user chose to enter an adress
                    {
                        Console.WriteLine("{0} {1},{2} ", Street, Num, City);
                    }
                }
            }


            public void SetLocation(double Rochav, double Orech)
            {
                Latitude = Rochav;
                Longitude = Orech;
                Console.WriteLine("do you want to enter an adress? ");
                Console.WriteLine("if you do please enter the bus station adress else press 0");//i didnt check if it is actually put each val into the right var
                adress.Street= Console.ReadLine();
                if (adress.Street!="0")
                {
                    adress.Num = Console.Read();
                    adress.City = Console.ReadLine();
                }
                else
                {
                    adress.Street = null;
                }
              
            }
            public double GetLatitude()
            {
                return Latitude;
            }
            public double GetLongitude()
            {
                return Longitude;
            }
            public void PrintLocation()
            {
                char Long;
                char lat;
                if (Longitude > 0)
                    Long = 'N';
                else if (Longitude < 0)
                    Long = 'S';
                else
                    Long = ' ';

                if (Latitude > 0)
                    lat = 'E';
                else if (Latitude < 0)
                    lat = 'W';
                else
                    lat = ' ';
                Console.WriteLine("{0}°{1},{2}°{3}", Longitude, Long, Latitude, lat);
            }
        }

        public bool ValidCodeStation(string str, List<BusStop> BusStops)
        {//func checks abbility to add the code that was entered
            if (str.Length!=6)
            {
                //if the entered code was incorrect
                return false;
            }
            else//if it is 6 digits
            {
                //checks if it is not already in the list
                if (BusStops.Any())
                {
                    foreach (BusStop b in BusStops)
                    {
                        if (b.CodeStation == CodeStation)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        
    }
}
