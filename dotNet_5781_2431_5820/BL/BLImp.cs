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
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();

        #region Bus
        BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            DO.Person personDO;
            int id = BusDO.ID;
            try
            {
                personDO = dl.GetPerson(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadBusIdException("Bus ID is illegal", ex);
            }
            personDO.CopyPropertiesTo(BusBO);
            //BusBO.ID = personDO.ID;
            //BusBO.BirthDate = personDO.BirthDate;
            //BusBO.City = personDO.City;
            //BusBO.Name = personDO.Name;
            //BusBO.HouseNumber = personDO.HouseNumber;
            //BusBO.Street = personDO.Street;
            //BusBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            BusDO.CopyPropertiesTo(BusBO);
            //BusBO.StartYear = BusDO.StartYear;
            //BusBO.Status = (BO.BusStatus)(int)BusDO.Status;
            //BusBO.Graduation = (BO.BusGraduate)(int)BusDO.Graduation;

            BusBO.ListOfCourses = from sic in dl.GetBussInCourseList(sic => sic.PersonId == id)
                                      let course = dl.GetCourse(sic.CourseId)
                                      select course.CopyToBusCourse(sic);
            //new BO.BusCourse()
            //{
            //    ID = course.ID,
            //    Number = course.Number,
            //    Name = course.Name,
            //    Year = course.Year,
            //    Semester = (BO.Semester)(int)course.Semester,
            //    Grade = sic.Grade
            //};

            return BusBO;
        }

        public BO.Bus GetBus(int id)
        {
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadBusIdException("Person id does not exist or he is not a Bus", ex);
            }
            return BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetAllBuss()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from BusDO in dl.GetAllBuss()
                   orderby BusDO.ID
                   select BusDoBoAdapter(BusDO);
        }
        public IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ListedPerson> GetBusIDNameList()
        {
            return from item in dl.GetBusListWithSelectedFields((BusDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.ListedPerson() { ID = BusDO.ID, Name = dl.GetPerson(BusDO.ID).Name };
            })
                   let BusBO = item as BO.ListedPerson
                   //orderby Bus.ID
                   select BusBO;
        }

        public void UpdateBusPersonalDetails(BO.Bus Bus)
        {
            //Update DO.Person            
            DO.Person personDO = new DO.Person();
            Bus.CopyPropertiesTo(personDO);
            try
            {
                dl.UpdatePerson(personDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadBusIdException("Bus ID is illegal", ex);
            }

            //Update DO.Bus            
            DO.Bus BusDO = new DO.Bus();
            Bus.CopyPropertiesTo(BusDO);
            try
            {
                dl.UpdateBus(BusDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadBusIdException("Bus ID is illegal", ex);
            }

        }

        public void DeleteBus(int id)
        {
            try
            {
                dl.DeletePerson(id);
                dl.DeleteBus(id);
                dl.DeleteBusFromAllCourses(id);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadBusIdCourseIDException("Bus ID and Course ID is Not exist", ex);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadBusIdException("Person id does not exist or he is not a Bus", ex);
            }
        }

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
            catch (DO.BadPersonIdCourseIDException ex)
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
            catch (DO.BadPersonIdCourseIDException ex)
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
            catch (DO.BadPersonIdCourseIDException ex)
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
            return from sic in dl.GetBussInCourseList(sic => sic.PersonId == id)
                   let course = dl.GetCourse(sic.CourseId)
                   select course.CopyToBusCourse(sic);
        }

        #endregion


    }
}