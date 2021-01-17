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

        public bool AddBus( List<Bus> busses, DateTime Start)
        {

            //a check to see if the date of entering the road is valid

           bool Succes = false;
            while (Succes == false)  // while the date is not correct
            {
                Console.WriteLine("Please enter date of entry to the road");

                //    string x = Console.ReadLine();
                string response = Console.ReadLine();
                Succes = DateTime.TryParse(response, out Start);
                if (!Succes)//if the enterence was imposible
                {
                    Console.WriteLine("ERROR");
                   bool flag = Mistake()=="1";
                    if (flag)
                    {
                        return AddBus(busses, Start);
                    }
                    else
                    {
                        return false;
                    }
                }
                this.setbeganToWork(Start);//the date was entered sucssesfully
                return  EnteringValidLicense( busses);
            }
            
            return true;
        }


        /// <summary>
        /// Set functions that put the data into the private fields of the class
        /// </summary>
        /// <param name="s">The variable that receives the data sent to it in the function</param>
        ///        

        public bool ValidRide(DateTime currentTime)
        {//checks validation of ride km and gas
            if ((KmToFix < 20000) && (Gas - Km >= 0) && (LastFix >= currentTime.AddYears(-1)))
                return true;
            else
                return false;
        }
        public void TikunClali(List<Bus> Buses)
        {

        }
        public bool ValidLicense()
        {//if the license is correct

            if ((LicenseNum.Length != 7) && (LicenseNum.Length != 8))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EnteringValidLicense(List<Bus> busses)
        {
            bool flag;
            Console.WriteLine("Please enter the bus's license number:");
            if ((StartToDrive.Year) >= 2018)
            {//amount of numbers the license number should have
                string Str = Console.ReadLine();
                if (busses.Any())
                {
               if( findlicense(busses, Str)!=null)
                    {
                        Console.WriteLine("ERROR");
                        Console.WriteLine("the bus is already in the system");
                        flag = Mistake() == "1"; 
                        if (flag)
                        {
                            return EnteringValidLicense(busses);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                if(Str.Length != 8 )
                {
                    Console.WriteLine("ERROR");
                     flag = Mistake() == "1"; if (flag)
                    {
                        return EnteringValidLicense( busses);
                    }
                    else
                    {
                        return false;
                    }
                }
                this.setLicenseNum(Str);
            }
            else
            {
                string Str = Console.ReadLine();
                if (Str.Length != 7)
                {
                    Console.WriteLine("ERROR");
                     flag = Mistake() == "1"; if (flag)
                    {
                        return EnteringValidLicense( busses);
                    }
                    else
                    {
                        return false;
                    }
                }
                this.setLicenseNum(Str);
            }
            return true;
        }
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
        private string Mistake()
        {
            string choise;
            Console.WriteLine("if you want to try again press 1");
            Console.WriteLine("if you want to go back to the main menu press 0");
            choise = Console.ReadLine();
            return choise;
        }

        public Bus findlicense(List<Bus> busses, string LicenseNum)

        {
            foreach (Bus b in busses)
            {
                if (b.LicenseNum == LicenseNum)
                {
                    return b;
                }
            }
            return null;
        }
    }

    
}
 

