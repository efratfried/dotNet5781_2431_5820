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
        #region Bus
        IEnumerable<DO.Bus> GetAllBusses();
        IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate);
        //IEnumerable<DO.Bus> GetBusIDList();
        IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate);
        DO.Bus GetBus(string Num);
        void AddBus(DO.Bus Bus);
        void UpdateBus(DO.Bus Bus);
        //void UpdateBus(string Num, Action<DO.Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(string Num);
      
        #endregion

        #region BusLine
        DO.BusLine GetBusLine(int id);
        IEnumerable<DO.BusLine> GetAllBusLines();
        IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object,object> generate);
        void AddBusLine(DO.BusLine BusLine);
        void UpdateBusLine(DO.BusLine newLine);
        //void UpdateBusLine(string id, Action<DO.BusLine> update); //method that knows to updt specific fields in Student
        void DeleteBusLine(string id); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region BusStationLine
        IEnumerable<DO.BusStationLine> GetLineStationsListOfALine(string lineId);
        IEnumerable<DO.BusStationLine> GetAllBusStationLines(string BusStationLineNum);
        IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate);
        void AddBusStationLine(DO.BusStationLine busStationLine);
        void UpdateBusStationLine(DO.BusStationLine StationeNum);
        DO.BusStationLine GetBusStationLine(string Id);
        void DeleteBusStationLine( string StationeNum);
        void DeleteBusStationLineFromAllStations(string StationID);
        IEnumerable<object> GetBusStationsLineListWithSelectedFields(Func<DO.BusStationLine, object> generate);

        #endregion

        #region Station
        IEnumerable<DO.Station> GetAllStations();
        IEnumerable<DO.Station> GetAllStations(Predicate<DO.Station> predicate);
        IEnumerable<object> GetAllStationListWithSelectedFields(Func<DO.Station, object> generate);
        DO.Station GetStation(string StationCode);
        void AddStation(DO.Station station);
        void UpdateStation(DO.Station station);
        void DeleteStation(string codeStation);
        #endregion
      
        #region User
        IEnumerable<DO.User> GetAllUser();
        IEnumerable<DO.User> GetAllUser(Predicate<DO.User> predicate);
        DO.User GetUser(string Name,string pass);
        void AddUser(DO.User user);
        IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate);
        void UpdateUser(DO.User User);
        void DeleteUser(string Name,string password);
        #endregion

        //#region UserDrive
        //IEnumerable<DO.UserDrive> GetAllUserDrive();
        //IEnumerable<DO.UserDrive> GetAllUserDrive(Predicate<DO.UserDrive> predicate);
        //DO.UserDrive GetUserDrive(string Num);
        //void AddUserDrive(DO.UserDrive UserDrive);
        //void UpdateUserDrive(DO.UserDrive UserDrive);
        //void UpdateUserDrive(string Num, Action<DO.UserDrive> update); //method that knows to updt specific fields in Person
        //void DeleteUserDrive(string Num);
        //#endregion

        #region drivingbus
        IEnumerable<DO.DrivingBus> GetAllDrivingsBusLists();
        DO.DrivingBus GetDrivingBus(string Num);
        IEnumerable<DO.DrivingBus> GetDrivingsListList(Predicate<DO.DrivingBus> predicate);
        IEnumerable<object> GetDrivingsListListWithSelectedFields(Func<DO.DrivingBus, object> generate);
        void AddDrivingsList(DO.DrivingBus OutGoingLine);
        void UpdateDrivingBus(DO.DrivingBus OutGoingLine);
        void DeleteDrivingBus(string Num); // removes only OutGoingLine, does not remove the appropriate Bus...
        #endregion

        #region Accident
        DO.Accident GetAccident(int Accidentnum);
        IEnumerable<DO.Accident> GetAllAccidentsList(Predicate<DO.Accident> predicate);
        IEnumerable<DO.Accident> GetAllAccidents();
        IEnumerable<object> GetAccidentListWithSelectedFields(Func<DO.Accident, object> generate);
        void AddAccident(DO.Accident Accident);
        void UpdateAccident(DO.Accident Accident);
        void DeleteAccident(int Accidentnum); // removes only Accident, does not remove the appropriate Person...
        #endregion*/

        #region FollowingStation
        IEnumerable<DO.FollowingStations> GetAllFollowingStationss();
        IEnumerable<object> GetAllFollowingStationsListWithSelectedFields(Func<DO.FollowingStations, object> generate);
        //IEnumerable<DO.Bus> GetBusIDList();
      //  IEnumerable<object> GetAllFollowingStationListWithSelectedFields(Func<DO.FollowingStations, object> generate);
        DO.FollowingStations GetFollowingStation(string code);
        void AddFollowingStations(DO.FollowingStations FollowingStations);
        void UpdateFollowingStations(DO.FollowingStations FollowingStations);
        //void UpdateBus(string Num, Action<DO.Bus> update); //method that knows to updt specific fields in Person
        void DeleteFollowingStation(DO.FollowingStations followingstation);
        void DeleteFollowingStation(string code);
        #endregion
    }
}