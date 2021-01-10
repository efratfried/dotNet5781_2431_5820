using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;

//using BO;

namespace BL
{
    /*
    class BLImp : IBL //internal
    {
        
        IDL dl = DLFactory.GetDL();

        #region BusLine
        BO.BusLine studentDoBoAdapter(DO.BusLine studentDO)
        {
            BO.BusLine studentBO = new BO.BusLine();
            DO.Bus personDO;
            int id = studentDO.ID;
            try
            {
                personDO = dl.GetBus(id);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadBusLineIdException("Student ID is illegal", ex);
            }
            personDO.CopyPropertiesTo(BusLineBO);
            //studentBO.ID = personDO.ID;
            //studentBO.BirthDate = personDO.BirthDate;
            //studentBO.City = personDO.City;
            //studentBO.Name = personDO.Name;
            //studentBO.HouseNumber = personDO.HouseNumber;
            //studentBO.Street = personDO.Street;
            //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            studentDO.CopyPropertiesTo(BusLineBO);
            //studentBO.StartYear = studentDO.StartYear;
            //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
            //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

            studentBO.ListOfCourses = from sic in dl.GetStudentsInCourseList(sic => sic.BusId == id)
                                      let course = dl.GetBusLine(sic.CourseId)
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

        public BO.BusLine GetBusLine(int Num)
        {
            DO.BusLine studentDO;
            try
            {
                studentDO = dl.GetBusLine(Num);
            }
            catch (DO.BadBusException ex)
            {
                throw new BO.BadBusLineIdException("BusLine num does not exist or it is not a BusLine", ex);
            }
            return studentDoBoAdapter(BusLineDO);
        }

        public IEnumerable<BO.BusLine> GetAllBusLines()
        {
            //return from item in dl.GetStudentListWithSelectedFields( (stud) => { return GetStudent(stud.ID); } )
            //       let student = item as BO.Student
            //       orderby student.ID
            //       select student;
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.ID
                   select BusLineDoBoAdapter(BusLineDO);
        }
        public IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Bus> GetBusLineIDNameList()
        {
            return from item in dl.GetBusLineListWithSelectedFields((studentDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Bus() { ID = BusLineDO.ID, Name = dl.GetBus(BusLineDO.ID).Name };
            })
                   let studentBO = item as BO.Bus
                   //orderby student.ID
                   select studentBO;
        }

        public void UpdateStudentPersonalDetails(BO.BusLine busline)
        {
            //Update DO.Person            
            DO.Person personDO = new DO.Person();
            student.CopyPropertiesTo(personDO);
            try
            {
                dl.UpdatePerson(personDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }

            //Update DO.Student            
            DO.Student studentDO = new DO.Student();
            student.CopyPropertiesTo(studentDO);
            try
            {
                dl.UpdateStudent(studentDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }

        }

        public void DeleteBusLine(int num)
        {
            try
            {
                dl.DeletePerson(id);
                dl.DeleteStudent(id);
                dl.DeleteStudentFromAllCourses(id);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
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
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
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
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
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
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }
        }
        #endregion

        #region Course

        BO.BusStationLine courseDoBoAdapter(DO.BusStationLine courseDO)
        {
            BO.Course courseBO = new BO.Course();
            int id = courseDO.ID;
            courseDO.CopyPropertiesTo(courseBO);

            courseBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.CourseId == id)
                                 let course = dl.GetCourse(lic.CourseId)
                                 select (BO.CourseLecturer)course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
            return courseBO;
        }
        public IEnumerable<BO.BusStationLine> GetAllCourses()
        {
            return from crsDO in dl.GetAllCourses()
                   select courseDoBoAdapter(crsDO);
        }

        public IEnumerable<BO.BusStationLine> GetAllCoursesPerStudent(int id)
        {
            return from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
                   let course = dl.GetCourse(sic.CourseId)
                   select course.CopyToStudentCourse(sic);
        }

        #endregion
        
    }
        */
}
