﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStationLine
    {
        public string ID { set; get; }
        public string BusStationNum { set; get; }
        public int IndexInLine { set; get; }//index on the line
        public string StationName { get; set; }
        public int NumOfPassingLines { set; get; }
        public double Distance { set; get; }
        public TimeSpan AverageDrivingTime { set; get; }
        public TimeSpan WalkingTime { set; get; }
        public override string ToString() => this.ToStringProperty();
        
    }
}
