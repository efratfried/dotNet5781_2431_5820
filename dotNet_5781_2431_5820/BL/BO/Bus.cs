using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public string LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Foul_Status Foul_Status { get; set; }
        public Status Status { get; set; }
        public Firm Firm { get; set; }
        public IEnumerable <DO.Accident> AccidentsDuco { get; set; }
        public IEnumerable<Treat> TreatsDuco { get; set; }
        public IEnumerable<DrivingBus> drivingBusesDuco { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
