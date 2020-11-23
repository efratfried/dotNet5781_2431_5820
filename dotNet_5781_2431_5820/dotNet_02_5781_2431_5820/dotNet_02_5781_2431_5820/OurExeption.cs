using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using dotNet_02_5781_2431_5820.git;
using System.Xml.Schema;
namespace dotNet_02_5781_2431_5820
{[Serializable]

    class OurExeption : Exception
    {
/*q
 list of exeptions we need to check if we covered 
- when you get in 
        public OurExeption() : base() { }
        public OurExeption(string Error) : base(Error) { }
        public OurExeption(string Error,Exception inner) : base(Error,inner) { }
        public OurExeption(SerializableAttribute info,StreamingContext context) : base(info,context) { }
*/
    }
}
