﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using DO;

namespace DLAPI
{
    //basic on the "CRUD" rule - Create_Request_Update_Delete
    public interface IDL
    {
        #region Bus
        IEnumerable<DO.Bus> GetAllBusses();
        IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate);
        DO.Bus GetBus(string Num);
        void AddBus(DO.Bus Bus);
        void UpdateBus(DO.Bus Bus);
        void DeleteBus(string Num);
      
        #endregion

        #region BusLine
        DO.BusLine GetBusLine(int id);
        IEnumerable<DO.BusLine> GetAllBusLines();
        IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object,object> generate);
        int AddBusLine(DO.BusLine BusLine);
        void UpdateBusLine(DO.BusLine newLine);
        void DeleteBusLine(int id); // removes only Student, does not remove the appropriate Person...
        IEnumerable<DO.BusLine> GetLineStationsListThatMatchAStation(int code);
        #endregion

        #region BusStationLine
        IEnumerable<DO.BusStationLine> GetLineStationsListOfALine(string lineId);
        IEnumerable<DO.BusStationLine> GetAllBusStationLines(string BusStationLineNum);
        IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate);
        void AddBusStationLine(DO.BusStationLine busStationLine);
        void UpdateBusStationLine(DO.BusStationLine StationeNum);
        DO.BusStationLine GetBusStationLine(string Id);
        void DeleteBusStationLine(string id, string LineNum);
        void DeleteBusStationLineFromAllStations(string StationID);
        void DeleteBusStationLine(string id);
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
        DO.User GetUser(string Name,string pass);
        void AddUser(DO.User user);
        IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate);
        void UpdateUser(DO.User User);
        void DeleteUser(string Name,string password);
        #endregion
       
        #region drivingbus
        IEnumerable<DO.DrivingBus> GetAllDrivingsBusLists();
        DO.DrivingBus GetDrivingBus(string Num);
        IEnumerable<DO.DrivingBus> GetDrivingsListList(Predicate<DO.DrivingBus> predicate);
        IEnumerable<object> GetDrivingsListListWithSelectedFields(Func<DO.DrivingBus, object> generate);
        void AddDrivingsList(DO.DrivingBus OutGoingLine);
        void UpdateDrivingBus(DO.DrivingBus OutGoingLine);
        void DeleteDrivingBus(string Num); // removes only OutGoingLine, does not remove the appropriate Bus...
        #endregion

        #region FollowingStation
        IEnumerable<DO.FollowingStations> GetAllFollowingStationss();
        IEnumerable<object> GetAllFollowingStationsListWithSelectedFields(Func<DO.FollowingStations, object> generate);
        DO.FollowingStations GetFollowingStation(string code1,string code2);
        void AddFollowingStations(DO.FollowingStations FollowingStations);
        void UpdateFollowingStations(DO.FollowingStations FollowingStations);
        void DeleteFollowingStation(DO.FollowingStations followingstation);
        void DeleteFollowingStation(string code);
        #endregion

        #region outgoingline
        void AddLineExit(DO.OutGoingLine lineExit);
        void DeleteLineExit(int lineNumber, TimeSpan StartTime);
        void UpdatingLineExit(DO.OutGoingLine lineExit);
        DO.OutGoingLine ReturnLineExit(int lineNumber, TimeSpan StartTime);
        DO.OutGoingLine OneLineExitFromList(int numberLine, TimeSpan StartTime);
        IEnumerable<DO.OutGoingLine> LineExitList(int numberLine);
        #endregion
    }
}
