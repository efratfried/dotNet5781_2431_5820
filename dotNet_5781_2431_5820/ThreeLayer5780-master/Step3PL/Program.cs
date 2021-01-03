using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlCli
{
    class Program
    {
        static BlApi.IBL bl;
        static void Main(string[] args)
        {
            bl = BlApi.BlFactory.GetBl();
            Console.Write("Please enter data number: ");
            int num = int.Parse(Console.ReadLine());
            BO.DataPair pair = bl.GetDataPair(num, n => n + 1);
            Console.Write("{0} + {1} => ", pair.FirstName, pair.SecondName);
            foreach (byte b in pair.Code)
                Console.Write("{0,2:X}", b);
            Console.WriteLine();
        }
    }
}

