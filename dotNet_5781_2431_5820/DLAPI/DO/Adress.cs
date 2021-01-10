using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * preson = BusLine
 * student =bus
 * lecturer=outgoingBus 
 * course= Station
 * studentIncuorse=BusStationLine
 * lecturerIncourse=drivingBus
 * */

namespace DO
{
    public class Adress
    {
        public string Address { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
