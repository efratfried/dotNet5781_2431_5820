using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ListedBus
    {
        BO.ListedBus bus;
        public BO.ListedBus Bus { set { bus = value; Show = string.Format("{0,-9} {1}", bus.LincenseNum); } get => bus; }
        public string Show { get; private set; }
        public override string ToString() => Show;
    }
}
