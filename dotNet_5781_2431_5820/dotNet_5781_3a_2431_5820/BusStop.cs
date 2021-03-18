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

    public class BusStop
    {
        protected Area MyArea;
        public static int Code = 1;
        public int CodeStation;
        public Location BusStopLocation;
        static Random rnd1 = new Random();
        public BusStop(bool flag = true)//we need to do the valid check first-???
        {//get a valid code from ?
            Random rndArea = new Random();
            BusStopLocation = new Location();

            MyArea = (Area)(rndArea.Next() % 7);
            CodeStation = Code++;
            
            double rochav = (rnd1.NextDouble() + rnd1.NextDouble()) % 2.4 + 9;
            double orech = (rnd1.NextDouble() + rnd1.NextDouble()) % 1.4 + 34.3;
            this.BusStopLocation.SetLocation(orech, rochav, flag); //check the ctor if zero doesnt do any problems.
        }
        public bool ValidStation(string StationNum)
        {
            if(StationNum.Length<0||StationNum.Length>6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override string ToString()
        {
            return "Bus Station Code: " + CodeStation + " , "+ BusStopLocation.ToString();
        }
    }
}