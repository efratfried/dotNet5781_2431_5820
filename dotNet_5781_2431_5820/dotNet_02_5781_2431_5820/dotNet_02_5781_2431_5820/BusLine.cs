using System.Collections.Generic;

namespace dotNet_02_5781_2431_5820.git
{
    internal class BusLine
    {
      public  int LineNum;
      public BusStopLine Start { get; set; }
      public  BusStopLine End { get; set; }
        public List <BusStopLine> LineStops;//list of all the station of the line
        public Area MyArea;
        public void UpdateArea (BusStopLine NewStop)
        {
            if (( - NewStop.BusStopLocation.GetLatitude())==)
        }
    }
}