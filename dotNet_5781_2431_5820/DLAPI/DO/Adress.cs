using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Adress
    {
        public string Address { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
