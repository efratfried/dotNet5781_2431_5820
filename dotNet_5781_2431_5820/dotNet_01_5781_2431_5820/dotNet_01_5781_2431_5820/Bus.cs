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
        private string LicenseNum;
        private DateTime StartToDrive;
        private int Km;
        private int KmToFix;
        private DateTime LastFix;
        private int Gas;

        /// <summary>
        /// Set functions that put the data into the private fields of the class
        /// </summary>
        /// <param name="s">The variable that receives the data sent to it in the function</param>
        ///        
        //public bool ValidRide(DateTime currentTime)//checks validation of ride
        //{
        //    if ((KmToFix < 20000) && (Gas - Km >= 0) && (LastFix >= currentTime.AddYears(-1)))
        //        return true;
        //    else
        //        return false;
        //}
        //public bool ValidLicense()
        //{
        //    if ((LicenseNum.Length != 7) && (LicenseNum.Length != 8))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public void setLicenseNum(string b)
        {
            LicenseNum = b;
        }
        public void setbeganToWork(DateTime b)
        {
            StartToDrive = b;
        }
        public void settotalKm(int b)
        {
            Km = b;
        }
        public void setkmToTritment(int b)
        {
            KmToFix = b;
        }
        public void setlastTritment(DateTime b)
        {
            LastFix = b;
        }
        public void setfuel(int b)
        {
            Gas = b;
        }


        /// <summary>
        /// Returns the value of the private fields of the class
        /// </summary>
        /// <returns></returns>
        public string getLicenseNum()
        {
            return LicenseNum;
        }
        public DateTime getbeganToWork()
        {
            return StartToDrive;
        }
        public int gettotalKm()
        {
            return Km;
        }
        public int getkmToTritment()
        {
            return KmToFix;
        }
        public DateTime getlastTritment()
        {
            return LastFix;
        }
        public int getfuel()
        {
            return Gas;
        }
    }
}