using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DigitalPanel
    {
        public int BusLineNumber { get; set; }

        public string NameOfStation { get; set; }

        public TimeSpan TimeComeToStation { get; set; }

        public TimeSpan TimeComeToDistanation { get; set; }

        public double DistanceFromStation { get; set; }
    }
}
