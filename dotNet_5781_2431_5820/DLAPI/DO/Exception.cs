using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
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
        public override string ToString() => base.ToString() + $", bad driver id: {ID}";
        }
    public class BadBusLineException : Exception
    {//busline related
        public string BusID;
        public string BusNum;
        public BadBusLineException(string BID, string BNum) : base() { BusID = BID; BusNum = BNum; }
        public BadBusLineException(string BID, string BNum, string message) :
            base(message)
        { BusID = BID; BusNum = BNum; }
        public BadBusLineException(string BID, string BNum, string message, Exception innerException) :
            base(message, innerException)
        { BusID = BID; BusNum = BNum; }

        public override string ToString() => base.ToString() + $", bad Bus id: {BusID} the Num is: {BusNum}";
    }
    public class BadBusLicenseNumException : Exception
    {
        public string LicenseNum;
        public BadBusLicenseNumException(string L) : base() => LicenseNum = L;
        public BadBusLicenseNumException(string L, string message) :
            base(message) => LicenseNum = L;
        public BadBusLicenseNumException(string L, string message, Exception innerException) :
            base(message, innerException) => LicenseNum = L;

        public override string ToString() => base.ToString() + $", bad License num : {LicenseNum}";
    }
    public class BadStationNumException : Exception
    {
        public string LicenseNum;
        public BadStationNumException(string L) : base() => LicenseNum = L;
        public BadStationNumException(string L, string message) :
            base(message) => LicenseNum = L;
        public BadStationNumException(string L, string message, Exception innerException) :
            base(message, innerException) => LicenseNum = L;

        public override string ToString() => base.ToString() + $", bad station num : {LicenseNum}";
    }
    public class BadLocationExeption:Exception
    {
        public double Langtitude;
        public double Longtitude;
        public BadLocationExeption(double rochav, double orech) : base() { Langtitude = rochav; Longtitude = orech; }
        public BadLocationExeption(double rochav, double orech, string message) :
            base(message)
        { Langtitude = rochav; Longtitude = orech; }
        public BadLocationExeption(double rochav, double orech, string message, Exception innerException) :
            base(message, innerException)
        { Langtitude = rochav; Longtitude = orech; }

        public override string ToString() => base.ToString() + $", bad langtitude cordinates : {Langtitude} the logtitude cordinates are: {Longtitude}";
    }
    public class BadLicenseNumException : Exception
    {
        public string License;
        public BadLicenseNumException(string L) : base() => License = L;
        public BadLicenseNumException(string L, string message) :
            base(message) => License = L;
        public BadLicenseNumException(string L, string message, Exception innerException) :
            base(message, innerException) => License = L;

        public override string ToString() => base.ToString() + $", bad License num : {License}";
    }
    public class beyondTimeLimitLineException : Exception
    {
        public DateTime Time;
        public beyondTimeLimitLineException(DateTime T) : base() => Time = T;
        public beyondTimeLimitLineException(DateTime T, string message) :
            base(message) => Time = T;
        public beyondTimeLimitLineException(DateTime T, string message, Exception innerException) :
            base(message, innerException) => Time = T;

        public override string ToString() => base.ToString() + $", bad working hours : {Time}";
    }
    public class BadCodeStationException : Exception
    {
        public string StationCode;
        public BadCodeStationException(string Code) : base() => StationCode = Code;
        public BadCodeStationException(string Code, string message) :
            base(message) => StationCode = Code;
        public BadCodeStationException(string Code, string message, Exception innerException) :
            base(message, innerException) => StationCode = Code;

        public override string ToString() => base.ToString() + $", bad station code : {StationCode}";
    }
    public class BadStationNameException : Exception
    {
        public string StationName;
        public BadStationNameException(string Name) : base() => StationName = Name;
        public BadStationNameException(string Name, string message) :
            base(message) => StationName = Name;
        public BadStationNameException(string Name, string message, Exception innerException) :
            base(message, innerException) => StationName = Name;

        public override string ToString() => base.ToString() + $", bad station's name : {StationName}";
    }
    public class BadUserName_PasswordException : Exception
    {
        public string Password;
        public string Name;
        public BadUserName_PasswordException(string pass,string name) : base() { Password=pass; Name=name; }
        public BadUserName_PasswordException(string pass, string name, string message) :
            base(message) { Password = pass; Name = name; }
        public BadUserName_PasswordException(string pass, string name, string message, Exception innerException) :
            base(message, innerException) { Password = pass; Name = name; }
        public override string ToString() => base.ToString() + $", bad User's Password : {Password}";
    }
    public class BadStationIndexInLineException : Exception
    {
        public int Index;
        public BadStationIndexInLineException(int index) : base() => Index = index;
        public BadStationIndexInLineException(int index, string message) :
            base(message) => Index = index;
        public BadStationIndexInLineException(int index, string message, Exception innerException) :
            base(message, innerException) => Index = index;

        public override string ToString() => base.ToString() + $", bad station's index on the line : {Index}";
    }
    public class BadUserDriveNameException : Exception
    {
        public string Name;
        public BadUserDriveNameException(string name) : base() => Name = name;
        public BadUserDriveNameException(string name, string message) :
            base(message) => Name = name;
        public BadUserDriveNameException(string name, string message, Exception innerException) :
            base(message, innerException) => Name = name;

        public override string ToString() => base.ToString() + $", bad User's Drive name : {Name}";
    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

}
