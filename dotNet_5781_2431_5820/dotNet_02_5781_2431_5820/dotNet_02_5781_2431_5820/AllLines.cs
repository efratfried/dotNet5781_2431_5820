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
        public List<BusStopLine> busStops ;
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

                }
            }
        }
        public double DistanceBetweenTwoStations(int StationCode1,int StationCode2)
        {
            double orech1=0;
            double rochav1=0;
            double orech2=0;
            double rochav2=0;
            foreach(var item in busStops)
            {
                if(item.CodeStation == StationCode1)
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
            double Distance = StartCor.DistancefromPriviouStation(StartCor,EndCor);
            return Distance;
        }

       public double DrivingBetweenTwoStations(int StationCode1, int StationCode2)
        {
            int i = 0;
            double DrivingTime = 0;
            foreach(var item in )
            {
                i++;
                if(item.CodeStation == StationCode1)
                {
                    break;
                }
                while (busStops[i].CodeStation != StationCode2) 
                {
                    DrivingTime = busStops[i].TimefromPriviouStation();
                    i++;
                }
            }
            DrivingTime = busStops[i].TimefromPriviouStation();
            return DrivingTime;
        }
    }
}
