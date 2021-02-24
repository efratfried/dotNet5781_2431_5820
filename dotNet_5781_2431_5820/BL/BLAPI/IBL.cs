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
        void DeleteBusStationLine(string StationNum);

        #endregion
        
        #region BusLine
        BO.BusLine GetBusLine(int Num);
        IEnumerable<BO.BusLine> GetBusLines();
        IEnumerable<BO.BusLine> GetBusLineIDList();
        IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate);
        void UpdateBusLineDetails(BO.BusLine busLine);
        void DeleteBusLine(int Num);
        void AddBusLine(BO.BusLine busLine);
        #endregion

        #region user
        BO.Bus GetUser(string name,string password);
        IEnumerable<BO.User> GetAllUsers();
        IEnumerable<BO.User> GetUserIDList();
        IEnumerable<BO.User> GetUserBy(Predicate<BO.User> predicate);
        IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate);
        void UpdateUserPersonalDetails(BO.User user);
        void DeleteUser(string name,string password);
        void AddUser(BO.User bususer);
        #endregion

        #region outgoingline
        BO.Bus GetOutGoingBus(string id);
        IEnumerable<BO.Bus> GetAllOutGoingBuss();
        IEnumerable<BO.Bus> GetOutGoingBusIDList();
        IEnumerable<BO.Bus> GetOutGoingBussBy(Predicate<BO.Bus> predicate);
        void UpdateOutGoingBusPersonalDetails(BO.Bus bus);
        void DeleteOutGoingBus(string id);
        void AddOutGoingBus(BO.Bus bus);
        #endregion
    }
}
