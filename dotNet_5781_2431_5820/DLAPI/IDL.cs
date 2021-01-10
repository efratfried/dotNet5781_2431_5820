using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using DO;

namespace DLAPI
{//basic on the "CRUD" rule - Create_Request_Update_Delete
    public interface IDL
    {
        /*#region Bus
        int AddBus(Bus Bus);
        Bus GetBus(int BusNum);
        void UpdateBus(Bus Bus);
        void DeleteBus(Bus Bus);
        #endregion
        #region Station
        int AddStation(Station Station);
        Station GetStation(int CodeStation);
        void UpdateStation(Station Station);
        void DeleteStation(Station Station);
        #endregion
        #region User
        int AddUser(User User);
        User GetUserName(int password);
        User GetUserPassword(int PAssword);
        void UpdateUser(User Userr);
        void DeleteUser(User User);
        #endregion
        #region Adress
        int AddAdress(Adress Adress);
        void GetAdress(string Street, int num, string city);
        void UpdateAdress(Adress Adress);
        void DeleteAdress(Adress Adress);
        #endregion
        #region BusLine
        int AddBusLine(BusLine BusLine);
        BusLine GetBusLine(int BusNum);
        void UpdateBusLine(BusLine BusLine);
        void DeleteBusLine(BusLine BusLine);
        #endregion
        #region BusStationLine
        int AddBusStationLine(BusStationLine BusStationLine);
        BusStationLine GetBusStationLine(int BusNum, int CodeStation);
        void UpdateBusStationLine(BusStationLine BusStationLine);
        void DeleteBusStationLine(BusStationLine BusStationLine);
        #endregion
        #region DrivingBus
        int AddDrivingLine(DrivingBus DrivingBus);
        DrivingBus GetDrivingBus(int BusNum);
        void UpdateDrivingLine(DrivingBus DrivingBus);
        void DeleteDrivingLine(DrivingBus DrivingBus);
        #endregion
        #region Location
        int AddLocation(Location Location);
        Location GetLocation(double latitude, double longtitude);
        void UpdateLocation(Location Location);
        void DeleteLocation(Location Location);
        #endregion
        #region OutGoingLine
        int AddOutGoingLine(OutGoingLine OutGoingLine);
        OutGoingLine GetOutGoingLine(OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(OutGoingLine OutGoingLine);
        void DeleteOutGoingLine(OutGoingLine OutGoingLine);
        #endregion
        #region UserDrive
        int AddUserDrive(UserDrive UserDrive);
        UserDrive GetUserDrive(UserDrive UserDrive);
        void UpdateUserDrive(UserDrive UserDrive);
        void DeleteUserDrive(UserDrive UserDrive);

        #endregion
        #region FollowingStations
        int AddFolowingStations(int FirstCodeStation, int SecondCodeStation);
        FollowingStations GetFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void UpdateFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void DeleteFollowingStations(int FirstCodeStation, int SecondCodeStation);
        #endregion
        */

        #region Bus
        IEnumerable<DO.Bus> GetAllBusses();
        IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate);
        DO.Bus GetBus(int Num);
        void AddBus(DO.Bus Bus);
        void UpdateBus(DO.Bus Bus);
        void UpdateBus(int Num, Action<DO.Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(int Num);
        #endregion

        #region Station
        DO.Station GetStation(int Num);
        IEnumerable<DO.Station> GetAllStations();
        IEnumerable<object> GetStationListWithSelectedFields(Func<DO.Station, object> generate);
        void AddStation(DO.Station Station);
        void UpdateStation(DO.Station Station);
        void UpdateStation(int Num, Action<DO.Station> update); //method that knows to updt specific fields in Student
        void DeleteStation(int Num); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region User
        IEnumerable<DO.User> GetAllUser();
        IEnumerable<DO.User> GetUserDrive(Predicate<DO.User> predicate);
        DO.Bus GetUser(int Num);
        void AddUser(DO.User User);
        void UpdateUser(DO.User User);
        void UpdateUser(int Num, Action<DO.User> update); //method that knows to updt specific fields in Person
        void DeleteUser(int Num);
        #endregion

        #region UserDrive
        IEnumerable<DO.UserDrive> GetAllUserDrive();
        IEnumerable<DO.UserDrive> GetUserDrive(Predicate<DO.UserDrive> predicate);
        DO.Bus GetUserDrive(int Num);
        void AddUserDrive(DO.UserDrive UserDrive);
        void UpdateUserDrive(DO.UserDrive UserDrive);
        void UpdateUserDrive(int Num, Action<DO.UserDrive> update); //method that knows to updt specific fields in Person
        void DeleteUserDrive(int Num);
        #endregion

        #region BusLine
        DO.BusLine GetBusLine(int Num);
        IEnumerable<DO.BusLine> GetAllBusLines();
        IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate);  
        void AddBusLine(DO.BusLine BusLine);
        void UpdateBusLine(DO.BusLine BusLine);
        void UpdateBusLine(int Num, Action<DO.BusLine> update); //method that knows to updt specific fields in BusLine
        void DeleteBusLine(int Num); // removes only BusLine, does not remove the appropriate Bus...
        #endregion

        #region OutGoingLine
        DO.OutGoingLine GetOutGoingLine(int Num);
        IEnumerable<DO.OutGoingLine> GetAllOutGoingLines();
        IEnumerable<object> GetOutGoingLineListWithSelectedFields(Func<DO.OutGoingLine, object> generate);
        void AddOutGoingLine(DO.OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(DO.OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(int Num, Action<DO.OutGoingLine> update); //method that knows to updt specific fields in OutGoingLine
        void DeleteOutGoingLine(int Num); // removes only OutGoingLine, does not remove the appropriate Bus...
        #endregion

        #region BusStationLine
        DO.BusStationLine GetBusStationLine(int Num);
        IEnumerable<DO.BusStationLine> GetAllBusStationLine();
        IEnumerable<object> GetBusStationLineListWithSelectedFields(Func<DO.BusStationLine, object> generate);
        void AddBusStationLine(DO.BusStationLine BusStationLine);
        void UpdateBusStationLine(DO.BusStationLine BusStationLine);
        void UpdateBusStationLinee(int Num, Action<DO.BusStationLine> update); //method that knows to updt specific fields in BusStationLine
        void DeleteBusStationLine(int Num); // removes only BusStationLine, does not remove the appropriate Bus...
        #endregion
    }
}
