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
        string CodeStation;
        Location BusStopLocation;

        class Location
        {
            private double Latitude;
            private double Longitude;
            class Adress
            {
                public string City;
                public string Street;
                public int Num;

                public void PrintAdress()
                {
                    Console.WriteLine("{0} {1},{2} ", Street, Num, City);
                }
            }


            public void SetLocation(double Rochav, double Orech)
            {
                Latitude = Rochav;
                Longitude = Orech;
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
