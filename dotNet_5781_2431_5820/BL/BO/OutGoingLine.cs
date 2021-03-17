using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OutGoingLine
    { 
            public int Id { get; set; }
            public int BusLineID1 { get; set; }
            public int LineFrequency { get; set; }
            public string NameOfLastStation { get; set; }
            public TimeSpan LineStartTime { get; set; }
            public TimeSpan LineFinishTime { get; set; }
            public TimeSpan LineFrequencyTime { get; set; }
            /// <summary>
            /// Frequency of the line to the current port.
            /// </summary>
            public List<TimeSpan> DepartureTimes = new List<TimeSpan>();
            /// <summary>
            /// Arrivals at the current departure point.
            /// </summary>
            public List<TimeSpan> TimeFinishTrval = new List<TimeSpan>();
    }
}
