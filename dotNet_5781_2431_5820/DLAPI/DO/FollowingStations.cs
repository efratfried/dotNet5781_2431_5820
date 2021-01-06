using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class FollowingStations
    {
        public int FirstStationCode { set; get; }
        public int SecondStationCode { set; get; }
        public double Distance { set; get; }
        public TimeSpan AaverageDrivingTime { set; get; }
        public TimeSpan WalkingTime { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
