using System;
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
        void DeleteBus(int id);
        void AddBus(BO.Bus bus);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        IEnumerable<BO.Station> GetStation();
        BO.Station GetStation(string BusStationLineNum);
        void AddBus(BO.BusStationLine bus_station_line);
        void UpdateBusPersonalDetails(BO.BusStationLine bus_station_line);
        void DeleteStation(BO.BusStationLine bus_station_line);

        #endregion

        #region BusStationLine
        IEnumerable<BO.BusStationLine> GetAllBusStationLines();
        IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate);
        IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum);
        BO.Station GetBusStationLine(string StationNum);
        void AddBusStationLine(BO.BusStationLine station);
        void UpdateBusPersonalDetails(BO.Station station);
        void DeleteStation(string StationNum);

        #endregion
        
        #region BusLine
        BO.BusLine GetBusLine(string Num);
        IEnumerable<BO.BusLine> GetBusLines();
        IEnumerable<BO.BusLine> GetBusLineIDList();
        IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate);
        void UpdateBusLineDetails(BO.BusLine busLine);
        void DeleteBusLine(int Num);
        void AddBusLine(BO.BusLine busLine);
        #endregion

        #region user
        BO.Bus GetUser(int id);
        IEnumerable<BO.User> GetAllUsers();
        IEnumerable<BO.User> GetUserIDList();
        IEnumerable<BO.User> GetUserBy(Predicate<BO.User> predicate);
        void UpdateUserPersonalDetails(BO.User user);
        void DeleteUser(int id);
        void AddUser(BO.User bususer);
        #endregion
    }
}
