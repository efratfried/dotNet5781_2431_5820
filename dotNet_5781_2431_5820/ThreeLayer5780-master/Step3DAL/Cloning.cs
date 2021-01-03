using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    static class Cloning
    {
        public static Data Clone(this Data data)
        {
            return new Data() { Id = data.Id, Name = data.Name };
        }
    }
}
