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
        BO.Bus GetBus(int id);
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
        IEnumerable<BO.Station> GetStationBumList();
        BO.Station GetStation(string StationNum);
        void AddBus(BO.Station station);
        void UpdateBusPersonalDetails(BO.Station station);
        void DeleteStation(string StationNum);
      
        #endregion

        /*
        #region BusLine
        BO.BusLine GetBusLine(int Num);
        IEnumerable<BO.BusLine> GetBusLines();
        IEnumerable<BO.Bus> GetBusLineIDNameList();

        IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate);

        void UpdateBusLineDetails(BO.BusLine busLine);

        void DeleteBusLine(int Num);

        #endregion

        #region BusStationLine
        IEnumerable<BO.BusStationLine> GetAllBusStationLines();
        #endregion
        //מפה לטפל
        #region BusInCourse
        void AddBusInCourse(int perID, int courseID, float grade = 0);
        void UpdateBusGradeInCourse(int perID, int courseID, float grade);
        void DeleteBusInCourse(int perID, int courseID);

        #endregion       

        #region BusCourse
        IEnumerable<BO.BusCourse> GetAllCoursesPerBus(int id);
        #endregion
    */
    }
}
