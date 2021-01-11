using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLAPI;
//using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal

    {
        //Implement IDL methods, CRUD
        #region Bus

        public DO.Bus GetBus(int LicenseNum)
        {
            DO.Bus bus = DataSource.BusList.Find(B => B.LicenseNum == LicenseNum);

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadBusNumException(LicenseNum, $"no Bus has License Num: {LicenseNum}");
        }
        public IEnumerable<DO.Bus> GetAllBusses()
        {//returns all members in list
            return from Bus in DataSource.BusList
                   select Bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        public void AddBus(DO.Bus bus)
        { //need a check if actually it is ==bus.---- or only ==licensnum
            if (DataSource.BusList.FirstOrDefault(B => B.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "Duplicate bus LicenseNum");
            DataSource.BusList.Add(bus.Clone());
        }

        public void DeleteBus(int Num)
        {
            DO.Bus bus = DataSource.BusList.Find(p => p.LicenseNum == Num);

            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
            }
            else
                throw new DO.BadLicenseNumException(Num, $"bad person id: {Num}");
        }

        public void UpdateBus(DO.Bus Bus)
        {
            DO.Bus bus = DataSource.BusList.Find(b => b.LicenseNum == Bus.LicenseNum);

            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
                DataSource.BusList.Add(Bus.Clone());
            }
            else
                throw new DO.BadLicenseNumException(Bus.LicenseNum , $"bad person id: {Bus.LicenseNum}");
        }

        public void UpdateBus(int Num, Action<DO.Bus> update) //method that knows to updt specific fields in Person
        {
            throw new NotImplementedException();
        }
        #endregion Bus 

        #region BusLine
        public DO.BusLine GetBusLine(int Num)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == Num);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadLicenseNumException(Num, $"bad line id: {Num}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            if (DataSource.BusLineList.FirstOrDefault(s => s.ID == BusLine.ID) != null)
                throw new DO.BadLicenseNumException(BusLine.ID, "Duplicate line ID");
            if (DataSource.BusLineList.FirstOrDefault(p => p.ID == BusLine.ID) == null)
                throw new DO.BadBusException(BusLine.ID, BusLine.BusNum, "Missing line num");
            DataSource.BusLineList.Add(BusLine.Clone());
        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            return from busline in DataSource.BusLineList
                   select busline.Clone();
        }
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from busline in DataSource.BusLineList
                   select generate(busline.ID, GetBusLine(busline.ID).BusNum);
        }

        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from BusLine in DataSource.BusLineList
                   select generate(BusLine);
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == BusLine.ID);
            if (busl != null)
            {
                //  DataSource.BusLineList.Remove(busl);
                DataSource.BusLineList.Add(BusLine.Clone());
            }
            else
                throw new DO.BadBusException(BusLine.ID, BusLine.BusNum, $"bad Line id: {BusLine.ID}");
        }

        public void UpdateBusLine(int Num, Action<DO.BusLine> update) //method that knows to updt specific fields in BusLine
        {
            throw new NotImplementedException();
        }

        void DeleteBusLine(int Num)// removes only BusLine, does not remove the appropriate Bus...
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == Num);

            if (busl != null)
            {
                DataSource.BusLineList.Remove(busl);
            }
            else
                throw new DO.BadLicenseNumException(Num, $"bad Line id: {Num}");
        }
        #endregion BusLine
        //finito
        #region 

        public IEnumerable<DO.StudentInCourse> GetStudentsInCourseList(Predicate<DO.StudentInCourse> predicate)
        {
            //option A - not good!!!
            //produces final list instead of deferred query and does not allow proper cloning:
            // return DataSource.listStudInCourses.FindAll(predicate);

            // option B - ok!!
            //Returns deferred query + clone:
            //return DataSource.listStudInCourses.Where(sic => predicate(sic)).Select(sic => sic.Clone());

            // option c - ok!!
            //Returns deferred query + clone:
            return from sic in DataSource.ListStudInCourses
                   where predicate(sic)
                   select sic.Clone();
        }
        public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        {
            if (DataSource.ListStudInCourses.FirstOrDefault(cis => (cis.PersonId == perID && cis.CourseId == courseID)) != null)
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is already registered to course ID");
            DO.StudentInCourse sic = new DO.StudentInCourse() { PersonId = perID, CourseId = courseID, Grade = grade };
            DataSource.ListStudInCourses.Add(sic);
        }
        public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        {
            DO.StudentInCourse sic = DataSource.ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");
        }

        public void DeleteStudentInCourse(int perID, int courseID)
        {
            DO.StudentInCourse sic = DataSource.ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

            if (sic != null)
            {
                DataSource.ListStudInCourses.Remove(sic);
            }
            else
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");
        }
        public void DeleteStudentFromAllCourses(int perID)
        {
            DataSource.ListStudInCourses.RemoveAll(p => p.PersonId == perID);
        }

        #endregion StudentInCourse

        #region BusStationLine

        public DO.BusStationLine GetBusStationLine(int Num)
        {
            return DataSource.StationLists.Find(c => c.ID == Num).Clone();
        }

        public IEnumerable<DO.BusStationLine> GetAllBusStationLine()
        {
            return from course in DataSource.StationLists
                   select course.Clone();
        }
        public void AddBusStationLine(DO.BusStationLine BusStationLine)
        {

        }
        public void UpdateBusStationLine(DO.BusStationLine BusStationLine)
        {

        }
        public void UpdateBusStationLinee(int Num, Action<DO.BusStationLine> update) //method that knows to updt specific fields in BusStationLine
        {

        }
        public void DeleteBusStationLine(int Num); // removes only BusStationLine, does not remove the appropriate Bus...
        {

        }

    #endregion BusStationLine
    #region outgoingbus
    public IEnumerable<DO.Station> GetStationListWithSelectedFields(Predicate<DO.Station> predicate)
    {
        //Returns deferred query + clone:
        return from sic in DataSource.StationLists
               where predicate(sic)
               select sic.Clone();
    }
    #endregion
    }
}
