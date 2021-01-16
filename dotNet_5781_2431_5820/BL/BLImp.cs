﻿using System;
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
            int LicenseNum = BusDO.LicenseNum;
            try
            {
                BusDO = dl.GetBus(LicenseNum); 
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusIdException("Bus LicenseNum is illegal", ex);
            }

            BusDO.CopyPropertiesTo(BusBO);
            
            //BusBO.StartYear = BusDO.StartYear;
            //BusBO.Status = (BO.BusStatus)(int)BusDO.Status;
            //BusBO.Graduation = (BO.BusGraduate)(int)BusDO.Graduation;
            /*
             *       studentBO.ListOfCourses = from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
                                      let course = dl.GetCourse(sic.CourseId)
                                      select course.CopyToStudentCourse(sic);
             * */
            BusBO.AccidentsDuco = from sic in dl.GetAllAccidentsList(sic => sic.LicenseNum == LicenseNum)
                                      let Accident = dl.GetAccident(sic.AccidentNum)
                                      select Accident.CopyPropertiesTo(sic);

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
        public IEnumerable<BO.Bus> GetBusIDList()
        {
            return from item in dl.GetAllBusListWithSelectedFields((BusDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Bus() { LicenseNum = BusDO.LicenseNum};
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

        }

        #endregion Bus
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