using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDAL
{
    class Station
    {
            string StationName { get; set; }
            protected Area MyArea { get; set; }
            public int CodeStation { get; set; }
            public Location BusStopLocation { get; set; }
            public bool DisableAccess { get; set; }

     }
}
