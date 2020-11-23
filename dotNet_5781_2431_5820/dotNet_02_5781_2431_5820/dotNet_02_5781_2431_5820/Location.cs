//efrat fried
//tamar packter
using dotNet_02_5781_2431_5820.git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace dotNet_02_5781_2431_5820
{
    public class Location
    {
        protected double Latitude { set;  get; }
        protected double Longitude { set; get; }
        private Adress adress;
        public Location()
        {
            Latitude = 0;
            Longitude = 0;
            adress = new Adress();
        }  //ctor
        class Adress
        {
            public string City { set; get; }
            public string Street { set; get; }
            public int Num { set; get; }
            public override string ToString()
            {
                if (Street != null)
                {
                    return Street + Num + City+".";
                }
                else
                    return ".";
            }
        }
        public override string ToString()
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
                return Longitude + "°" + Long + " " + Latitude + "°" + lat + adress.ToString();// adress to string is never null:)
        }
            public void SetLocation(double Rochav, double Orech, bool flag)
        {
            Latitude = Rochav;
            Longitude = Orech;
            //the defult is yes i want addres but in busstopline its false so i can have location without adress
            if (flag)
            {
                DoYouWantAdress();
            }
        }
        public void DoYouWantAdress()
        {
            Console.WriteLine("do you want to enter an adress? ");
            Console.WriteLine("if you do ,Please enter the bus station adress. else press 0");//i didnt check if it is actually put each val into the right var
            adress.Street = Console.ReadLine();
            if (adress.Street != "0")
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
    }
}
