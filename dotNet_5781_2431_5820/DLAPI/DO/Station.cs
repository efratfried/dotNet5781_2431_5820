using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        string StationName { get; set; }
        protected Adress Adress { get; set; }
        public int CodeStation { get; set; }
        public Location StationLocation { get; set; }
        public bool DisableAccess { get; set; }
    }
}
