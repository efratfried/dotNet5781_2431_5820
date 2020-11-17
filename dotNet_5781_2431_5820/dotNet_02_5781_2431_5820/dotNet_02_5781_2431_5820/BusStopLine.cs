﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet_02_5781_2431_5820
{
    public class BusStopLine : BusStop
    {
        public BusStopLine() : base() 
        {}

        public double DistancefromPriviouStation(BusStopLine BusstopLine1 , BusStopLine BusstopLine2)
        {
            //returns the distance by equation sqrt( (x-x)^2+(y-y)^2)
            double Distance = Math.Sqrt((Math.Pow(BusstopLine1.BusStopLocation.GetLatitude() - BusstopLine2.BusStopLocation.GetLatitude(), 2) + (Math.Pow(BusstopLine1.BusStopLocation.GetLongitude() - BusstopLine2.BusStopLocation.GetLongitude(), 2))));
            return Distance;
        }

        public TimeSpan TimefromPriviouStation(BusStopLine BusstopLine2)
        {
         double Dis= DistancefromPriviouStation(this, BusstopLine2);
            Dis =  Dis * 60 / 75;//75 km per hour is a avrage of the able speed on the
            int dis = Convert.ToInt32(Dis);
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }
    }

}
