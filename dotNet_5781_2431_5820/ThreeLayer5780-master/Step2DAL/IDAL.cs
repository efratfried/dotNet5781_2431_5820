using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDAL
    {
        int GetDataCount();
        DO.Data GetData(int id);
        IEnumerable<DO.Data> GetDatas();
    }
}
