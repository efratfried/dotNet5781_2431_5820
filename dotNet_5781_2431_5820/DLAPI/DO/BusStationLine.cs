using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusStationLine
    {// the station info inside the line
        public  int ID { set; get; }
        public int BusStationNum { set; get; }
        public int IndexInLine { set; get; }
        public int NumOfPassingLines { set; get; }
    }
}
