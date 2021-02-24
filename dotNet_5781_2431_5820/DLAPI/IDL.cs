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
        void UpdateBus(string Num, Action<DO.Bus> update); //method that knows to updt specific fields in Person
        void DeleteBus(string Num);
        #endregion

        #region BusLine
        DO.BusLine GetBusLine(string id);
        IEnumerable<DO.BusLine> GetAllBusLines();
        IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object,object> generate);
        void AddBusLine(DO.BusLine BusLine);
        void UpdateBusLine(DO.BusLine BusLine);
        void UpdateBusLine(string id, Action<DO.BusLine> update); //method that knows to updt specific fields in Student
        void DeleteBusLine(string id); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region BusStationLine
        IEnumerable<DO.BusStationLine> GetAllBusStationLines(string BusStationLineNum);
        IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate);
        void AddBusStationLine(string StationID, string BusStationeNum);
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
        DO.Bus GetStation(string StationCode);
        void AddStation(DO.Station station);
        void UpdateStation(DO.Station station);
        void UpdateStation(string StationCode, Action<DO.Station> update); //method that knows to updt specific fields in Person
        void DeleteStation(string NuStationCodem);
        #endregion

        #region DrivingBus
        IEnumerable<DO.OutGoingLine> GetOutGoingLineList(Predicate<DO.OutGoingLine> predicate);

        #endregion

        #region User
        IEnumerable<DO.User> GetAllUser();
        IEnumerable<DO.User> GetAllUser(Predicate<DO.User> predicate);
        DO.User GetUser(string Num,string pass);
        void AddUser(DO.User User);
        IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate);
        void UpdateUser(DO.User User);
        void UpdateUser(string Num, Action<DO.User> update); //method that knows to updt specific fields in Person
        void DeleteUser(string Num);
        #endregion

        #region UserDrive
        IEnumerable<DO.UserDrive> GetAllUserDrive();
        IEnumerable<DO.UserDrive> GetAllUserDrive(Predicate<DO.UserDrive> predicate);
        DO.UserDrive GetUserDrive(string Num);
        void AddUserDrive(DO.UserDrive UserDrive);
        void UpdateUserDrive(DO.UserDrive UserDrive);
        void UpdateUserDrive(string Num, Action<DO.UserDrive> update); //method that knows to updt specific fields in Person
        void DeleteUserDrive(string Num);
        #endregion

        #region OutGoingLine
        IEnumerable<DO.OutGoingLine> GetAllOutGoingLines();
        DO.OutGoingLine GetOutGoingLine(string Num);
        IEnumerable<DO.OutGoingLine> GetAOutGoingLineList(Predicate<DO.OutGoingLine> predicate);
        IEnumerable<object> GetOutGoingLineListWithSelectedFields(Func<DO.OutGoingLine, object> generate);
        void AddOutGoingLine(DO.OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(DO.OutGoingLine OutGoingLine);
        void DeleteOutGoingLine(string Num); // removes only OutGoingLine, does not remove the appropriate Bus...
        #endregion

        #region Accident
        DO.Accident GetAccident(int Accidentnum);
        IEnumerable<DO.Accident> GetAllAccidentsList(Predicate<DO.Accident> predicate);
        IEnumerable<DO.Accident> GetAllAccidents();
        IEnumerable<object> GetAccidentListWithSelectedFields(Func<DO.Accident, object> generate);
        void AddAccident(DO.Accident Accident);
        void UpdateAccident(DO.Accident Accident);
        void DeleteAccident(int Accidentnum); // removes only Accident, does not remove the appropriate Person...
        #endregion
    }
}