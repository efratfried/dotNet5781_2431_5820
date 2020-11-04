using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_5781_2431_5820
{
    /// <summary>
    /// class representing bus, with data of fuel, number of miles, licensing tax, last treatment date.
    /// </summary>
    class Bus
    {
        private string licenseNum;
        private DateTime StartToDrive;
        private int Km;
        private int kmToFix;
        private DateTime lastFix;
        private int gas;

        /// <summary>
        /// Set functions that put the data into the private fields of the class
        /// </summary>
        /// <param name="s">The variable that receives the data sent to it in the function</param>
        public void setLicenseNum(string b)
        {
            licenseNum = b;
        }
        public void setbeganToWork(DateTime b)
        {
            StartToDrive = b;
        }
        public void settotalKm(int b)
        {
            Km = b;
        }
        public void setkmToTratment(int b)
        {
            kmToFix = b;
        }
        public void setlastTratment(DateTime b)
        {
            lastFix = b;
        }
        public void setfuel(int b)
        {
            gas = b;
        }


        /// <summary>
        /// Returns the value of the private fields of the class
        /// </summary>
        /// <returns></returns>
        public string getLicenseNum()
        {
            return licenseNum;
        }
        public DateTime getbeganToWork()
        {
            return StartToDrive;
        }
        public int gettotalKm()
        {
            return Km;
        }
        public int getkmToTratment()
        {
            return kmToFix;
        }
        public DateTime getlastTratment()
        {
            return lastFix;
        }
        public int getfuel()
        {
            return gas;
        }
    }
}