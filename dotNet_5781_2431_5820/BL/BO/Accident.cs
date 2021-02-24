using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Accident
    {
        public int LicenseNum { get; set; }
        public IEnumerable<DateTime> AccidentDate { get; set; }
        public int AccidentNum { get; set; }
    }


}
