using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
//using DO;

namespace DL
{
    sealed class DLXML : LicenseNumL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance probusty to use
        #endregion

        #region DS XML Files

        string BussPath = @"BusesXml.xml"; //XElement
        string BusLinesPath = @"BusLinesXml.xml"; //XMLSerializer
        string StationsPath = @"StationsXml.xml"; //XMLSerializer
        string DrivingBussPath = @"DrivingBusesXml.xml"; //XMLSerializer
        string lectInStationsPath = @"OutGoingBusesXml.xml"; //XMLSerializer
        string studInStationsPath = @"BusStationLineXml.xml"; //XMLSerializer


        #endregion

        #region Bus
        public DO.Bus GetBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);
            Bus p = (from bus in BussRootElem.Elements()
                        where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                        select new Bus()
                        {
                            LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                            foul = Double.Parse(bus.Element("LicenseNum").Value),
                            KM = Int32.Parse(bus.Element("KM").Value),
                            LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                            Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                            Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                        }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus License Num: {LicenseNum}");
            return p;
        }
        public IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            return (from p in BussRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = Int32.Parse(p.Element("LicenseNum").Value),
                        Name = p.Element("Name").Value,
                        Street = p.Element("Street").Value,
                        KM = Int32.Parse(p.Element("KM").Value),
                        City = p.Element("City").Value,
                        LicenseDate = DateTime.Parse(p.Element("LicenseDate").Value),
                        BusalStatus = (BusalStatus)Enum.Parse(typeof(BusalStatus), p.Element("BusalStatus").Value)
                    }
                   );
        }
        public IEnumerable<DO.Bus> GetAllBussBy(Predicate<DO.Bus> predicate)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            return from p in BussRootElem.Elements()
                   let p1 = new Bus()
                   {
                       LicenseNum = Int32.Parse(p.Element("LicenseNum").Value),
                       Name = p.Element("Name").Value,
                       Street = p.Element("Street").Value,
                       KM = Int32.Parse(p.Element("KM").Value),
                       City = p.Element("City").Value,
                       LicenseDate = DateTime.Parse(p.Element("LicenseDate").Value),
                       BusalStatus = (BusalStatus)Enum.Parse(typeof(BusalStatus), p.Element("BusalStatus").Value)
                   }
                   where predicate(p1)
                   select p1;
        }
        public voLicenseNum AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement bus1 = (from p in BussRootElem.Elements()
                             where int.Parse(p.Element("LicenseNum").Value) == Bus.LicenseNum
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, "Duplicate Bus LicenseNum");

            XElement BusElem = new XElement("Bus",
                                   new XElement("LicenseNum", Bus.LicenseNum),
                                   new XElement("", Bus.Name),
                                   new XElement("Street", Bus.Street),
                                   new XElement("KM", Bus.KM.ToString()),
                                   new XElement("City", Bus.City),
                                   new XElement("LicenseDate", Bus.LicenseDate),
                                   new XElement("BusalStatus", Bus.BusalStatus.ToString()));

            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
        }

        public voLicenseNum DeleteBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement bus = (from p in BussRootElem.Elements()
                            where int.Parse(p.Element("LicenseNum").Value) == LicenseNum
                            select p).FirstOrDefault();

            if (bus != null)
            {
                bus.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
            }
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus LicenseNum: {LicenseNum}");
        }

        public voLicenseNum UpdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement bus = (from p in BussRootElem.Elements()
                            where int.Parse(p.Element("LicenseNum").Value) == Bus.LicenseNum
                            select p).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("LicenseNum").Value = Bus.LicenseNum.ToString();
                bus.Element("Name").Value = Bus.Name;
                bus.Element("Street").Value = Bus.Street;
                bus.Element("KM").Value = Bus.KM.ToString();
                bus.Element("City").Value = Bus.City;
                bus.Element("LicenseDate").Value = Bus.LicenseDate.ToString();
                bus.Element("BusalStatus").Value = Bus.BusalStatus.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
            }
            else
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, $"bad Bus LicenseNum: {Bus.LicenseNum}");
        }

        public voLicenseNum UpdateBus(int LicenseNum, Action<DO.Bus> update)
        {
            throw new NotImplementedException();
        }

        #endregion Bus

        #region BusLine
        public DO.BusLine GetBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.LicenseNum == LicenseNum);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");
        }
        public voLicenseNum AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            if (ListBusLines.FirstOrDefault(s => s.LicenseNum == BusLine.LicenseNum) != null)
                throw new DO.BadBusLicenseNumException(BusLine.LicenseNum, "Duplicate BusLine LicenseNum");

            if (GetBus(BusLine.LicenseNum) == null)
                throw new DO.BadBusLicenseNumException(BusLine.LicenseNum, "Missing Bus LicenseNum");

            ListBusLines.Add(BusLine); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);

        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            return from BusLine in ListBusLines
                   select BusLine; //no need to Clone()
        }
        public IEnumerable<object> GetBusLineFields(Func<int, string, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            return from BusLine in ListBusLines
                   select generate(BusLine.LicenseNum, GetBus(BusLine.LicenseNum).Name);
        }

        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            return from BusLine in ListBusLines
                   select generate(BusLine);
        }
        public voLicenseNum UpdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.LicenseNum == BusLine.LicenseNum);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no nee to Clone()
            }
            else
                throw new DO.BadBusLicenseNumException(BusLine.LicenseNum, $"bad BusLine LicenseNum: {BusLine.LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);
        }

        public voLicenseNum UpdateBusLine(int LicenseNum, Action<DO.BusLine> update)
        {
            throw new NotImplementedException();
        }

        public voLicenseNum DeleteBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.LicenseNum == LicenseNum);

            if (stu != null)
            {
                ListBusLines.Remove(stu);
            }
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);
        }
        #endregion BusLine

        #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetBusLinesInStationList(Predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            return from sic in ListStudInStations
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        public voLicenseNum AddBusStationLine(int busLicenseNum, int StationLicenseNum, float grade = 0)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            if (ListStudInStations.FirstOrDefault(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum)) != null)
                throw new DO.BadBusLicenseNumStationLicenseNumException(busLicenseNum, StationLicenseNum, "Bus LicenseNum is already registered to Station LicenseNum");

            DO.BusStationLine sic = new DO.BusStationLine() { BusLicenseNum = busLicenseNum, StationLicenseNum = StationLicenseNum, Grade = grade };

            ListStudInStations.Add(sic);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);
        }

        public voLicenseNum UpdateBusLineGradeInStation(int busLicenseNum, int StationLicenseNum, float grade)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumException(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);
        }

        public voLicenseNum DeleteBusStationLine(int busLicenseNum, int StationLicenseNum)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                ListStudInStations.Remove(sic);
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumException(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);

        }
        public voLicenseNum DeleteBusLineFromAllStations(int busLicenseNum)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            ListStudInStations.RemoveAll(p => p.BusLicenseNum == busLicenseNum);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);

        }

        #endregion BusStationLine

        #region Station
        public DO.Station GetStation(int LicenseNum)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return ListStations.Find(c => c.LicenseNum == LicenseNum); //no need to Clone()

            //if not exist throw exception etc.
        }

        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return from Station in ListStations
                   select Station; //no need to Clone()
        }

        #endregion Station

        #region DrivingBus
        public IEnumerable<DO.OutgoingLine> GetDrivingBussInStationList(Predicate<DO.OutgoingLine> predicate)
        {
            List<OutgoingLine> ListLectInStations = XMLTools.LoadListFromXMLSerializer<OutgoingLine>(lectInStationsPath);

            return from sic in ListLectInStations
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        #endregion


    }
}