using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class OutGoingLine
    {//info page in sation on line
        public int Id { get; set; }
        public int BusLineID1 { get; set; }
        public TimeSpan LineStartTime { get; set; }
        public TimeSpan LineFinishTime { get; set; }
        public TimeSpan LineFrequencyTime { get; set; }
        public int LineFrequency { get; set; }
        public override string ToString()
        {
            return ToStringProperty();
        }
        private string ToStringProperty()
        {
            throw new NotImplementedException();
        }
        //public DateTime Frequency { get; set; }
    }
}
