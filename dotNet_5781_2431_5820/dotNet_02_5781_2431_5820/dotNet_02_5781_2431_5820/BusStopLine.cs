using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet_02_5781_2431_5820
{
    public class BusStopLine : BusStop
    {
        public BusStopLine(string code) : base(code) 
        {}

        public double DistancefromPriviouStation(BusStopLine BusstopLine1 , BusStopLine BusstopLine2)
        {
            //returns the distance by equation sqrt( (x-x)^2+(y-y)^2)
            double Distance = Math.Sqrt((Math.Pow(BusstopLine1.BusStopLocation.GetLatitude() - BusstopLine2.BusStopLocation.GetLatitude(), 2) + (Math.Pow(BusstopLine1.BusStopLocation.GetLongitude() - BusstopLine2.BusStopLocation.GetLongitude(), 2))));
            return Distance;
        }

        public string TimefromPriviouStation(BusStopLine BusstopLine1, double DistancefromPriviouStation(BusStopLine BusstopLine1, BusStopLine BusstopLine2))
        {
            string TotalTime = "0";
            BusstopLine1
            return TotalTime;
        }
    }

}
