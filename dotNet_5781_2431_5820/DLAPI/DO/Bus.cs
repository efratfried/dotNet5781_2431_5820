using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public TimeSpan LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Bus_Status Status { get; set; }
        public Firm MyFirm { get; set; }
    }
}
