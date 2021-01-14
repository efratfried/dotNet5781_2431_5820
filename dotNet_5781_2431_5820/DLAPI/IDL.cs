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
        IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate);
        DO.Bus GetBus(int Num);
        void AddBus(DO.Bus Bus);
        void UpdateBus(DO.Bus Bus);
        void UpdateBus(int Num, Action<DO.Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(int Num);
        #endregion

        #region BusLine
        DO.BusLine GetBusLine(int id);
        IEnumerable<DO.BusLine> GetAllBusLines();
        IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate);
        void AddBusLine(DO.BusLine BusLine);
        void UpdateBusLine(DO.BusLine BusLine);
        void UpdateBusLine(int id, Action<DO.BusLine> update); //method that knows to updt specific fields in Student
        void DeleteBusLine(int id); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region BusStationLine
        IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate);
        void AddBusStationLine(int StationID, int BusStationeNum);
        void UpdateBusStationLine(int StationeNum);
        void DeleteBusStationLine( int StationeNum);
        void DeleteBusStationLineFromAllStations(int StationID);
        void GetBusStationLineListWithSelectedFields(Func<DO.BusStationLine, object> generate);

        #endregion

        #region Station
        DO.Station GetStation(int id);
        IEnumerable<DO.Station> GetAllStations();
        #endregion

        #region DrivingBus
        IEnumerable<DO.OutGoingLine> GetOutGoingLineList(Predicate<DO.OutGoingLine> predicate);

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

        #region OutGoingLine
        DO.OutGoingLine GetOutGoingLine(int Num);
        IEnumerable<DO.OutGoingLine> GetAllOutGoingLines();
        IEnumerable<object> GetOutGoingLineListWithSelectedFields(Func<DO.OutGoingLine, object> generate);
        void AddOutGoingLine(DO.OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(DO.OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(int Num, Action<DO.OutGoingLine> update); //method that knows to updt specific fields in OutGoingLine
        void DeleteOutGoingLine(int Num); // removes only OutGoingLine, does not remove the appropriate Bus...
        #endregion
    }
}