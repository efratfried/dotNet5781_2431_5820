using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
//using BO;

namespace BLAPI
{
    public interface BLI
    {
        IDL dl = DLFactory.GetDL();

        #region BusLine
        BO.BusLine studentDoBoAdapter(DO.BusLine BusLineDO)
        {
            BO.BusLine BusLineBO = new BO.BusLine();
            DO.Bus BusDO;
            int id = BusLineDO.ID;
            try
            {
                BusDO = dl.GetBus(id);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadBusLineIdException("BusLine ID is illegal", ex);
            }
            BusDO.CopyPropertiesTo(BusLineBO);
            //studentBO.ID = personDO.ID;
            //studentBO.BirthDate = personDO.BirthDate;
            //studentBO.City = personDO.City;
            //studentBO.Name = personDO.Name;
            //studentBO.HouseNumber = personDO.HouseNumber;
            //studentBO.Street = personDO.Street;
            //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            BusLineDO.CopyPropertiesTo(BusLineBO);
            //studentBO.StartYear = studentDO.StartYear;
            //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
            //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

            BusLineBO.StationLists = from sic in dl.GetBusLineInBusStationLineList(sic => sic.BusId == id)
                                      let course = dl.GetBusStationLine(sic.BusStationLineId)
                                      select course.CopyToStudentCourse(sic);
            //new BO.StudentCourse()
            //{
            //    ID = course.ID,
            //    Number = course.Number,
            //    Name = course.Name,
            //    Year = course.Year,
            //    Semester = (BO.Semester)(int)course.Semester,
            //    Grade = sic.Grade
            //};

            return BusLineBO;
        }

        public BO.BusLine GetBusLine(int num)
        {
            DO.BusLine BusLineDO;
            try
            {
                BusLineDO = dl.GetBusLine(num);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadStudentIdException("Bus id does not exist or it is not a BusLine", ex);
            }
            return studentDoBoAdapter(BusLineDO);
        }

        public IEnumerable<BO.BusLine> GetAllBusLines()
        {
            //return from item in dl.GetStudentListWithSelectedFields( (stud) => { return GetStudent(stud.ID); } )
            //       let student = item as BO.Student
            //       orderby student.ID
            //       select student;
            return from studentDO in dl.GetAllBusLines()
                   orderby studentDO.ID
                   select studentDoBoAdapter(studentDO);
        }
        public IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ListedPerson> GetStudentIDNameList()
        {
            return from item in dl.GetBusLineListWithSelectedFields((BusLineDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.ListedPerson() { ID = BusLineDO.ID, Name = dl.GetBus(BusLineDO.ID).Name };
            })
                   let studentBO = item as BO.ListedPerson
                   //orderby student.ID
                   select studentBO;
        }

        public void UpdateStudentPersonalDetails(BO.BusLine busline)
        {
            //Update DO.Person            
            DO.Bus personDO = new DO.Bus();
            BusLine.CopyPropertiesTo(personDO);
            try
            {
                dl.UpdateBus(personDO);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadStudentIdException("busline id is illegal", ex);
            }

            //Update DO.Student            
            DO.BusLine studentDO = new DO.BusLine();
            BusLine.CopyPropertiesTo(studentDO);
            try
            {
                dl.UpdateBusLine(studentDO);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadStudentIdException("busline ID is illegal", ex);
            }

        }

        public void DeleteBusLine(int id)
        {
            try
            {
                dl.DeleteBus(id);
                dl.DeleteBusLine(id);
                dl.DeleteStudentFromAllCourses(id);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("busline ID and busstationline ID is Not exist", ex);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("bus id does not exist or it is not a busline", ex);
            }
        }

        #endregion

        #region StudentIn Course
        public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        {
            try
            {
                dl.AddStudentInCourse(perID, courseID, grade);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("busline ID and buslinestation ID is Not exist", ex);
            }
        }

        public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        {
            try
            {
                dl.UpdateStudentGradeInCourse(perID, courseID, grade);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("busline ID and buslinestation ID is Not exist", ex);
            }
        }

        public void DeleteStudentInCourse(int perID, int courseID)
        {
            try
            {
                dl.DeleteStudentInCourse(perID, courseID);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("busline ID and buslinestation ID is Not exist", ex);
            }
        }
        #endregion

        #region BusStationLine

        BO.BusStationLine courseDoBoAdapter(DO.BusStationLine BusStationLineDO)
        {
            BO.BusStationLine BusStationLineBO = new BO.BusStationLine();
            int id = BusStationLineDO.ID;
            BusStationLineDO.CopyPropertiesTo(BusStationLineBO);

            BusStationLineBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.BusStationLineId == id)
                                 let course = dl.GetBusStationLine(lic.BusStationLineId)
                                 select (BO.CourseLecturer)course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
            return BusStationLineBO;
        }
        public IEnumerable<BO.BusStationLine> GetAllBusStationLines()
        {
            return from crsDO in dl.GetAllBusStationLines()
                   select BusStationLineDoBoAdapter(crsDO);
        }

        public IEnumerable<BO.StudentCourse> GetAllCoursesPerStudent(int id)
        {
            return from sic in dl.GetStudentsInCourseList(sic => sic.BusId == id)
                   let BusStationLine = dl.GetBusStationLine(sic.BusStationLineId)
                   select BusStationLine.CopyToStudentCourse(sic);
        }

        #endregion

    }
}
