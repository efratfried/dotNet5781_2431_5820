using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDAL
{
    class BusStation
    {
        public int StatonCode { get; set; }
        public Location Location { get; set; }
        public string StationName { get; set; }
        public Adress Adress { get; set; }
        public bool DisableAccess { get; set; }

    }
}
