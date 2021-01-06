using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class UserDrive
    {
        protected int ID { set; get; }
        public string UserName { set; get; }
        // public string name { set; get; }
        public int LineIndifinder { set; get; }
        public int StartStationIndifinder { set; get; }
        public TimeSpan HopOn { set; get; }
        public int EndStationIndifinder { set; get; }
        public TimeSpan HopOff { set; get; }
        public override string ToString() => this.ToStringProperty();
    }
}
