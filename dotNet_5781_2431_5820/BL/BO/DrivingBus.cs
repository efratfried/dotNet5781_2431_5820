using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DrivingBus:BusLine
    {
        public bool finish { get; set; }
        IEnumerable<Station> StationsList;
        protected int ID { get; set; }//key one
        public int LicenseNum { get; set; }//key two 
        public TimeSpan AstimateTimeOut { get; set; }
        public TimeSpan ActualTimeOut { get; set; }
        public string LastestStation { get; set; }
        public TimeSpan TimePassFromLastestStation { get; set; }
        public TimeSpan AstimateArrive { get; set; }
        public string DriverID { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
