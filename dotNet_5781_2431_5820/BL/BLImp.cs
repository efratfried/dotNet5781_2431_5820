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

            BusBO.AccidentsDuco = from sic in dl.GetAllAccidentsList(sic => sic.LicenseNum == LicenseNum)
                                  let Accident = dl.GetAccident(sic.AccidentNum)
                                  select (BO.Accident)Accident.CopyPropertiesToNew(typeof(BO.Accident));

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
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
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
            if (bus.KM > 20000 || bus.foul < 100)
            {
                bus.Status = Status.UnAvailable;
            }
            else if (bus.drivingBusesDuco.Last().finish == false)
            {
                bus.Status = Status.OnDrive;
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
                throw new BO.BadStationException("Bus LicenseNum is illegal", Ex);
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
            catch (DO.BadLicenseNumException ex)
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
        public void DeleteStation(Station station)
        {
            try
            {
                dl.DeleteStation(station.CodeStation);
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
 
            double rochav = (rand.NextDouble() * rand.NextDouble()) % 2.4 + 31;
            double orech = (rand.NextDouble() * rand.NextDouble()) % 1.4 + 34.3;

            if (station.Latitude!=rochav || station.longitude!=orech)
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
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("station num is illegal", Ex);
            }
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
            return from item in dl.GetBusStationsLineListWithSelectedFields((BusStationLineDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.BusStationLine() { ID = BusStationLineDO.ID };
            })
                   let BusStationLineBo = item as BO.BusStationLine
                   //orderby Bus.LicenseNum
                   select BusStationLineBo;
        }
        public void AddBusStationLine(BusStationLine busStationLine)
        {
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            BusStationLineDO.CopyPropertiesToNew(typeof(BO.BusStationLine));
            try
            {
                dl.AddBusStationLine(BusStationLineDO.ID, busStationLine.BusStationNum);
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
        public IEnumerable<BO.BusStationLine> GetAllBusStationLinesPerLine(int lineId)
        {
            return from DOlineStation in dl.GetBusStationLinesListThatMatchAStation(lineId.ToString())
                   let BOlineStation = BusStationLineDoBoAdapter(DOlineStation)
                   select BOlineStation;
        }
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
            int busNumber = blDO.BusNum;
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
        public IEnumerable<BO.BusLine> GetAllLinesByArea(BO.Area area)
        {
            return from LineDO in dl.GetAllBusLines()
                   where LineDO.Area.CompareTo((DO.Area)area) == 0//if the erea is equal to the given area
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        public IEnumerable<BO.BusLine> GetAllLinesPerStation(int code)
        {
            return from lineStation in dl.GetBusStationLinesListThatMatchAStation(code.ToString())
                   let line = dl.GetBusLine(int.Parse(lineStation.ID))
                   select line.CopyDOLineStationToBOLine(lineStation);
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
                throw new BO.BadBusLineIdException("Buss' LicenseNum does not exist or it is not a Bus", ex);
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
        {
            try
            {
                string id = ID.ToString();
                dl.DeleteBus(id);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("BusLine's ID does not exist or it is not a Bus", ex);
            }
        }
        public void AddBusLine(BO.BusLine busline)
        {
            DO.BusLine BusLineDO = new DO.BusLine();

            BusLineDO.CopyPropertiesToNew(typeof(BO.BusLine));
            try
            {
                dl.AddBusLine(BusLineDO);
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
                throw new BO.BadUserName_PasswordException("ERROR!\n", ex);
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
        /*public IEnumerable<BO.OutGoingLine> GetOutGoingLinesBy(Predicate<BO.OutGoingLine> predicate)
        {
            throw new NotImplementedException();
        }*/
        public IEnumerable<BO.OutGoingLine> GetOutGoingLineLicenseNumList()
        {
            return from item in dl.GetOutGoingLineListWithSelectedFields((OutGoingLineDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
               return new BO.OutGoingLine() { /*id=  OutGoingLineDO.ID*/ };
            })
                   let OutGoingLineBO = item as BO.OutGoingLine
                   //orderby Bus.LicenseNum
                   select OutGoingLineBO;
        }
        public void UpdateOutGoingLinePersonalDetails(BO.OutGoingLine OutGoingLine)
        {
            //Update DO.Bus            
            DO.OutGoingLine OutGoingLineDO = new DO.OutGoingLine();
            OutGoingLine.CopyPropertiesTo(OutGoingLineDO);
            try
            {
                dl.UpdateOutGoingLine(OutGoingLineDO);
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
                dl.DeleteOutGoingLine(OutGoingLine.ToString());
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
                dl.AddOutGoingLine(OutGoingLineDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }

          }

        #endregion OutGoingLine

        #region Accident

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
        #endregion
    }
}