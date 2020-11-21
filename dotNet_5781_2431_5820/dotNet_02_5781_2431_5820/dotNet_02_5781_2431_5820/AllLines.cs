using dotNet_02_5781_2431_5820.git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public double DistanceBetweenTwoStations(int StationCode1, int StationCode2)
        {
            double orech1 = 0;
            double rochav1 = 0;
            double orech2 = 0;
            double rochav2 = 0;
            foreach (var item in busStops)
            {//is that the check ? i do not understand this :(
                //wait i think i do- did you tried to compare coordinate and do the distance ?
                //i think they meant to calculate threw the path it meeans dis1+dis2 etc... 
                //if you calculate the dis just with cootdinate i think its incorrect couse you get nuch shorter dis between the stops 
                if (item.CodeStation == StationCode1)
                {
                    orech1 = item.BusStopLocation.GetLongitude();
                    rochav1 = item.BusStopLocation.GetLatitude();
                }
                if (item.CodeStation == StationCode2)
                {
                    orech2 = item.BusStopLocation.GetLongitude();
                    rochav2 = item.BusStopLocation.GetLatitude();
                }
            }
            var StartCor = new GeoCoordinate(rochav1, orech1);
            var EndCor = new GeoCoordinate(rochav2, orech2);
            double Distance = StartCor.DistancefromPriviouStation(StartCor, EndCor);
            return Distance;
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