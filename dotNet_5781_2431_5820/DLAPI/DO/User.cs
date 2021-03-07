using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public Access Me { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
