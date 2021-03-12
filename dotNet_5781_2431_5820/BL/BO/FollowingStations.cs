﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class FollowingStations
    {
        public string FirstStationCode { set; get; }
        public string SecondStationCode { set; get; }
        //public string FirstStationName { set; get; }
        //public string SecondStationName { set; get; }
        public double Distance { set; get; }
        public TimeSpan AaverageDrivingTime { set; get; }
        public TimeSpan WalkingTime { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
