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
       // IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate);
        void UpdateBusPersonalDetails(BO.Bus bus);
        void DeleteBus(string id);
        void AddBus(BO.Bus bus);
        BO.Foul_Status foul_Status(BO.Bus bus);
        BO.Status status(BO.Bus bus);

        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
      //  IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        BO.Station GetStation(string BusStationLineNum);
        void AddStation(BO.Station station);
        void UpdateStationPersonalDetails(BO.Station station);
        void DeleteStation(string code);
        TimeSpan GetLineTimingPerStation(BO.Station station, TimeSpan CurrentTime);
        IEnumerable<BO.Station> GetStationLicenseNumList();

        #endregion

        #region BusStationLine
        void DeleteStationFromLine(BO.BusLine busline, string code);
        IEnumerable<BO.BusStationLine> GetAllBusStationLines(int num);
       // IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate);
        IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum);
        BO.Station GetBusStationLine(string StationNum);
        void AddBusStationLine(BO.BusStationLine BusstationLine);
        void UpdateBusStationLinePersonalDetails(BO.BusStationLine BusstationLine);
        void DeleteBusStationLine(string num, int ID, int index);
       // IEnumerable<BO.BusStationLine> GetAllBusStationLinesPerLine(int lineId);

        #endregion

        #region BusLine
        BO.BusLine GetBusLine(int Num);
        IEnumerable<BO.BusLine> GetAllLinesByArea(BO.Area area);
        IEnumerable<BO.BusLine> GetBusLines();
        IEnumerable<BO.BusLine> GetBusLineIDList();
        //IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate);
        //IEnumerable<BO.BusLine> GetAllLinesPerStation(int code);
        void UpdateBusLinePersonalDetails(BO.BusLine busLine);
        void DeleteBusLine(int Num);
        void AddBusLine(BO.BusLine busLine);
        IEnumerable<BO.BusLine> GetAllLinesPerStation(int code);

        #endregion

        #region followingStation
        BO.FollowingStations GetFollowingStation(string code1,string code2);
        IEnumerable<BO.FollowingStations> GetAllFollowingStations();
        IEnumerable<BO.FollowingStations> GetFollowingStationSBy(Predicate<BO.FollowingStations> predicate);
        void UpdateFollowingStationPersonalDetails(BO.FollowingStations FollowingStation);
        void DeleteFollowingStation(string code); //delete station completely
        void DeleteFollowingStations(BO.FollowingStations followingStation);//delete station from line
        void AddFollowingStation(string code1, string code2);
        double DistancefromPriviouStation(string station1, string station2);
        TimeSpan DrivingTimeBetweenTwoStations(string station1, string station2);
        TimeSpan WalkingTimeBetweenTwoStations(string station1, string station2);
        #endregion


        #region user
        BO.User userBoDoAdapter(DO.User userDO);
        BO.User GetUser(string name, string password);
        void AddUser(BO.User user);
        //DO.User userBoDoAdapter(BO.User userBO);
        IEnumerable<BO.User> GetAllUsers();

        #endregion*/
        /*
        #region outgoingline
        BO.OutGoingLine GetOutGoingLine(string id);
        IEnumerable<BO.OutGoingLine> GetAllOutGoingLines();
        IEnumerable<BO.OutGoingLine> GetOutGoingLineIDList();
       // IEnumerable<BO.OutGoingLine> GetOutGoingLinesBy(Predicate<BO.OutGoingLine> predicate);
        void UpdateOutGoingLinePersonalDetails(BO.OutGoingLine outgoingline);
        void DeleteOutGoingLine(BO.OutGoingLine outgoingline);
        void AddOutGoingLine(BO.Bus outgoingline);
        #endregion*/
        /*
        #region Accident
        BO.Bus GetAccident(BO.Bus bus,int num);
        IEnumerable<BO.Bus> GetAccidentBy(Predicate<BO.Bus> predicate);
        void AddAccident(BO.Bus bus);
        #endregion*/
        IEnumerable<BO.OutGoingLine> GetAllfrequencies(int lineNum);
    }
}
