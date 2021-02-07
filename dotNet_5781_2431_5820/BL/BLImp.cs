using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
//using BO;

//      :הערות
//איך נחשב את מספר התחנה שעולה כול פעם
//איך נציג תחנות עוקבות
namespace BL
{
    class BLImp : HelpFunctions, IBL //internal
    {
        IDL dl = DLFactory.GetDL();

        #region Bus
        BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            string LicenseNum = BusDO.LicenseNum;
            try
            {
                BusDO = dl.GetBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusIdException("Bus LicenseNum is illegal", ex);
            }

            BusDO.CopyPropertiesTo(BusBO);

            BusBO.AccidentsDuco = from sic in dl.GetAllAccidentsList(sic => sic.LicenseNum == LicenseNum)
                                  let Accident = dl.GetAccident(sic.AccidentNum)
                                  select (DO.Accident)Accident.CopyPropertiesToNew(typeof(DO.Accident));


            return BusBO;
        }
        public BO.Bus GetBus(int LicenseNum)
        {
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusIdException("Buss' LicenseNum does not exist or it is not a Bus", ex);
            }
            return BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetAllBuss()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from BusDO in dl.GetAllBusses()
                   orderby BusDO.LicenseNum
                   select BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.Bus> GetBusLicenseNumList()
        {
            return from item in dl.GetAllBusListWithSelectedFields((BusDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Bus() { LicenseNum = BusDO.LicenseNum };
            })
                   let BusBO = item as BO.Bus
                   //orderby Bus.LicenseNum
                   select BusBO;
        }
        public void UpdateBusPersonalDetails(BO.Bus Bus)
        {
            //Update DO.Bus            
            DO.Bus BusDO = new DO.Bus();
            Bus.CopyPropertiesTo(BusDO);
            try
            {
                dl.UpdateBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusIdException("Bus's LicenseNum is illegal", ex);
            }
        }
        public void DeleteBus(int LicenseNum)
        {
            try
            {
                dl.DeleteBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusIdException("Bus LicenseNum does not exist or it is not a Bus", ex);
            }
        }
        public void AddBus(BO.Bus bus)
        {

            DO.Bus BusDO = new DO.Bus();
            if (bus.LicenseNum.Length != 7 && bus.LicenseNum.Length != 8)
            {
                throw new Exception("invalid license num lengh")...
            }

            if ((bus.LicenseDate.Year < 2018 && bus.LicenseNum.Length != 7) || (bus.LicenseDate.Year >= 2018 && bus.LicenseDate < DateTime.Now && bus.LicenseNum.Length != 8))
            {//check the validation of the license num accroding to it date
                throw new Exception("invalid license's date")..
            }

            bus.KM = 0;
            bus.foul = 0;
            bus.Status = Status.UnAvailable;

            BusDO.CopyPropertiesToNew(typeof(BO.Bus));
            try
            {
                dl.AddBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBustIdException("Bus ID is illegal", ex);
            }
        }

        #endregion Bus

        #region Station
        BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();
            string CodeStation = StationDO.CodeStation;
            try
            {
                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadCodeStationException ex)
            {
                throw new BO.BadStationIdException("Bus LicenseNum is illegal", ex);
            }

            StationDO.CopyPropertiesTo(StationBO);

