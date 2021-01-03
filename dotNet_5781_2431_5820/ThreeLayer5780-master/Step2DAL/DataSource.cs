using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    class DataSource
    {
        internal List<Data> datas = new List<Data>();
        
        internal DataSource()
        {
            datas.Add(new Data() { Id = 1, Name = "Reuven" });
            datas.Add(new Data() { Id = 2, Name = "Shim'on" });
            datas.Add(new Data() { Id = 3, Name = "Levi" });
        }
    }
}
