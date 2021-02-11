﻿using System;
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

          public override string ToString() => base.ToString() + $", bad Bus id: {ID}";
      }
      [Serializable]
      public class BadBusIdException : Exception
      {
          public string BusID;
          public string LicenseNum;
          public BadBusIdException(string BID, string LID) : base() { BusID = BID; LicenseNum = LID; }
          public BadBusIdException(string BID, string LID, string message) :
              base(message)
          { BusID = BID; LicenseNum = LID; }
          public BadBusIdException(string BID, string LID, string message, Exception innerException) :
              base(message, innerException)
          { BusID = BID; LicenseNum = LID; }

          public override string ToString() => base.ToString() + $", bad Bus id: {BusID} and Station id: {LicenseNum}";
      }
      [Serializable]
      public class BadStationException : Exception
      {
          public string xmlFilePath;
          public BadStationException(string xmlPath) : base() { xmlFilePath = xmlPath; }
          public BadStationException(string xmlPath, string message) :
              base(message)
          { xmlFilePath = xmlPath; }
          public BadStationException(string xmlPath, string message, Exception innerException) :
              base(message, innerException)
          { xmlFilePath = xmlPath; }

          public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
      }
      [Serializable]
      public class BadLicenseIdException : Exception
      {
          public string xmlFilePath;
          public BadLicenseIdException(string xmlPath) : base() { xmlFilePath = xmlPath; }
          public BadLicenseIdException(string xmlPath, string message) :
              base(message)
          { xmlFilePath = xmlPath; }
          public BadLicenseIdException(string xmlPath, string message, Exception innerException) :
              base(message, innerException)
          { xmlFilePath = xmlPath; }

          public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
      }
    [Serializable]
    public class BadStationNumException : Exception
    {
        public string xmlFilePath;
        public BadStationNumException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public BadStationNumException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public BadStationNumException(string xmlPath, string message, Exception innerException) :
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

     [Serializable]
    public class BadBusLineIdException : Exception
    {
        public string Num;
        public BadBusLineIdException(string message, Exception innerException) :
            base(message, innerException) => Num = ((DO.BadBusLicenseNumException)innerException).LicenseNum;
        public override string ToString() => base.ToString() + $", bad BusLine num: {Num}";
    }

    [Serializable]
    public class BadDrivingBusException : Exception
    {
        public string ID;
        public BadDrivingBusException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadBusLicenseNumException)innerException).LicenseNum;
        public override string ToString() => base.ToString() + $", bad DrivingBus id: {ID}";
    }

    [Serializable]
    public class BadOutGoingLineException : Exception
    {
        public string ID;
        public BadOutGoingLineException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadBusLicenseNumException)innerException).LicenseNum;
        public override string ToString() => base.ToString() + $", bad OutGoingLine id: {ID}";
    }

    [Serializable]//לטפל בזה
    public class BadBusLineIdStationIDException : Exception
    {
        public string BusID;
        public int BusNum;
        public BadBusLineIDException(string message, Exception innerException) :
            base(message, innerException)
        {
            BusID = ((DO.BadBusLineException)innerException).BusID;
            BusNum = ((DO.BadBusLineException)innerException).BusNum;
        }
        public override string ToString() => base.ToString() + $", bad BusLine id: {BusID} and BusLineNum : {BusNum}";
    }
}
