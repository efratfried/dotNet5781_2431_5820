using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class DriverIdException : Exception
    {
        public int ID;
        public DriverIdException(int id) : base() => ID = id;
        public DriverIdException(int id, string message) :
            base(message) => ID = id;
        public DriverIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad person id: {ID}";
    }
    [Serializable]
    public class BadBusIdCourseIDException : Exception
    {
        public int personID;
        public int courseID;
        public BadBusIdCourseIDException(int perID, int crsID) : base() { personID = perID; courseID = crsID; }
        public BadBusIdCourseIDException(int perID, int crsID, string message) :
            base(message)
        { personID = perID; courseID = crsID; }
        public BadBusIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
            base(message, innerException)
        { personID = perID; courseID = crsID; }

        public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
    }
    [Serializable]
    public class BadStationIdCourseIDException : Exception
    {
        public string xmlFilePath;
        public BadStationIdCourseIDException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadStationIdCourseIDException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadStationIdCourseIDException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    [Serializable]
    public class BadLicenseIdCourseIDException : Exception
    {
        public string xmlFilePath;
        public BadLicenseIdCourseIDException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadLicenseIdCourseIDException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadLicenseIdCourseIDException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    [Serializable]
    public class beyondTimeLimitLineException : Exception
    {
        public string xmlFilePath;
        public beyondTimeLimitLineException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public beyondTimeLimitLineException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public beyondTimeLimitLineException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    [Serializable]
    public class BadCodeStationIDException : Exception
    {
        public string xmlFilePath;
        public BadCodeStationIDException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadCodeStationIDException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadCodeStationIDException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    [Serializable]
    public class BadLocationException : Exception
    {
        public string xmlFilePath;
        public BadLocationException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadLocationException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadLocationException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
    [Serializable]
    public class BadNotExsitingStationException : Exception
    {
        public string xmlFilePath;
        public BadNotExsitingStationException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadNotExsitingStationException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadNotExsitingStationException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
