using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BL
{
    class BLImp : IBL //internal
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
                                  select (DO.Accident)Accident.CopyPropertiesToNew(typeof(DO.Accident));


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
            return from BusDO in dl.GetBusIDList()
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
            Bus.CopyPropertiesTo(BusDO);
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
        public IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }
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
            Station.CopyPropertiesTo(StationDO);
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
        BO.BusStationLine BusStationLineDoBoAdapter(DO.BusStationLine BusStationLineDO)
        {
            BO.BusStationLine BusStationLineBO = new BO.BusStationLine();
            string CodeStation = BusStationLineDO.BusStationNum;
            try
            {
                BusStationLineDO = dl.GetBusStationLine(CodeStation);
            }
            catch (DO.BadBusStationLineCodeException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum is illegal", Ex);
            }

            BusStationLineDO.CopyPropertiesTo(BusStationLineBO);

            return BusStationLineBO;
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
        public IEnumerable<BO.BusStationLine> GetAllBusStationLines()
        {
            return from BusStationLineDO in dl.GetBusStationLineList()
                   orderby BusStationLineDO.ID
                   select BusStationLineDoBoAdapter(BusStationLineDO);
        }
        public IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate)
        {//need fill field 
            throw new NotImplementedException();
        }
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
            //Update DO.Bus            
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            BusStationLine.CopyPropertiesTo(BusStationLineDO);
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
        public void DeleteBusStationLine(BO.BusStationLine bus_station_line)
        {
            try
            {
                string BusStationLine = bus_station_line.ToString();
                dl.DeleteBusStationLine(BusStationLine);
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }

        #endregion

        #region BusLine
        BO.BusLine BusLineDoBoAdapter(DO.BusLine BusLineDO)
        {
            BO.BusLine BusLineBO = new BO.BusLine();
            int LicenseNum = BusLineDO.BusNum;
            try
            {
                BusLineDO = dl.GetBusLine(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("Bus LicenseNum is illegal", ex);
            }

            BusLineDO.CopyPropertiesTo(BusLineBO);

            BusLineBO.AccidentsDuco = from sic in dl.GetAllAccidentsList(sic => sic.LicenseNum == LicenseNum)
                                  let Accident = dl.GetAccident(sic.AccidentNum)
                                  select (DO.Accident)Accident.CopyPropertiesToNew(typeof(DO.Accident));


            return BusLineBO;
        }
        public IEnumerable<BO.BusLine> GetAllLinesByArea(BO.Area area)
        {
            return from LineDO in dl.GetAllBusLines()
                   where LineDO.Area.CompareTo((DO.Area)area) == 0//if the erea is equal to the given area
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        public IEnumerable<BO.BusStationLine> GetAllLineStationsPerLine(int code)
        {
            return from lineStation in dl.GetLineStationsListThatMatchAStation(code)
                   let line = dl.GetBusLine(lineStation.LineId)
                   select line.CopyDOLineStationToBOLine(lineStation);
        }
        public IEnumerable<BO.BusLine> GetBusLines()
        {
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.ID
                   select BusLineDoBoAdapter(BusLineDO);
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
            return BusLineDoBoAdapter(BusLineDO);
        }
        public IEnumerable<BO.BusLine> GetBusLineIDList()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.IsDeleted
                   select BusLineDoBoAdapter(BusLineDO);
        }
        public IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.BusLine> GetBusLineLicenseNumList()
        {
            return from item in dl.GetBusLineListWithSelectedFields((BusLineDO,null) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.BusLine() { LicenseNum = BusLineDO.LicenseNum };
            })
                   let BusLineBO = item as BO.BusLine
                   //orderby Bus.LicenseNum
                   select BusLineBO;
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
            if (busline.LicenseNum.Length != 7 && busline.LicenseNum.Length != 8) 
            {
                throw new Exception("invalid license num lengh");
            }

            if ((busline.LicenseDate.Year < 2018 && busline.LicenseNum.Length != 7) || (busline.LicenseDate.Year >= 2018 && busline.LicenseDate < DateTime.Now && busline.LicenseNum.Length != 8))
            {//check the validation of the license num accroding to it date
                throw new Exception("invalid license's date");
            }

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
        BO.User UserDoBoAdapter(DO.User UserDO)
        {
            BO.User UserBO = new BO.User();
            string password = UserDO.Password;
            string name = UserDO.UserName;
            try
            {
                UserDO = dl.GetUser(name,password);
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("User's detatils ar'nt legal", Ex);
            }

            UserDO.CopyPropertiesTo(UserBO);

            return UserBO;
        }
        public BO.User GetUser(string name,string password)
        {
            DO.User UserDO;
            try
            {
                UserDO = dl.GetUser(name,password);
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadUserName_PasswordException("User's password does not exist or you are a new user", Ex);
            }
            return UserDoBoAdapter(UserDO);
        }
        public IEnumerable<BO.User> GetAllUsers()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from UserDO in dl.GetAllUser()
                   orderby UserDO.UserName
                   select UserDoBoAdapter(UserDO);
        }
        public IEnumerable<BO.User> GetUserIDList()
        {
            return from UserDO in dl.GetUserListWithSelectedFields()
                   orderby UserDO.Password
                   select UserDoBoAdapter(UserDO);
        }
        public IEnumerable<BO.User> GetUserBy(Predicate<BO.User> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate)
        {
            return from item in dl.GetUserListWithSelectedFields((UserDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.User() { UserName = UserDO.UserName };
            })
                   let UserBO = item as BO.User
                   //orderby Bus.LicenseNum
                   select UserBO;
        }
        public void UpdateUserPersonalDetails(BO.User User)
        {
            //Update DO.Bus            
            DO.User UserDO = new DO.User();
            User.CopyPropertiesTo(UserDO);
            try
            {
                dl.UpdateUser(UserDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadUserName_PasswordException("User's details are illegal", Ex);
            }
        }
        public void DeleteUser(string name,string password)
        {
            try
            {
                dl.DeleteUser(name,password);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadUserName_PasswordException("User's details does not exist or you are a new user", Ex);
            }
        }
        public void AddUser(BO.User User)
        {

            DO.User UserDO = new DO.User();

            if(UserDO.UserName.Length==0 && UserDO.Password.Length==0)
            {
                throw new Exception("the details are wrong");
            }
            UserDO.CopyPropertiesToNew(typeof(BO.User));
            try
            {
                dl.AddUser(UserDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadUserName_PasswordException("User's details are illegal", Ex);
            }
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
        public IEnumerable<BO.OutGoingLine> GetOutGoingLinesBy(Predicate<BO.OutGoingLine> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.OutGoingLine> GetOutGoingLineLicenseNumList()
        {
            return from item in dl.GetOutGoingLineListWithSelectedFields((OutGoingLineDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.OutGoingLine() { id = OutGoingLineDO.ID };
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

            DO.Bus OutGoingLineDO = new DO.Bus();
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
                dl.AddOutGoingLine(OutGoingLineDO.ToString());
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }
        }

        #endregion OutGoingLine
    }
}