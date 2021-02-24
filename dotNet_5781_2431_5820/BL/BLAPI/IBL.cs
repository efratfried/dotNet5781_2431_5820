﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BO;
namespace BLAPI
{
    public interface IBL
    {
        #region bus
        BO.Bus GetBus(string id);
        IEnumerable<BO.Bus> GetAllBuss();
        IEnumerable<BO.Bus> GetBusIDList();
        IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate);
        void UpdateBusPersonalDetails(BO.Bus bus);
        void DeleteBus(string id);
        void AddBus(BO.Bus bus);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        BO.Station GetStation(string BusStationLineNum);
        void AddStation(BO.Station station);
        void UpdateStationPersonalDetails(BO.Station station);
        void DeleteStation(BO.Station station);

        #endregion

        #region BusStationLine
        IEnumerable<BO.BusStationLine> GetAllBusStationLines();
        IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate);
        IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum);
        BO.Station GetBusStationLine(string StationNum);
        void AddBusStationLine(BO.BusStationLine BusstationLine);
        void UpdateBusStationLinePersonalDetails(BO.BusStationLine BusstationLine);
        void DeleteBusStationLine(BO.BusStationLine bus_station_line);

        #endregion
        
        #region BusLine
        BO.BusLine GetBusLine(int Num);
        IEnumerable<BO.BusLine> GetBusLines();
        IEnumerable<BO.BusLine> GetBusLineIDList();
        IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate);
        void UpdateBusLinePersonalDetails(BO.BusLine busLine);
        void DeleteBusLine(int Num);
        void AddBusLine(BO.BusLine busLine);
        #endregion

        #region user
        BO.User GetUser(string name,string password);
        IEnumerable<BO.User> GetAllUsers();
        IEnumerable<BO.User> GetUserIDList();
        IEnumerable<BO.User> GetUserBy(Predicate<BO.User> predicate);
        IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate);
        void UpdateUserPersonalDetails(BO.User user);
        void DeleteUser(string name,string password);
        void AddUser(BO.User bususer);
        #endregion

        #region outgoingline
        BO.OutGoingLine GetOutGoingLine(string id);
        IEnumerable<BO.OutGoingLine> GetAllOutGoingLines();
        IEnumerable<BO.OutGoingLine> GetOutGoingLineIDList();
        IEnumerable<BO.OutGoingLine> GetOutGoingLinesBy(Predicate<BO.OutGoingLine> predicate);
        void UpdateOutGoingLinePersonalDetails(BO.OutGoingLine outgoingline);
        void DeleteOutGoingLine(BO.OutGoingLine outgoingline);
        void AddOutGoingLine(BO.Bus outgoingline);
        #endregion
    }
}
