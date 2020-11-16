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
        public BusStop(string code = null,bool flag=true)//we need to do the valid check first !!!!!!
        {//get a valid code from the 
            this.CodeStation = code;
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            Random rnd4 = new Random();
            double rochav = (rnd1.NextDouble() + rnd2.NextDouble()) % 2.4 + 31;
            double orech = (rnd3.NextDouble() + rnd4.NextDouble()) % 1.4 + 34.3;
            this.BusStopLocation.SetLocation(orech, rochav,flag);
        }

        public string CodeStation;
        public Location BusStopLocation;
        public bool ValidCodeStation(string str, List<BusStop> BusStops)
        {//func checks abbility to add the code that was entered
            if (str.Length != 6)
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
