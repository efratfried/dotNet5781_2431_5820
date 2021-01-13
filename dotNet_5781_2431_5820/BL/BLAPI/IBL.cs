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
        IEnumerable<BO.ListBuss> GetAllBuss();
        IEnumerable<BO.ListBuss> GetBusIDNameList();

        IEnumerable<BO.ListBuss> GetBussBy(Predicate<BO.Bus> predicate);

        void UpdateBusPersonalDetails(BO.Bus Bus);

        void DeleteBus(int id);
        /*
        public int LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Bus_Status Status { get; set; }
        public Firm MyFirm { get; set; }
        public IEnumerable<DateTime> AccidentsDuco { get; set; }
        public IEnumerable<Treat> TreatsDuco { get; set; }
        public IEnumerable<DrivingBus> drivingBusesDuco { get; set; }
        */

        #endregion
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

    }
}
