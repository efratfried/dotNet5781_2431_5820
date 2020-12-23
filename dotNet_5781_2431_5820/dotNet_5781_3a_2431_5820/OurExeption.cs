using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet_02_5781_2431_5820
{[Serializable]

    class OurExeption : Exception
    {
        public OurExeption() : base() { }
        public OurExeption(string Error) : base(Error) { }
        public OurExeption(string Error,Exception inner) : base(Error,inner) { }
        public OurExeption(SerializationInfo info,StreamingContext context) : base(info,context) { }
    }
}
