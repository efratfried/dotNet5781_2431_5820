using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {// the thechnical phyzical fitchers of the bus
        public string LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Firm Firm { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
  
}
