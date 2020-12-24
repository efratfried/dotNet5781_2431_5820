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
        public BusStop(bool flag = true)//we need to do the valid check first-???
        {//get a valid code from ?
            Random rndArea = new Random();

            MyArea = (Area)(rndArea.Next() % 7);
            CodeStation = Code++;
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            Random rnd4 = new Random();
            double rochav = (rnd1.NextDouble() + rnd2.NextDouble()) % 2.4 + 31;
            double orech = (rnd3.NextDouble() + rnd4.NextDouble()) % 1.4 + 34.3;
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
            return "Bus Station Code:" + Code + ", "+ BusStopLocation.ToString();
        }
    }
}