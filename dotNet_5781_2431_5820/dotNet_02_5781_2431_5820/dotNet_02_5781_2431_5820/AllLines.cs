//efrat fried
//tamar packter

//we didnt have much time therefor we didnt finish to do all the request program , that is the best we could
//we hope so you appriciate the alot thinking & down to the smallest details.
using dotNet_02_5781_2431_5820.git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;


namespace dotNet_02_5781_2431_5820
{
    public class AllLines
    {
        public List<BusLine> Lines;
        public List<BusStopLine> busStops;
        public void AddLine(BusLine WantedLine)
        {
            if (!Lines.Any())
            {
                Lines.Add(WantedLine);
            }
            else
            {
                if (Lines.Contains(WantedLine))//check if the condition is not always false because of the adress ect...
                {
                    foreach (var item in busStops)
                    {

                    }
                }
            }
        }
        
      public TimeSpan DrivingBetweenTwoStations(int StationCode1, int StationCode2)
        {
            int i = 0;
            TimeSpan DrivingTime;
            foreach (var item in busStops)
            {// need to do a check who is first and who is seconed
                i++;

                if (item.CodeStation == StationCode1)
                {
                    if (item.CodeStation == StationCode1)
                    {//check if the first station's code is equal
                        break;
                    }
                    DrivingTime = new TimeSpan();
                    //why for each and than you use it once ? i didnt understand the loops 
                    while (busStops[i].CodeStation != StationCode2)
                    {
                        DrivingTime += busStops[i].TimefromPriviouStation(/*need to have an bus stop line here!*/);
                        i++;
                    }
                }
                DrivingTime = busStops[i].TimefromPriviouStation(/*need to have an bus stop line here!*/);
                return DrivingTime;
            }
        }
    }
}