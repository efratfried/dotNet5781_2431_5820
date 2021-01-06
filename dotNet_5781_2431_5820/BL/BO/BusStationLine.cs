using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStationLine
    {
        protected int ID { set; get; }
        public int BusStationNum { set; get; }
        public int IndexInLine { set; get; }
        public int NumOfPassingLines { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
