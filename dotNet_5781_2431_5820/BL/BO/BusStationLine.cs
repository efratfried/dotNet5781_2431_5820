using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStationLine
    {
        protected string ID { set; get; }
        public string BusStationNum { set; get; }
        public int IndexInLine { set; get; }
        public int NumOfPassingLines { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
