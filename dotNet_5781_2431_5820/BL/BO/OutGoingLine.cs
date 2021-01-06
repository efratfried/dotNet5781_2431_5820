using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OutGoingLine
    {
        protected int ID { set; get; }
        public TimeSpan Startine { set; get; }
        public TimeSpan Prequency { set; get; }
        public TimeSpan EndTime { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
