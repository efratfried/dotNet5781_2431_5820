using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDAL GetDAL(string type)
        {
            switch (type)
            {
                case "object":
                    return DAL.DAL.Instance;
                case "xml":
                    throw new NotImplementedException("DalXml is not implemented yet");
                default:
                    throw new ArgumentException("There is no such xml imeplementation");
            }
        }
    }
}