            return StationBO;
        }
        public BO.Station GetStation(string CodeStation)
        {
            DO.Station StationDO;
            try
            {
                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadStationIdException("Buss' LicenseNum does not exist or it is not a Bus", ex);
            }
            return StationDoBoAdapter(StationDO);
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from StationDO in dl.GetAllStations()
                   orderby StationDO.CodeStation
                   select StationDoBoAdapter(StationDO);
        }
        public IEnumerable<BO.Station> GetStationBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.Station> GetStationLicenseNumList()
        {
            return from item in dl.GetAllStationListWithSelectedFields((StationDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Station() { CodeStation = StationDO.CodeStation };
            })
                   let StationBO = item as BO.Station
                   //orderby Bus.LicenseNum
                   select StationBO;
        }
        public void UpdateStationPersonalDetails(BO.Station station)
        {
            //Update DO.Bus            
            DO.Station StationDO = new DO.Station();
            Station.CopyPropertiesTo(StationDO);
            try
            {
                dl.UpdateStation(StationDO);
            }
            catch (DO.BadStationNumException ex)
            {
                throw new BO.BadStationIdException("Bus's LicenseNum is illegal", ex);
            }
        }
        public void DeleteStation(string LicenseNum)
        {
            try
            {
                dl.DeleteStation(LicenseNum);
            }
            catch (DO.BadCodeStationException ex)
            {
                throw new BO.BadStationIdException("Bus LicenseNum does not exist or it is not a Bus", ex);
            }
        }
        public void AddStation(BO.Station station)
        {
            
            DO.Station StationDO = new DO.Station();
            static Random rand = new Random();
            double rochav = (rand.NextDouble() * rand.NextDouble()) % 2.4 + 31;
            double orech = (rand.NextDouble() * rand.NextDouble()) % 1.4 + 34.3;

            if (station.Latitude!=rochav || station.longitude!=orech)
            {
                throw new Exception("the cordinates are wrong")...
            }
            if (station.CodeStation.Length > 6 || station.CodeStation.Length < 0)
            {
                throw new Exception("incorrect station's code")...
            }
            StationDO.CopyPropertiesToNew(typeof(BO.Station));
            try
            {
                dl.AddStation(StationDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BaStationtIdException("station num is illegal", ex);
            }
        }

        #endregion

        #region BusStationLine
        /*
                IEnumerable<BO.BusStationLine> GetAllBusStationLines();
               IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate);
               IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum);
               BO.Station GetBusStationLine(string StationNum);
               void AddBusStationLine(BO.BusStationLine station);
               void UpdateBusPersonalDetails(BO.Station station);
               void DeleteStation(string StationNum);
         */
        BO.BusStationLine BusStationLineDoBoAdapter(DO.BusStationLine BusStationLineDO)
        {
            BO.BusStationLine BusStationLineBO = new BO.BusStationLine();
            string CodeStation = BusStationLineDO.BusStationNum;
            try
            {
                BusStationLineDO = dl.GetBusStationLine(CodeStation);
            }
            catch (DO.BadCodeStationException ex)
            {
                throw new BO.BadStationIdException("Bus LicenseNum is illegal", ex);
            }

            BusStationLineDO.CopyPropertiesTo(BusStationLineBO);

            return BusStationLineBO;
        }
        public BO.BusStationLine GetBusStationLine(string CodeStation)
        {
            DO.BusStationLine BusStationLineDO;
            try
            {
                BusStationLineDO = dl.GetBusStationLine(CodeStation);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadStationIdException("Buss' LicenseNum does not exist or it is not a Bus", ex);
            }
            return BusStationLineDoBoAdapter(BusStationLineDO);
        }
        public IEnumerable<BO.BusStationLine> GetAllBusStationLines()
        {
            return from BusStationLineDO in dl.GetBusStationLineList()
                   orderby BusStationLineDO.CodeStation
                   select BusStationLineDoBoAdapter(BusStationLineDO);
        }
        public IEnumerable<BO.BusStationLine> GetBusStationLineBy(Predicate<BO.BusStationLine> predicate)
        {//need fill field 
            throw new NotImplementedException();
        }
        public IEnumerable<BO.BusStationLine> GetBusStationLineList()
        {
            return from item in dl.GetBusStationLineListWithSelectedFields((BusStationLineDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.BusStationLine() { ID = BusStationLineDO.ID };
            })
                   let BusStationLineBo = item as BO.BusStationLine
                   //orderby Bus.LicenseNum
                   select BusStationLineBo;
        }
        public void AddBusStationLine(string bus_station_line)
        {
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            BusStationLineDO.CopyPropertiesToNew(typeof(BO.Station));
            try
            {
                dl.AddBusStationLine(BusStationLineDO.ID, bus_station_line);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBustIdException("Bus ID is illegal", ex);
            }
        }
        public void UpdateStationPersonalDetails(BO.BusStationLine bus_station_num)
        {
            //Update DO.Bus            
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            BusStationLine.CopyPropertiesTo(BusStationLineDO);
            try
            {
                dl.UpdateStation(BusStationLineDO);
            }
            catch (DO.BadStationNumException ex)
            {
                throw new BO.BadStationIdException("Bus's LicenseNum is illegal", ex);
            }
        }
        public void DeleteStation(BO.BusStationLine bus_station_line)
        {
            try
            {
                dl.DeleteStation(bus_station_line);
            }
            catch (DO.BadCodeStationException ex)
            {
                throw new BO.BadStationIdException("Bus LicenseNum does not exist or it is not a Bus", ex);
            }
        }

        /* IEnumerable<BO.Station> GetAllStations();
         IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
         IEnumerable<BO.Station> GetStationList();
         BO.Station GetStation(string BusStationLineNum);
         void AddBus(BO.BusStationLine bus_station_line);
         void UpdateBusPersonalDetails(BO.BusStationLine bus_station_line);
         void DeleteStation(BO.BusStationLine bus_station_line);*/

        #endregion
    }
}
/*
#region BusIn Course
public void AddBusInCourse(int perID, int courseID, float grade = 0)
{
    try
    {
        dl.AddBusInCourse(perID, courseID, grade);
    }
    catch (DO.BadBusIdCourseIDException ex)
    {
        throw new BO.BadBusIdCourseIDException("Bus ID and Course ID is Not exist", ex);
    }
}

public void UpdateBusGradeInCourse(int perID, int courseID, float grade)
{
    try
    {
        dl.UpdateBusGradeInCourse(perID, courseID, grade);
    }
    catch (DO.BadBusIdCourseIDException ex)
    {
        throw new BO.BadBusIdCourseIDException("Bus ID and Course ID is Not exist", ex);
    }
}

public void DeleteBusInCourse(int perID, int courseID)
{
    try
    {
        dl.DeleteBusInCourse(perID, courseID);
    }
    catch (DO.BadBusIdCourseIDException ex)
    {
        throw new BO.BadBusIdCourseIDException("Bus ID and Course ID is Not exist", ex);
    }
}
#endregion

#region Course

BO.Course courseDoBoAdapter(DO.Course courseDO)
{
    BO.Course courseBO = new BO.Course();
    int id = courseDO.ID;
    courseDO.CopyPropertiesTo(courseBO);

    courseBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.CourseId == id)
                         let course = dl.GetCourse(lic.CourseId)
                         select (BO.CourseLecturer)course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
    return courseBO;
}
public IEnumerable<BO.Course> GetAllCourses()
{
    return from crsDO in dl.GetAllCourses()
           select courseDoBoAdapter(crsDO);
}

public IEnumerable<BO.BusCourse> GetAllCoursesPerBus(int id)
{
    return from sic in dl.GetBussInCourseList(sic => sic.BusId == id)
           let course = dl.GetCourse(sic.CourseId)
           select course.CopyToBusCourse(sic);
}

#endregion


}
}
*/