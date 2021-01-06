using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLine
    {
        public int ID { set; get; }
        public int BusNum { set; get; }
        public Area Area { set; get; }
        public int FirstStation { set; get; }
        public int LastStation { set; get; }
        public BusLine MyBusLine { set; get; }
        public bool IsDeleted { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
