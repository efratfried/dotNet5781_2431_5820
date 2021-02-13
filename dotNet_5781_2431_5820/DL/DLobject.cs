using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLAPI;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance probusty to use
        #endregion
        //Implement IDL methods, CRUD
        #region Bus
        public IEnumerable<DO.Bus> GetAllBusses()
        {//returns all members in list
            return from Bus in DataSource.BussesList
                   select Bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        public DO.Bus GetBus(string LicenseNum)
        {
            DO.Bus bus = DataSource.BussesList.Find(B => B.LicenseNum == LicenseNum);

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"no Bus has License Num: {LicenseNum}");
        }
        public IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate)
        {
            return from Bus in DataSource.BussesList
                   select generate(Bus);
        }
        public void AddBus(DO.Bus bus)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.BussesList.FirstOrDefault(B => B.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "Duplicate bus LicenseNum");
            DataSource.BussesList.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus Bus)
        {
            DO.Bus bus = DataSource.BussesList.Find(b => b.LicenseNum == Bus.LicenseNum);

            if (bus != null)
            {
                DataSource.BussesList.Remove(bus);
                DataSource.BussesList.Add(Bus.Clone());
            }
            else
                throw new DO.BadLicenseNumException(Bus.LicenseNum, $"bad Bus id: {Bus.LicenseNum}");
        }
        public void UpdateBus(string Num, Action<DO.Bus> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteBus(string Num)
        {
            DO.Bus bus = DataSource.BussesList.Find(p => p.LicenseNum == Num);

            if (bus != null)
            {
                DataSource.BussesList.Remove(bus);
            }
            else
                throw new DO.BadLicenseNumException(Num, $"bad Bus id: {Num}");
        }

        #endregion Bus
        
        #region station
        public DO.Station GetStation(string CodeStation)
        {
            DO.Station station = DataSource.StationsList.Find(B => B.CodeStation == CodeStation);

            if (station != null)
                return station.Clone();
            else
                throw new DO.BadCodeStationException(CodeStation, $"no such station: {CodeStation}");
        }
        public IEnumerable<DO.Station> GetAllStations(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.StationsList
                   select station.Clone();
        }
        public IEnumerable<object> GetAllStationListWithSelectedFields(Func<DO.Station, object> generate)
        {
            return from station in DataSource.StationsList
                   select generate(station);
        }
        public void AddStation(DO.Station station)
        { //need a check if actually it is ==bus.---- or only ==CodeStation
            if (DataSource.StationsList.FirstOrDefault(B => B.CodeStation == station.CodeStation) != null)
                throw new DO.BadCodeStationException(station.CodeStation, "Duplicate station CodeStation");
            DataSource.StationsList.Add(station.Clone());
        }
        public void UpdateStation(DO.Station Station)
        {
            DO.Station station = DataSource.StationsList.Find(b => b.CodeStation == Station.CodeStation);

            if (station != null)
            {
                DataSource.StationsList.Remove(station);
                DataSource.StationsList.Add(Station.Clone());
            }
            else
                throw new DO.BadCodeStationException(Station.CodeStation, $"bad Station id: {Station.CodeStation}");
        }
        public void UpdateStation(string CodeStation, Action<DO.Station> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteStation(string CodeStation)
        {
            DO.Station station = DataSource.StationsList.Find(p => p.CodeStation == CodeStation);

            if (station != null)
            {
                DataSource.StationsList.Remove(station);
            }
            else
                throw new DO.BadCodeStationException(CodeStation, $"bad Station id: {CodeStation}");
        }

        #endregion station

        #region BusLine
        public DO.BusLine GetBusLine(string id)
        {
            DO.BusLine busl = DataSource.BusLinesList.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadBusLicenseNumException(id, $"bad BusLine id: {id}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            if (DataSource.BusLinesList.FirstOrDefault(s => s.ID == BusLine.ID) != null)
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, "Duplicate BusLine ID");
            if (DataSource.BusLinesList.FirstOrDefault(p => p.ID == BusLine.ID) == null)
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, "Missing Bus ID");
            DataSource.BusLinesList.Add(BusLine.Clone());
        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.BusLinesList
                   select BusLine.Clone();
        }
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from BusLine in DataSource.BusLinesList
                   select generate(BusLine.ID, GetBusLine(BusLine.ID).BusNum.ToString());
        }
        public IEnumerable<object> GetBusLinesListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from BusLine in DataSource.BusLinesList
                   select generate(BusLine);
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            DO.BusLine busl = DataSource.BusLinesList.Find(p => p.ID == BusLine.ID);
            if (busl != null)
            {
                DataSource.BusLinesList.Remove(busl);
                DataSource.BusLinesList.Add(BusLine.Clone());
            }
            else
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, $"bad BusLine id: {BusLine.ID}");
        }
        public void UpdateBusLine(string id, Action<DO.BusLine> update)
        {
            throw new NotImplementedException();//it means we need to put exeption here;//it means we need to put exeption here
        }
        public void DeleteBusLine(string id)
        {
            DO.BusLine busl = DataSource.BusLinesList.Find(p => p.ID == id);

            if (busl != null)
            {
                DataSource.BusLinesList.Remove(busl);
            }
            else
                throw new DO.BadBusLineException(id, id, $"bad BusLine id: {id}");
        }
        #endregion

        #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetAllBusStationLines(string busstationline)
        {//returns all members in list
            return from BusStationLine in DataSource.BusStationsLineList
                   select BusStationLine.Clone();
        }
        public IEnumerable<DO.BusStationLine> GetBusStationsLineList(Predicate<DO.BusStationLine> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public IEnumerable<object> GetBusStationsLineListWithSelectedFields(Func<DO.BusStationLine, object> generate)
        {
            return from BusSationLine in DataSource.BusStationsLineList
                   select generate(BusSationLine);
        }
        public DO.BusStationLine GetBusStationLine(string id)
        {
            DO.BusStationLine busl = DataSource.BusStationsLineList.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadCodeStationException(id, $"bad BusLine id: {id}");
        }
        public void AddBusStationLine(string ID, string BusStationNum)
        {
            if (DataSource.BusStationsLineList.FirstOrDefault(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum)) != null)
                throw new DO.BadCodeStationException(BusStationNum, "BusStationLine code is already registered to Stations code");
            DO.BusStationLine sic = new DO.BusStationLine() { ID = ID, BusStationNum = BusStationNum };
            DataSource.BusStationsLineList.Add(sic);
        }
        public void UpdateBusStationLine(DO.BusStationLine BusStationLine)
        {
            DO.BusStationLine busl = DataSource.BusStationsLineList.Find(p => p.ID == BusStationLine.ID);
            if (busl != null)
            {
                DataSource.BusStationsLineList.Remove(busl);
                DataSource.BusStationsLineList.Add(BusStationLine.Clone());
            }
            else
                throw new DO.BadCodeStationException(BusStationLine.ID, $"bad BusLine id: {BusStationLine.ID}");
        }
        public void UpdateBusLineIndexInLineInStation(string ID, string BusStationNum, int IndexInLine)
        {
            DO.BusStationLine sic = DataSource.BusStationsLineList.Find(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                sic.IndexInLine = IndexInLine;
            }
            else
                throw new DO.BadCodeStationException(BusStationNum, "Bus code is NOT registered to Stations codes");
        }
        public void DeleteBusStationLine(string BusStationNum)
        {
            DO.BusStationLine sic = DataSource.BusStationsLineList.Find(cis => (cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                DataSource.BusStationsLineList.Remove(sic);
            }
            else
                throw new DO.BadCodeStationException(BusStationNum, "Bus code is NOT registered to Stations codes");
        }
        public void DeleteBusStationLineFromAllStations(string ID)
        {
            DataSource.BusStationsLineList.RemoveAll(p => p.ID == ID);
        }

        #endregion BusStationLine  

        #region User
        public IEnumerable<DO.User> GetAllUser()
        {//returns all members in list
            return from User in DataSource.UsersList
                   select User.Clone();
        }
        public IEnumerable<DO.User> GetAllUser(Predicate<DO.User> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.User GetUser(string Name, string password)
        {
            DO.User user = DataSource.UsersList.Find(B => B.UserName == Name && B.Password == password);

            if (user != null)
                return user.Clone();
            else
                throw new DO.BadUserName_PasswordException(Name, password, $"no such user: {Name}{password}");
        }
        public IEnumerable<object> GetAllUsersListWithSelectedFields(Func<DO.User, object> generate)
        {
            return from User in DataSource.UsersList
                   select generate(User);
        }
        public void AddUser(DO.User user)
        { //need a check if actually it is ==bus.---- or only ==Username
            if (DataSource.UsersList.FirstOrDefault(B => B.UserName == user.UserName) != null)
                throw new DO.BadUserName_PasswordException(user.UserName, "Duplicate Users");
            DataSource.UsersList.Add(user.Clone());
        }
        public void UpdateUser(DO.User User)
        {
            DO.User user = DataSource.UsersList.Find(b => b.UserName == User.UserName);

            if (user != null)
            {
                DataSource.UsersList.Remove(user);
                DataSource.UsersList.Add(User.Clone());
            }
            else
                throw new DO.BadUserName_PasswordException(User.UserName, $"bad user name: {User.UserName}");
        }
        public void UpdateUser(string name, Action<DO.User> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteUser(string name)
        {
            DO.User user = DataSource.UsersList.Find(p => p.UserName == name);

            if (user != null)
            {
                DataSource.UsersList.Remove(user);
            }
            else
                throw new DO.BadUserName_PasswordException(name, $"User name : {name}");
        }

        #endregion Bus 

        #region UserDrive
   /*     public IEnumerable<object> GetAllUserDrivesListWithSelectedFields(Func<DO.UserDrive, object> generate)
        {
            return from UserDrive in DataSource.UserDrivesList
                   select generate(UserDrive);
        }*/
        public IEnumerable<DO.UserDrive> GetAllUserDrive()
        {//returns all members in list
            return from UserDrive in DataSource.UserDrivesList
                   select UserDrive.Clone();
        }
        public IEnumerable<DO.UserDrive> GetAllUserDrive(Predicate<DO.UserDrive> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.UserDrive GetUserDrive(string Name)
        {
            DO.UserDrive userDrive = DataSource.UserDrivesList.Find(B => B.UserName == Name);

            if (userDrive != null)
                return userDrive.Clone();
            else
                throw new DO.BadUserDriveNameException(Name, $"no userDrive has that name: {Name}");
        }
        public void AddUserDrive(DO.UserDrive userDrive)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.UserDrivesList.FirstOrDefault(B => B.UserName == userDrive.UserName) != null)
                throw new DO.BadUserDriveNameException(userDrive.UserName, "Duplicate User's drives names");
            DataSource.UserDrivesList.Add(userDrive.Clone());
        }
        public void UpdateUserDrive(DO.UserDrive UserDrive)
        {
            DO.UserDrive userDrive = DataSource.UserDrivesList.Find(b => b.UserName == UserDrive.UserName);

            if (UserDrive != null)
            {
                DataSource.UserDrivesList.Remove(UserDrive);
                DataSource.UserDrivesList.Add(UserDrive.Clone());
            }
            else
                throw new DO.BadUserDriveNameException(UserDrive.UserName, $"bad user name: {UserDrive.UserName}");
        }
        public void UpdateUserDrive(string name, Action<DO.UserDrive> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteUserDrive(string name)
        {
            DO.UserDrive user = DataSource.UserDrivesList.Find(p => p.UserName == name);

            if (user != null)
            {
                DataSource.UserDrivesList.Remove(user);
            }
            else
                throw new DO.BadUserDriveNameException(name, $"UserDrive name : {name}");
        }
        #endregion UserDrive 

        #region OutGoingLine

        //DO.OutGoingLine GetOutGoingLine(int Num);
        //IEnumerable<DO.OutGoingLine> GetAllOutGoingLines(Predicate<DO.OutGoingLine> predicate);

        public DO.OutGoingLine GetOutGoingLine(string ID)
        {
            DO.OutGoingLine outGoingLine = DataSource.OutGoingLinesList.Find(p => p.ID == ID);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (outGoingLine != null)
                return outGoingLine.Clone();
            else
                throw new DO.BadCodeStationException(ID, $"bad BusLine id: {ID}");
        }
        public IEnumerable<DO.OutGoingLine> GetAllOutGoingLines(Predicate<DO.OutGoingLine> predicate)
        {
            return from sic in DataSource.OutGoingLinesList
                   where predicate(sic)
                   select sic.Clone();
        }
        public IEnumerable<object> GetOutGoingLinesListWithSelectedFields(Func<DO.OutGoingLine, object> generate)
        {
            return from OutGoingLine in DataSource.OutGoingLinesList
                   select generate(OutGoingLine);
        }
        public void AddOutGoingLine(DO.OutGoingLine OutGoingLine)
        {
            if (DataSource.OutGoingLinesList.FirstOrDefault(cis => (cis.ID == OutGoingLine.ID)) != null)
                throw new DO.BadBusLicenseNumException(OutGoingLine.ID, "Bus ID is already registered AND OUT");
            DO.BusStationLine sic = new DO.BusStationLine() { ID = OutGoingLine.ID };
            DataSource.BusStationsLineList.Add(sic);
        }
        public void DeleteOutGoingLine(string Num)
        {
            DO.OutGoingLine sic = DataSource.OutGoingLinesList.Find(cis => (cis.ID == Num));

            if (sic != null)
            {
                DataSource.OutGoingLinesList.Remove(sic);
            }
            else
                throw new DO.BadBusLineException(Num, Num, "Bus ID is NOT registered to Station ID");
        }
        #endregion

        #region Accident
        public IEnumerable<DO.Accident> GetAllAccidentsList(Predicate<DO.Accident> predicate)
        {
            return from Accident in DataSource.AccidentsList
                   select Accident.Clone();
        }
        public IEnumerable<DO.Accident> GetAllAccidents()
        {//returns all members in list
            return from Accident in DataSource.AccidentsList
                   select Accident.Clone();
        }
        public IEnumerable<DO.Accident> GetAllAccidents(Func<DO.Accident, object> generate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.Accident GetAccident(int AccidentNum)
        {
            DO.Accident Accident = DataSource.AccidentsList.Find(B => B.AccidentNum == AccidentNum);

            if (Accident != null)
                return Accident.Clone();
            else
                throw new DO.BadBusLicenseNumException(AccidentNum, $"no Accident like that: {AccidentNum}");
        }
        public IEnumerable<object> GetAccidentListWithSelectedFields(Func<DO.Accident, object> generate)
        {
            return from Accident in DataSource.AccidentsList
                   select generate(Accident);
        }
        public void AddAccident(DO.Accident Accident)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.AccidentsList.FirstOrDefault(B => B.AccidentNum == Accident.AccidentNum) != null)
                throw new DO.BadLicenseNumException(Accident.AccidentNum, "Duplicate bus LicenseNum");
            DataSource.AccidentsList.Add(Accident.Clone());
        }
        public void UpdateAccident(DO.Accident Accident)
        {
            DO.Accident Accidents = DataSource.AccidentsList.Find(b => b.AccidentNum == Accident.AccidentNum);

            if (Accident != null)
            {
                DataSource.AccidentsList.Remove(Accident);
                DataSource.AccidentsList.Add(Accident.Clone());
            }
            else
                throw new DO.BadLicenseNumException(Accident.LicenseNum, $"bad Bus id: {Accident.LicenseNum}");
        }
        public void DeleteAccident(int AccidentNum)
        {
            DO.Accident Accident = DataSource.AccidentsList.Find(p => (p.AccidentNum == AccidentNum));

            if (Accident != null)
            {
                DataSource.AccidentsList.Remove(Accident);
            }
            else
                throw new DO.BadLicenseNumException(AccidentNum, $"bad Bus id: {AccidentNum}");
        }

        #endregion
    }
}
