using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        protected string UserName { set; get; }
        protected string Password { set; get; }
        public Access Me { protected set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
