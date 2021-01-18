using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {//the phizical fitchers of station
        public string StationName { get; set; }
        //public Adress Adress { get; set; }
        public string CodeStation { get; set; }
        public bool DisableAccess { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double longitude { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
