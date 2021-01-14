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
            return from Bus in DataSource.BusList
                   select Bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.Bus GetBus(int LicenseNum)
        {
            DO.Bus bus = DataSource.BusList.Find(B => B.LicenseNum == LicenseNum);

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"no Bus has License Num: {LicenseNum}");
        }
        public IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate)
        {
            return from Bus in DataSource.BusList
                   select generate(Bus);
        }
        public void AddBus(DO.Bus bus)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.BusList.FirstOrDefault(B => B.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "Duplicate bus LicenseNum");
            DataSource.BusList.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus Bus)
        {
            DO.Bus bus = DataSource.BusList.Find(b => b.LicenseNum == Bus.LicenseNum);

            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
                DataSource.BusList.Add(Bus.Clone());
            }
            else
                throw new DO.BadLicenseNumException(Bus.LicenseNum, $"bad Bus id: {Bus.LicenseNum}");
        }
        public void UpdateBus(int Num, Action<DO.Bus> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteBus(int Num)
        {
            DO.Bus bus = DataSource.BusList.Find(p => p.LicenseNum == Num);

            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
            }
            else
                throw new DO.BadLicenseNumException(Num, $"bad Bus id: {Num}");
        }

        #endregion Bus 
        #region station
        public DO.Station GetStation(int CodeStation)
        {
            DO.Station station = DataSource.StationLists.Find(B => B.CodeStation == CodeStation);

            if (station != null)
                return station.Clone();
            else
                throw new DO.BadCodeStationException(CodeStation, $"no such station: {CodeStation}");
        }
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.StationLists
                   select station.Clone();
        }
        public IEnumerable<DO.Bus> GetAllStations(Predicate<DO.Station> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public IEnumerable<object> GetAllStationsWithSelectedFields(Func<DO.Station, object> generate)
        {
            return from station in DataSource.StationLists
                   select generate(station);
        }
        public void AddStation(DO.Station station)
        { //need a check if actually it is ==bus.---- or only ==CodeStation
            if (DataSource.StationLists.FirstOrDefault(B => B.CodeStation == station.CodeStation) != null)
                throw new DO.BadCodeStationException(station.CodeStation, "Duplicate station CodeStation");
            DataSource.StationLists.Add(station.Clone());
        }
        public void UpdateStation(DO.Station Station)
        {
            DO.Station station = DataSource.StationLists.Find(b => b.CodeStation == Station.CodeStation);

            if (station != null)
            {
                DataSource.StationLists.Remove(station);
                DataSource.StationLists.Add(Station.Clone());
            }
            else
                throw new DO.BadCodeStationException(Station.CodeStation, $"bad Station id: {Station.CodeStation}");
        }
        public void UpdateStation(int Num, Action<DO.Station> update) //method that knows to updt specific fields in Bus
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public void DeleteStation(int Num)
        {
            DO.Station station = DataSource.StationLists.Find(p => p.CodeStation == Num);

            if (station != null)
            {
                DataSource.StationLists.Remove(station);
            }
            else
                throw new DO.BadCodeStationException(Num, $"bad Station id: {Num}");
        }
        #endregion station
        #region BusLine
        public DO.BusLine GetBusLine(int id)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadBusLicenseNumException(id, $"bad BusLine id: {id}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            if (DataSource.BusLineList.FirstOrDefault(s => s.ID == BusLine.ID) != null)
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, "Duplicate BusLine ID");
            if (DataSource.BusLineList.FirstOrDefault(p => p.ID == BusLine.ID) == null)
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, "Missing Bus ID");
            DataSource.BusLineList.Add(BusLine.Clone());
        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.BusLineList
                   select BusLine.Clone();
        }
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<int, string, object> generate)
        {
            return from BusLine in DataSource.BusLineList
                   select generate(BusLine.ID, GetBusLine(BusLine.ID).BusNum.ToString());
        }
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from BusLine in DataSource.BusLineList
                   select generate(BusLine);
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == BusLine.ID);
            if (busl != null)
            {
                DataSource.BusLineList.Remove(busl);
                DataSource.BusLineList.Add(BusLine.Clone());
            }
            else
                throw new DO.BadBusLineException(BusLine.ID, BusLine.BusNum, $"bad BusLine id: {BusLine.ID}");
        }
        public void UpdateBusLine(int id, Action<DO.BusLine> update)
        {
            throw new NotImplementedException();//it means we need to put exeption here;//it means we need to put exeption here
        }
        public void DeleteBusLine(int id)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == id);

            if (busl != null)
            {
                DataSource.BusLineList.Remove(busl);
            }
            else
                throw new DO.BadBusLineException(id, id, $"bad BusLine id: {id}");
        }
        #endregion
        #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate)
        {
            //option A - not good!!! 
            //produces final list instead of deferred query and does not allow probus cloning:
            // return DataSource.BusStationLineList.FindAll(predicate);

            // option B - ok!!
            //Returns deferred query + clone:
            //return DataSource.BusStationLineList.Where(sic => predicate(sic)).Select(sic => sic.Clone());

            // option c - ok!!
            //Returns deferred query + clone:
            return from sic in DataSource.BusStationLineList
                   where predicate(sic)
                   select sic.Clone();
        }
        public IEnumerable<object> GetBusStationLineListWithSelectedFields(Func<DO.BusStationLine, object> generate)
        {
            return from BusSationLine in DataSource.BusStationLineList
                   select generate(BusSationLine);
        }
        public DO.BusStationLine GetBusStationLine(int id)
        {
            DO.BusStationLine busl = DataSource.BusStationLineList.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadCodeStationException(id, $"bad BusLine id: {id}");
        }
        public void AddBusStationLine(int ID, int BusStationNum)
        {
            if (DataSource.BusStationLineList.FirstOrDefault(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum)) != null)
                throw new DO.BadCodeStationException(BusStationNum, "BusStationLine code is already registered to Stations code");
            DO.BusStationLine sic = new DO.BusStationLine() { ID = ID, BusStationNum = BusStationNum };
            DataSource.BusStationLineList.Add(sic);
        }
        public void UpdateBusStationLine(DO.BusStationLine BusStationLine)
        {
            DO.BusStationLine busl = DataSource.BusStationLineList.Find(p => p.ID == BusStationLine.ID);
            if (busl != null)
            {
                DataSource.BusStationLineList.Remove(busl);
                DataSource.BusStationLineList.Add(BusStationLine.Clone());
            }
            else
                throw new DO.BadCodeStationException(BusStationLine.ID, $"bad BusLine id: {BusStationLine.ID}");
        }
        public void UpdateBusLineIndexInLineInStation(int ID, int BusStationNum, int IndexInLine)
        {
            DO.BusStationLine sic = DataSource.BusStationLineList.Find(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                sic.IndexInLine = IndexInLine;
            }
            else
                throw new DO.BadCodeStationException(BusStationNum, "Bus code is NOT registered to Stations codes");
        }
        public void DeleteBusStationLine(int BusStationNum)
        {
            DO.BusStationLine sic = DataSource.BusStationLineList.Find(cis => (cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                DataSource.BusStationLineList.Remove(sic);
            }
            else
                throw new DO.BadCodeStationException(BusStationNum, "Bus code is NOT registered to Stations codes");
        }
        public void DeleteBusStationLineFromAllStations(int ID)
        {
            DataSource.BusStationLineList.RemoveAll(p => p.ID == ID);
        }

        #endregion BusStationLine  
        #region User
        public IEnumerable<DO.User> GetAllUser()
        {//returns all members in list
            return from User in DataSource.UserList
                   select User.Clone();
        }
        public IEnumerable<DO.User> GetAllUser(Predicate<DO.User> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.User GetUser(string Name, string password)
        {
            DO.User user = DataSource.UserList.Find(B => B.UserName == Name && B.Password == password);

            if (user != null)
                return user.Clone();
            else
                throw new DO.BadUserName_PasswordException(Name, password, $"no such user: {Name}{password}");
        }
        public IEnumerable<object> GetAllUserListWithSelectedFields(Func<DO.User, object> generate)
        {
            return from User in DataSource.UserList
                   select generate(User);
        }
        public void AddUser(DO.User user)
        { //need a check if actually it is ==bus.---- or only ==Username
            if (DataSource.UserList.FirstOrDefault(B => B.UserName == user.UserName) != null)
                throw new DO.BadUserName_PasswordException(user.UserName, "Duplicate Users");
            DataSource.UserList.Add(user.Clone());
        }
        public void UpdateUser(DO.User User)
        {
            DO.User user = DataSource.UserList.Find(b => b.UserName == User.UserName);

            if (user != null)
            {
                DataSource.UserList.Remove(user);
                DataSource.UserList.Add(User.Clone());
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
            DO.User user = DataSource.UserList.Find(p => p.UserName == name);

            if (user != null)
            {
                DataSource.UserList.Remove(user);
            }
            else
                throw new DO.BadUserName_PasswordException(name, $"User name : {name}");
        }

        #endregion Bus 
        #region UserDrive
   /*     public IEnumerable<object> GetAllUserDriveListWithSelectedFields(Func<DO.UserDrive, object> generate)
        {
            return from UserDrive in DataSource.UserDriveList
                   select generate(UserDrive);
        }*/
        public IEnumerable<DO.UserDrive> GetAllUserDrive()
        {//returns all members in list
            return from UserDrive in DataSource.UserDriveList
                   select UserDrive.Clone();
        }
        public IEnumerable<DO.UserDrive> GetAllUserDrive(Predicate<DO.UserDrive> predicate)
        {
            throw new NotImplementedException();//it means we need to put exeption here;
        }
        public DO.UserDrive GetUserDrive(string Name)
        {
            DO.UserDrive userDrive = DataSource.UserDriveList.Find(B => B.UserName == Name);

            if (userDrive != null)
                return userDrive.Clone();
            else
                throw new DO.BadUserDriveNameException(Name, $"no userDrive has that name: {Name}");
        }
        public void AddUserDrive(DO.UserDrive userDrive)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.UserDriveList.FirstOrDefault(B => B.UserName == userDrive.UserName) != null)
                throw new DO.BadUserDriveNameException(userDrive.UserName, "Duplicate User's drives names");
            DataSource.UserDriveList.Add(userDrive.Clone());
        }
        public void UpdateUserDrive(DO.UserDrive UserDrive)
        {
            DO.UserDrive userDrive = DataSource.UserDriveList.Find(b => b.UserName == UserDrive.UserName);

            if (UserDrive != null)
            {
                DataSource.UserDriveList.Remove(UserDrive);
                DataSource.UserDriveList.Add(UserDrive.Clone());
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
            DO.UserDrive user = DataSource.UserDriveList.Find(p => p.UserName == name);

            if (user != null)
            {
                DataSource.UserDriveList.Remove(user);
            }
            else
                throw new DO.BadUserDriveNameException(name, $"UserDrive name : {name}");
        }
        #endregion UserDrive 
        #region OutGoingLine

        //DO.OutGoingLine GetOutGoingLine(int Num);
        //IEnumerable<DO.OutGoingLine> GetAllOutGoingLines(Predicate<DO.OutGoingLine> predicate);

        public DO.OutGoingLine GetOutGoingLine(int ID)
        {
            DO.OutGoingLine outGoingLine = DataSource.OutGoingLineList.Find(p => p.ID == ID);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (outGoingLine != null)
                return outGoingLine.Clone();
            else
                throw new DO.BadCodeStationException(ID, $"bad BusLine id: {ID}");
        }
        public IEnumerable<DO.OutGoingLine> GetAllOutGoingLines(Predicate<DO.OutGoingLine> predicate)
        {
            return from sic in DataSource.OutGoingLineList
                   where predicate(sic)
                   select sic.Clone();
        }
        public IEnumerable<object> GetOutGoingLineListWithSelectedFields(Func<DO.OutGoingLine, object> generate)
        {
            return from OutGoingLine in DataSource.OutGoingLineList
                   select generate(OutGoingLine);
        }
        public void AddOutGoingLine(DO.OutGoingLine OutGoingLine)
        {
            if (DataSource.OutGoingLineList.FirstOrDefault(cis => (cis.ID == OutGoingLine.ID)) != null)
                throw new DO.BadBusLicenseNumException(OutGoingLine.ID, "Bus ID is already registered AND OUT");
            DO.BusStationLine sic = new DO.BusStationLine() { ID = OutGoingLine.ID };
            DataSource.BusStationLineList.Add(sic);
        }
        public void DeleteOutGoingLine(int Num)
        {
            DO.OutGoingLine sic = DataSource.OutGoingLineList.Find(cis => (cis.ID == Num));

            if (sic != null)
            {
                DataSource.OutGoingLineList.Remove(sic);
            }
            else
                throw new DO.BadBusLineException(Num, Num, "Bus ID is NOT registered to Station ID");
        }
        #endregion
    }
}
