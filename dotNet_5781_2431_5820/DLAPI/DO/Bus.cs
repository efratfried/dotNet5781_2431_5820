using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {// the thechnical phyzical fitchers of the bus
        public int LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Bus_Status Status { get; set; }
        public Firm MyFirm { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
    /*
     *   public int ID { get; set; }
        public string Name { get; set; }
        ****public PersonalStatus PersonalStatus { get; set; }
       **** public string Street { get; set; }
        ****public int HouseNumber { get; set; }
        public string City { get; set; }
       **** public DateTime BirthDate { get; set; }
        ****public override string ToString()
        {
            return this.ToStringProperty();
        }
     * */

}
