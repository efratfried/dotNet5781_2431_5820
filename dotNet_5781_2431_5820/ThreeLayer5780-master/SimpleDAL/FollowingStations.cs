using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDAL
{
    class FollowingStations
    {
        public int FirstCode { set; get; }
        public int SecondCode { set; get; }
        public double Distance { set; get; }
        public TimeSpan AaverageDrivingTime { set; get; }
        public TimeSpan WalkingTime { set; get; } 
    }
}
