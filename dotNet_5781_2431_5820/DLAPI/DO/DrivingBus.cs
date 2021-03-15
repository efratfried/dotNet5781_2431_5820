using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DrivingBus
    {// real time updates 
        public int ID { get; set; }//key one
        public string LicenseNum { get; set; }//key two 
        public TimeSpan AstimateTimeOut { get; set; }
        public TimeSpan ActualTimeOut { get; set; }
        public string LastestStation { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        public TimeSpan TimePassFromLastestStation { get; set; }
        public TimeSpan AstimateArrive { get; set; }
        //public string DriverID { get; set; }
    }
}
