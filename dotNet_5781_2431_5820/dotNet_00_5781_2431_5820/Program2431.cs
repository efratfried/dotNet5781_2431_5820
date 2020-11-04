using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_00_5781_2431_5820
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2431();
            Welcome5820();
            Console.ReadKey();
        }

        private static void Welcome2431()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
         static partial void Welcome5820();
        
    }
}
