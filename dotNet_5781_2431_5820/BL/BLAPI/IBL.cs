using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {
        #region Bus
        BO.BusLine GetStudent(int id);
        IEnumerable<BO.BusLine> GetAllStudents();
        IEnumerable<BO.BusStationLine> GetStudentIDNameList();

        IEnumerable<BO.BusLine> GetStudentsBy(Predicate<BO.BusLine> predicate);

        void UpdateStudentPersonalDetails(BO.BusLine BusLine);

        void DeleteStudent(int id);

        #endregion

        #region StudentInCourse
        void AddStudentInCourse(int perID, int courseID, float grade = 0);
        void UpdateStudentGradeInCourse(int perID, int courseID, float grade);
        void DeleteStudentInCourse(int perID, int courseID);

        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllCourses();
        #endregion

        #region StudentCourse
        IEnumerable<BO.Station> GetAllCoursesPerStudent(int id);
        #endregion
    }
}
