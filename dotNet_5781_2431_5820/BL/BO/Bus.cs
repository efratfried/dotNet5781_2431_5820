using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Bus_Status Status { get; set; }
        public Firm MyFirm { get; set; }
        public IEnumerable <DateTime> AccidentsDuco { get; set; }
        public IEnumerable<Treat> TreatsDuco { get; set; }
        public IEnumerable<DrivingBus> drivingBusesDuco { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
