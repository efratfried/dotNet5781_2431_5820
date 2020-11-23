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
<<<<<<< HEAD
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
=======
        public double DistanceBetweenTwoStations(int StationCode1,int StationCode2)
        {//function to get the distance between two stations.
            double orech1=0;
            double rochav1=0;
            double orech2=0;
            double rochav2=0;
            foreach(var item in busStops)
            {
                if(item.CodeStation == StationCode1)
                {//get the cordinate of the first station
                      orech1 = item.BusStopLocation.GetLongitude();
                      rochav1 = item.BusStopLocation.GetLatitude();
>>>>>>> 5c7138ae8ac1aa8c803f70558ecca644cf1913dc
                }
                if (item.CodeStation == StationCode2)
                {//get the cordinate of the second station
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
<<<<<<< HEAD
                if (item.CodeStation == StationCode1)
                {=======
                if(item.CodeStation == StationCode1)
                {//check if the first station's code is equal>>>>>>> 5c7138ae8ac1aa8c803f70558ecca644cf1913dc
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