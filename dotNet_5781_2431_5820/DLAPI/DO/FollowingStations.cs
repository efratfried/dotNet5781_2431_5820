using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class FollowingStations
    {//info between to folowing station 
        public string FirstStationCode { set; get; }
        public string SecondStationCode { set; get; }
        public string FirstStationName { set; get; }
        public string SecondStationName { set; get; }
        public double Distance { set; get; }
        public TimeSpan AverageDrivingTime { set; get; }
        public TimeSpan WalkingTime { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
