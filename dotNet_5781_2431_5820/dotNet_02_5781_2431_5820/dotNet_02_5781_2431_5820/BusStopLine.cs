//efrat fried
//tamar packter
using dotNet_02_5781_2431_5820.git;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
namespace dotNet_02_5781_2431_5820.git
{
    public class BusStopLine : BusStop, IComparable, IEnumerator
    {
        private int iCurrent = -1;
        public BusStopLine() : base()
        {

        }

        public double DistancefromPriviouStation(BusStopLine BusstopLine1, BusStopLine BusstopLine2)
        {
            //returns the distance by equation sqrt( (x-x)^2+(y-y)^2)
            double Distance = Math.Sqrt((Math.Pow(BusstopLine1.BusStopLocation.GetLatitude() - BusstopLine2.BusStopLocation.GetLatitude(), 2) + (Math.Pow(BusstopLine1.BusStopLocation.GetLongitude() - BusstopLine2.BusStopLocation.GetLongitude(), 2))));
            return Distance;
        }

        public TimeSpan TimefromPriviouStation(BusStopLine BusstopLine2)
        {
            double Dis = DistancefromPriviouStation(this, BusstopLine2);
            Dis = Dis * 60 / 75;//75 km per hour is a avrage of the able speed on the road for busses - 50 in the city and 100 out of the city
            int dis = Convert.ToInt32(Dis);//dont care to loose a little bit info because it is not exact but evaluieted time
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            BusLine otherTemperature = obj as BusLine;
            if (otherTemperature != null)
                return this.CompareTo(otherTemperature);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
        public int CompareTo(BusLine A, BusLine B, BusStopLine distanation)//compare which driving time is shorter.
        {
            //compare between two stations's code to see which station is first/second.
            int IndexOfFirst = A.IndexOfStation(this.CodeStation);
            int IndexOfSecond = B.IndexOfStation(this.CodeStation);

            if (A.DrivingTimeBetweenTwoStations(A.LineStops[IndexOfFirst].CodeStation, distanation.CodeStation) < (B.DrivingTimeBetweenTwoStations(B.LineStops[IndexOfSecond].CodeStation, distanation.CodeStation)))
            {//if a is amaller than b.
                return -1;
            }
            else if (A.DrivingTimeBetweenTwoStations(A.LineStops[IndexOfFirst].CodeStation, distanation.CodeStation) > (B.DrivingTimeBetweenTwoStations(B.LineStops[IndexOfSecond].CodeStation, distanation.CodeStation)))
            {//if a is bigger than b.
                return 1;
            }
            else//if a & b are equal.
                return 0;
        }
        public bool MoveNext()
        {
            if (iCurrent < LineStops.Count - 1)
            {
                ++iCurrent;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            iCurrent = -1;
        }
        public object Current
        {
            get
            {
                return LineStops[iCurrent];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
