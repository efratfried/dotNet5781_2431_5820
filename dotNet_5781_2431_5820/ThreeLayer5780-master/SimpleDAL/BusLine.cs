using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDAL
{
    class BusLine
    {
        protected int ID { set; get; }
        public int BusNum { set; get; }
        public Area Area { set; get; }
        public int FirstStation { set; get; }
        public int LastStation { set; get; }
        public Company Mycompany { set; get; }
    }
}
