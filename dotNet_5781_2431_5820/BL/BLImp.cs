using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
//using DocumentFormat.OpenXml.Office2010.Excel;

namespace BL
{
    class BLImp : IBL//internal
    {    
        IDL dl = DLFactory.GetDL();
        static Random rand = new Random();
      
        #region Bus
        BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            string LicenseNum = BusDO.LicenseNum;
            try
            {
                BusDO = dl.GetBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus LicenseNum is illegal", Ex);
            }

            BusDO.CopyPropertiesTo(BusBO);
            BusBO.Status = status(BusBO);
            BusBO.Foul_Status = foul_Status(BusBO);
            return BusBO;
        }
        public BO.Bus GetBus(string LicenseNum)
        {
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Buss' LicenseNum does not exist or it is not a Bus", Ex);
            }
            return BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetAllBuss()
        {
            return from BusDO in dl.GetAllBusses()
                   orderby BusDO.LicenseNum
                   select BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetBusIDList()
        {
            return from BusDO in dl.GetAllBusses()
                   orderby BusDO.LicenseNum
                   select BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.Bus> GetBusLicenseNumList()
        {
            return from item in dl.GetAllBusListWithSelectedFields((BusDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Bus() { LicenseNum = BusDO.LicenseNum };
            })
                   let BusBO = item as BO.Bus
                   //orderby Bus.LicenseNum
                   select BusBO;
        }
        public void UpdateBusPersonalDetails(BO.Bus bus)
        {
            //Update DO.Bus            
            DO.Bus BusDO = new DO.Bus();
            bus.CopyPropertiesTo(BusDO);
            try
            {
                dl.UpdateBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus's LicenseNum is illegal", Ex);
            }
        }
        public void DeleteBus(string LicenseNum)
        {
            try
            {
                dl.DeleteBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }
        public void AddBus(BO.Bus bus)
        {
            DO.Bus BusDO = new DO.Bus();
            if (bus.LicenseNum.Length != 7 && bus.LicenseNum.Length != 8)
            {
                throw new Exception("invalid license num lengh");
            }

            if ((bus.LicenseDate.Year < 2018 && bus.LicenseNum.Length != 7) || (bus.LicenseDate.Year >= 2018 && bus.LicenseDate < DateTime.Now && bus.LicenseNum.Length != 8))
            {//check the validation of the license num accroding to it date
                throw new Exception("invalid license's date");
            }

            bus.KM = 0;
            bus.foul = 0;
            bus.Status = Status.UnAvailable;

            BusDO.CopyPropertiesToNew(typeof(BO.Bus));
            try
            {
                dl.AddBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }
        }
        public BO.Foul_Status foul_Status(BO.Bus bus)
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
        public BO.Status status(BO.Bus bus)
        {
            if (/*bus.KM > 20000 ||*/ bus.foul < 200)
            {
                bus.Status = Status.UnAvailable;
            }
            else if (bus.drivingBusesDuco!=null)
            {
                if (bus.drivingBusesDuco.Last().finish == false)
                {
                    bus.Status = Status.OnDrive;
                }
            }
            else
            {
                bus.Status = Status.Available;
            }
            return bus.Status;
        }


        #endregion Bus

        #region Station
        BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();
            string CodeStation = StationDO.CodeStation;
            try
            {

                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("wring station's code", Ex);
            }

            StationDO.CopyPropertiesTo(StationBO);

            return StationBO;
        }
        public BO.Station GetStation(string CodeStation)
        {
            DO.Station StationDO;
            try
            {
                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("Station's code don't exist or it is not a station", Ex);
            }
            return StationDoBoAdapter(StationDO);
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from StationDO in dl.GetAllStations()
                   orderby StationDO.CodeStation
                   select StationDoBoAdapter(StationDO);
        }
       /* public IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }*/
        public IEnumerable<BO.Station> GetStationLicenseNumList()
        {
            return from item in dl.GetAllStationListWithSelectedFields((StationDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Station() { CodeStation = StationDO.CodeStation };
            })
                   let StationBO = item as BO.Station
                   //orderby Bus.LicenseNum
                   select StationBO;
        }
        public void UpdateStationPersonalDetails(BO.Station station)
        {
            //Update DO.Bus            
            DO.Station StationDO = new DO.Station();
            station.CopyPropertiesTo(StationDO);
            try
            {
                dl.UpdateStation(StationDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationNumException("Station num is illegal", Ex);
            }
        }
        public void DeleteStation(string Codestation)
        {
            try
            {
                dl.DeleteStation(Codestation);
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("station's code does not exist or it is not a correct code", Ex);
            }
        }
        public void AddStation(BO.Station station)
        {
            //the random number in in the begining
            DO.Station StationDO = new DO.Station();
            bool exist = bool.Parse((GetAllStations().Contains(station)).ToString());
            if(exist)
            {/** Content="Longitude   : 34.3-35.5 "
         Content="Latitude     :      31-33.3"*/
                throw new Exception("The wanted station is already exist in the data");
            }
            if (station.Latitude > 33.3 || station.Latitude < 31 || station.longitude < 33.3 || station.longitude > 35.5) 
            {
                throw new Exception("the cordinates are wrong");
            }
            if (station.CodeStation.Length > 6 || station.CodeStation.Length < 0)
            {
                throw new Exception("incorrect station's code");
            }
            StationDO.CopyPropertiesToNew(typeof(BO.Station));
            try
            {
                dl.AddStation(StationDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("station num is illegal", Ex);
            }
        }
        public TimeSpan GetLineTimingPerStation(BO.Station station, TimeSpan CurrentTime)
        {
            return CurrentTime;//need fix
        }


        #endregion

        #region BusStationLine
        BO.BusStationLine BusStationLineDoBoAdapter(DO.BusStationLine BLSDO)
        {
            BO.BusStationLine BLSBO = new BO.BusStationLine();

            //copy all relevant properties
            BLSDO.CopyPropertiesTo(BLSBO);

            return BLSBO;
        }
        public BO.Station GetBusStationLine(string CodeStation)
        {
            DO.Station BusStationLineDO;
            try
            {
                BusStationLineDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadBusStationLineCodeException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Buss' LicenseNum does not exist or it is not a Bus", Ex);
            }
            return StationDoBoAdapter(BusStationLineDO);
        }
        public IEnumerable<BO.BusStationLine> GetAllBusStationLines(int num)
        {
            return from BusStationLineDO in dl.GetBusStationLineList(b => b.ID == num.ToString())
                   orderby BusStationLineDO.ID
                   select BusStationLineDoBoAdapter(BusStationLineDO);
        }
       /* public IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate)
        {//need fill field 
            throw new NotImplementedException();
        }*/
        public IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum)
        {
            return from l in dl.GetBusStationLineList(i => i.ID.ToString() == BusStationLineNum)
                   select BusStationLineDoBoAdapter(l);
            //return from item in dl.GetBusStationsLineListWithSelectedFields((BusStationLineDO) =>
            //{
            //    try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
            //    return new BO.BusStationLine() { ID = BusStationLineDO.ID };
            //})
            //       let BusStationLineBo = item as BO.BusStationLine
            //       //orderby Bus.LicenseNum
            //       select BusStationLineBo;
        }
        public void AddBusStationLine(BusStationLine busStationLine)
        {
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            BusStationLineDO.CopyPropertiesToNew(typeof(BO.BusStationLine));
            try
            {
                dl.AddBusStationLine(BusStationLineDO);
            }
            catch (DO.BadBusStationLineCodeException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus Station Line ID is illegal", Ex);
            }
        }
        public void UpdateBusStationLinePersonalDetails(BO.BusStationLine bus_station_num)
        {            
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            bus_station_num.CopyPropertiesTo(BusStationLineDO);
            try
            {
                dl.UpdateBusStationLine(BusStationLineDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus's LicenseNum is illegal", Ex);
            }
        }
        public void DeleteBusStationLine(string num)
        {
            try
            {
                string BusStationLine = num;
                dl.DeleteBusStationLine(BusStationLine);
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }
        /*public IEnumerable<BO.BusStationLine> GetAllBusStationLinesPerLine(int lineId)
        {
            return from DOlineStation in dl.GetBusStationLinesListThatMatchAStation(lineId.ToString())
                   let BOlineStation = BusStationLineDoBoAdapter(DOlineStation)
                   select BOlineStation;
        }*/
        public void DeleteStationFromLine(BO.BusLine busline, string code)
        {
            try
            {
               /* if(busline.stationsList.Contains(code)==false)
                {
                    throw new BO.BadBusStationLineCodeException(busline.ID.ToString(),code);
                }*/

                dl.DeleteBusStationLine(code);
            }
            catch(DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }

        #endregion

        #region BusLine
        BO.BusLine BuslineDoBoAdapter(DO.BusLine blDO)
        {
            BO.BusLine blBO = new BO.BusLine();
            DO.BusLine newblDO;//before copying lineDO to lineBO, we need to ensure that lineDO is legal- legal busNumber.
            //sometimes we get here after the user filled lineDO fields. thats why we copy the given lineDO to a new lineDO and check if it is legal.
           // int blID = blDO.ID;
            int busNumber = blDO.ID;
            try
            {
                newblDO = dl.GetBusLine(busNumber);//if code is legal, returns a new lineStationDO. if not- ecxeption.
            }
            catch (DO.BadBusLineException ex)
            {
                throw new BO.BadBusLineIdException("Line bus number is illegal\n", ex);
            }

            newblDO.CopyPropertiesTo(blBO);//copies- only flat properties.

            //now we need to restart the "lineStations" list of each line.

            //blBO.stationsList = from BusStationLineDO in dl.GetBusStationLinesListOfALine(blID.ToString())
            //                      let lineStationBO = BusStationLineDoBoAdapter(BusStationLineDO)
            //                      select lineStationBO;

            return blBO;
        }
        public IEnumerable<BO.BusLine> GetAllLinesPerStation(int code)
        {
            return from lineStation in dl.GetLineStationsListThatMatchAStation(code)         
                   select BuslineDoBoAdapter(lineStation);
        }
        public IEnumerable<BO.BusLine> GetAllLinesByArea(BO.Area area)
        {
            return from LineDO in dl.GetAllBusLines()
                   where LineDO.Area.CompareTo((DO.Area)area) == 0//if the erea is equal to the given area
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        public IEnumerable<BO.BusLine> GetBusLines()
        {
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.ID
                   select BuslineDoBoAdapter(BusLineDO);
        }
        public BO.BusLine GetBusLine(int LicenseNum)
        {
            DO.BusLine BusLineDO;
            try
            {
                int licensenum = LicenseNum;
                BusLineDO = dl.GetBusLine(licensenum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("Bus's LicenseNum does not exist or it is not a Bus", ex);
            }
            return BuslineDoBoAdapter(BusLineDO);
        }
        public IEnumerable<BO.BusLine> GetBusLineIDList()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.IsDeleted
                   select BuslineDoBoAdapter(BusLineDO);
        }
       public IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate)
        {

            List<BO.BusLine> busLines = new List<BusLine>();
            List<DO.BusLine> busLines1 = dl.GetAllBusLines().ToList();
            for (int i = 0; i < busLines1.Count; i++)
            {
                busLines.Add(BuslineDoBoAdapter(busLines1[i]));
             //  busLines1[i].CopyPropertiesTo(busLines[i]);
            }
            return from l in busLines
                   where predicate(l)
                   select l;
        }
        public IEnumerable<BO.BusLine> GetBusLineLicenseNumList()
        {
            return from LineDO in dl.GetAllBusLines()
                   //where LineDO.ID.CompareTo((DO.BusLine)busLine) == 0
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        public void UpdateBusLinePersonalDetails(BO.BusLine BusLine)
        {
            //Update DO.Bus            
            DO.BusLine BusLineDO = new DO.BusLine();
            BusLine.CopyPropertiesTo(BusLineDO);
            try
            {
                dl.UpdateBusLine(BusLineDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("BusLine's LicenseNum is illegal", ex);
            }
        }
        public void DeleteBusLine(int ID)
        {//delete all the stations
            try
            {
                string id = ID.ToString();
                dl.DeleteBusLine(id);
                dl.DeleteBusStationLine(id);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("BusLine's ID does not exist or it is not a Bus", ex);
            }
        }
        
        public void AddBusLine(BO.BusLine busline)
        {
            DO.BusLine BusLineDO = new DO.BusLine();

            busline.CopyPropertiesTo(BusLineDO);

            try
            {
                DO.BusStationLine first = new DO.BusStationLine();
                DO.BusStationLine second = new DO.BusStationLine();
               
                busline.ID =  dl.AddBusLine(BusLineDO);
                first.BusStationNum = busline.FirstStation.ToString();
                second.BusStationNum = busline.LastStation.ToString();
                first.IndexInLine = 0;
                second.IndexInLine = 1;
                first.ID = busline.ID.ToString();
                second.ID =busline.ID.ToString();
                dl.AddBusStationLine(first);
                dl.AddBusStationLine(second);
                DO.FollowingStations fs = new DO.FollowingStations();
                fs.FirstStationCode = first.BusStationNum;
                fs.SecondStationCode = second.BusStationNum;
                dl.AddFollowingStations(fs); 
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }
        }

        #endregion

        #region User
        public BO.User userBoDoAdapter(DO.User userDO)
        {
            BO.User userBO = new BO.User();
            DO.User newUserDO;
            string name = userDO.UserName;
            string password = userDO.Password;
            try
            {
                newUserDO = dl.GetUser(name, password);
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                throw new BO.BadUserName_PasswordException( "ERROR!\n", ex);
            }
            newUserDO.CopyPropertiesTo(userBO);

            userDO.CopyPropertiesTo(userBO);

            return userBO;
        }

        /// <summary>
        /// returns the user with the given name and password
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>user bo</returns>
        public BO.User GetUser(string name, string password)
        {
            DO.User userDO;
            try
            {
                userDO = dl.GetUser(name, password);
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                throw new BO.BadUserName_PasswordException("The user with this password wasn't found\n", ex);
            }
            return userBoDoAdapter(userDO);
        }

        /// <summary>
        /// add a user
        /// </summary>
        /// <param name="user">user</param>
        public void AddUser(BO.User user)
        {
            try
            {
                dl.AddUser(userBoDoAdapter(user));
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                throw new BO.BadUserName_PasswordException("couldn't add the user\n", ex);
            }
        }

        /// <summary>
        /// adopt BO.user to DO.user and return it
        /// </summary>
        /// <param name="userBO"></param>
        /// <returns></returns>
        private DO.User userBoDoAdapter(BO.User userBO)
        {
            DO.User userDO = new DO.User();
            userBO.CopyPropertiesTo(userDO);
            return userDO;
        }

        /// <summary>
        /// returns IEnumerable of all the users
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<BO.User> GetAllUsers()
        {
            return from item in dl.GetAllUser()
                   select userBoDoAdapter(item);
        }

        #endregion
        /*
        #region OutGoingLine
        BO.OutGoingLine OutGoingLineDoBoAdapter(DO.OutGoingLine OutGoingLineDO)
        {
            BO.OutGoingLine OutGoingLineBO = new BO.OutGoingLine();
            string LicenseNum = OutGoingLineDO.ID;
            try
            {
                OutGoingLineDO = dl.GetOutGoingLine(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus LicenseNum is illegal", Ex);
            }

            OutGoingLineDO.CopyPropertiesTo(OutGoingLineBO);

            return OutGoingLineBO;
        }
        public BO.OutGoingLine GetOutGoingLine(string LicenseNum)
        {
            DO.OutGoingLine OutGoingLineDO;
            try
            {
                OutGoingLineDO = dl.GetOutGoingLine(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Buss' LicenseNum does not exist or it is not a Bus", Ex);
            }
            return OutGoingLineDoBoAdapter(OutGoingLineDO);
        }
        public IEnumerable<BO.OutGoingLine> GetAllOutGoingLines()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from OutGoingLineDO in dl.GetAllOutGoingLines()
                   orderby OutGoingLineDO.ID
                   select OutGoingLineDoBoAdapter(OutGoingLineDO);
        }
        public IEnumerable<BO.OutGoingLine> GetOutGoingLineIDList()
        {
            return from OutGoingLineDO in dl.GetAllOutGoingLines()
                   orderby OutGoingLineDO.ID
                   select OutGoingLineDoBoAdapter(OutGoingLineDO);
        }
        #endregion OutGoingLine
        /*public IEnumerable<BO.OutGoingLine> GetOutGoingLinesBy(Predicate<BO.OutGoingLine> predicate)
        {
            throw new NotImplementedException();
        }
          public IEnumerable<BO.OutGoingLine> GetOutGoingLineLicenseNumList()
           {
              // return from item in dl.GetOutGoingLineListWithSelectedFields((OutGoingLineDO) =>
               {
                   try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                  return new BO.OutGoingLine() { /*id=  OutGoingLineDO.ID*//* };
               })
                      let OutGoingLineBO = item as BO.OutGoingLine
                      //orderby Bus.LicenseNum
                      select OutGoingLineBO;
           }*/
        /*public void UpdateOutGoingLinePersonalDetails(BO.OutGoingLine OutGoingLine)
        {
            //Update DO.Bus            
            DO.OutGoingLine OutGoingLineDO = new DO.OutGoingLine();
            OutGoingLine.CopyPropertiesTo(OutGoingLineDO);
            try
            {
               // dl.UpdateOutGoingLine(OutGoingLineDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus's LicenseNum is illegal", Ex);
            }
        }
        public void DeleteOutGoingLine(BO.OutGoingLine OutGoingLine)
        {
            try
            {
               // dl.DeleteOutGoingLine(OutGoingLine.ToString());
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("BusLicenseNum does not exist or it is not a Bus", Ex);
            }
        }
        public void AddOutGoingLine(BO.Bus OutGoingLine)
        {

            DO.OutGoingLine OutGoingLineDO = new DO.OutGoingLine();
            if (OutGoingLine.LicenseNum.Length != 7 && OutGoingLine.LicenseNum.Length != 8)
            {
                throw new Exception("invalid license num lengh");
            }

            if ((OutGoingLine.LicenseDate.Year < 2018 && OutGoingLine.LicenseNum.Length != 7) || (OutGoingLine.LicenseDate.Year >= 2018 && OutGoingLine.LicenseDate < DateTime.Now && OutGoingLine.LicenseNum.Length != 8))
            {//check the validation of the license num accroding to it date
                throw new Exception("invalid license's date");
            }

            OutGoingLineDO.CopyPropertiesToNew(typeof(BO.OutGoingLine));
            try
            {
               // dl.AddOutGoingLine(OutGoingLineDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }

          }

        

        /*#region Accident

        /*BO.Bus GetAccident(BO.Bus bus, int num)
        {
            DO.Accident AccidentDO;
            try
            {
                AccidentDO = dl.GetAccident(num);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("the given accident didnt accured in this bus", Ex);
            }
            return BusDoBoAdapter(AccidentDO as );
        }*/
        /*  IEnumerable<BO.Bus> GetAccidentBy(Predicate<BO.Bus> predicate)
          {

          }*/
        /*
        #region Accident
        void AddAccident(BO.Bus bus)
        {

        }

        public void DeleteStation(string code)
        {
            throw new NotImplementedException();
        }

        DO.User IBL.userBoDoAdapter(User userBO)
        {
            throw new NotImplementedException();
        }

        public Bus GetAccident(Bus bus, int num)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAccidentBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        void IBL.AddAccident(Bus bus)
        {
            throw new NotImplementedException();
        }
        #endregion*/

        #region FollowingStation
        BO.FollowingStations FollowingSDoBoAdapter(DO.FollowingStations fsDO)
        {
            BO.FollowingStations fsBO = new BO.FollowingStations();
            string code = fsDO.FirstStationCode;
            try
            {
                fsDO = dl.GetFollowingStation(code);
            }
            catch (DO.BadBusLineException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("wring details", Ex);
            }

            fsDO.CopyPropertiesTo(fsBO);

            return fsBO;
        }
        public BO.FollowingStations GetFollowingStation(string code1, string code2)
        {
            DO.FollowingStations fsDO;
            try
            {
                fsDO = dl.GetFollowingStation(code1);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("station's codes are wrong", Ex);
            }
            return FollowingSDoBoAdapter(fsDO);
        }
        public IEnumerable<BO.FollowingStations> GetAllFollowingStations()
        {
            return from fsDO in dl.GetAllFollowingStationss()
                   orderby fsDO.FirstStationCode
                   select FollowingSDoBoAdapter(fsDO);
        }
        public IEnumerable<BO.FollowingStations> GetFollowingStationSBy(Predicate<BO.FollowingStations> predicate)
        {
            List<BO.FollowingStations> fs = new List<FollowingStations>();
            List<DO.FollowingStations> fs1 = dl.GetAllFollowingStationss().ToList();

            for (int i = 0; i < fs1.Count; i++)
            {
                fs.Add(FollowingSDoBoAdapter(fs1[i]));
                //  busLines1[i].CopyPropertiesTo(busLines[i]);
            }
            return from f in fs
                   where predicate(f)
                   select f;
        }
        public void UpdateFollowingStationPersonalDetails(BO.FollowingStations FollowingStation)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            FollowingStation.CopyPropertiesTo(fsDO);
            try
            {
                dl.UpdateFollowingStations(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("wrong details", Ex);
            }
        }
        public void DeleteFollowingStation(string code)
        {
            try
            {
                dl.DeleteFollowingStation(code);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("deleting failed", Ex);
            }
        }
        public void DeleteFollowingStations(BO.FollowingStations followingStation)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            followingStation.CopyPropertiesTo(fsDO);
            try
            {
                dl.DeleteFollowingStation(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("deleting failed", Ex);
            }
        }
        public void AddFollowingStation(string code1, string code2)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            if (code1.Length <= 0 || code1.Length > 6 && code2.Length < 0 || code2.Length > 6) 
            {
                throw new Exception("invalid station num lengh");
            }

            fsDO.CopyPropertiesToNew(typeof(BO.FollowingStations));
            try
            {
                dl.AddFollowingStations(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("wrong details", Ex);
            }
        }
        public double DistancefromPriviouStation(string station1 ,string station2)
        {
            DO.Station st1 = dl.GetStation(station1);
            DO.Station st2 = dl.GetStation(station2);
            //returns the distance by equation sqrt( (x-x)^2+(y-y)^2)
            double Distance = Math.Sqrt((Math.Pow(st1.longitude - st2.longitude, 2) + (Math.Pow(st1.Latitude - st2.Latitude, 2))));
            return Distance;
        }
        public TimeSpan DrivingTimeBetweenTwoStations(string station1, string station2)
        {
            double Dis = DistancefromPriviouStation( station1,station2);
            Dis = Dis * 60 / 75;//75 km per hour is a avrage of the able speed on the road for busses - 50 in the city and 100 out of the city
            int dis = Convert.ToInt32(Dis);//dont care to loose a little bit info because it is not exact but evaluieted time
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }
        public TimeSpan WalkingTimeBetweenTwoStations(string station1,string station2)
        {
            double Dis = DistancefromPriviouStation(station1, station2);
            Dis = Dis * 60 /4 ;//75 km per hour is a avrage of the able speed on the road for busses - 50 in the city and 100 out of the city
            int dis = Convert.ToInt32(Dis);//dont care to loose a little bit info because it is not exact but evaluieted time
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }
        #endregion
    }
}