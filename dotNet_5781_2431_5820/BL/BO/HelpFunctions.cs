using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class HelpFunctions
    {
        public BO.Status MyStatus(ref BO.Bus bus)
        {
           if(bus.KM>20000 || bus.foul<100)
            {
                bus.Status = Status.UnAvailable;
            }
           else if(bus.drivingBusesDuco.Last().finish==false)
            {
                bus.Status = Status.OnDrive;
            }
            else
            {
                bus.Status = Status.Available;
            }
            return bus.Status;
        }
        public BO.Foul_Status MyFoul_Status(ref BO.Bus bus)
        {
            if (bus.foul == 0)
            {
                bus.Foul_Status = Foul_Status.empty;
            }

            if (bus.foul == 600)
            {//as we check in some websisites
                bus.Foul_Status = Foul_Status.full_tank;
            }

            if (bus.foul < 400 && bus.foul > 200)
            {
                bus.Foul_Status = Foul_Status.avrage;
            }

            if (bus.foul < 300 && bus.foul > 0)
            {
                bus.Foul_Status = Foul_Status.low;
            }
            return bus.Foul_Status;
        }
        
    }
}
