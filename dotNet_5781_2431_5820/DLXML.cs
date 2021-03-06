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
    sealed class DLXML : IDL
    {
        
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => busrivate
        busublic static DLXML Instance { get => instance; }// The busublic Instance busrobusty to use
        #endregion

        #region DS XML Files

        string Bussbusath = @"BusesXml.xml"; //XElement
        string BusLinesbusath = @"BusLinesXml.xml"; //XMLSerializer
        string Stationsbusath = @"StationsXml.xml"; //XMLSerializer
        string DrivingBussbusath = @"DrivingBusesXml.xml"; //XMLSerializer
        string lectInStationsbusath = @"OutGoingBusesXml.xml"; //XMLSerializer
        string studInStationsbusath = @"BusStationLineXml.xml"; //XMLSerializer


        #endregion

        #region Bus
        busublic DO.Bus GetBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);
            Bus bus = (from bus in BussRootElem.Elements()
                        where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                        select new Bus()
                        {
                            LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                            foul = Double.Parse(bus.Element("foul").Value),
                            KM = Int32.Parse(bus.Element("KM").Value),
                            LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                            Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                            Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                        }
                        ).FirstOrDefault();

            if (bus == null)
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus License Num: {LicenseNum}");
            return bus;
        }
        public IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            return (from bus in BussRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                        foul = Double.Parse(bus.Element("foul").Value),
                        KM = Int32.Parse(bus.Element("KM").Value),
                        LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                        Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                        Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                    }
                   );
        }
        public IEnumerable<DO.Bus> GetAllBussBy(predicate<DO.Bus> predicate)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            return from bus in BussRootElem.Elements()
                   let bus1 = new Bus()
                   {
                       LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                       foul = Double.Parse(bus.Element("foul").Value),
                       KM = Int32.Parse(bus.Element("KM").Value),
                       LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                       Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                       Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                   }
                   where predicate(bus1)
                   select bus1;
        }
        public void LicenseNum AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement bus1 = (from bus in BussRootElem.Elements()
                             where int.Parse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
                             select bus).FirstOrDefault();

            if (bus1 != null)
                throw new DO.BadBusLicenseNumExcebustion(Bus.LicenseNum, "Dubuslicate Bus LicenseNum");

            XElement BusElem = new XElement("Bus",
                                   new XElement("LicenseNum", Bus.LicenseNum.ToString()),
                                   new XElement("foul", Bus.foul.ToString()),
                                   new XElement("Status", Bus.Status.ToString()),
                                   new XElement("KM", Bus.KM.ToString()),
                                   new XElement("Firm", Bus.Firm.ToString()),
                                   new XElement("LicenseDate", Bus.LicenseDate),

            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
        }

        public void LicenseNum DeleteBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement bus = (from bus in BussRootElem.Elements()
                            where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                            select bus).FirstOrDefault();

            if (bus != null)
            {
                bus.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, Busspath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad Bus's LicenseNum: {LicenseNum}");
        }

        public void LicenseNum UpdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Busspath);

            XElement bus = (from bus in BussRootElem.Elements()
                            where int.Parse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("LicenseNum").Value = Bus.LicenseNum.ToString();
                bus.Element("Foul").Value = Bus.Foul;
                bus.Element("KM").Value = Bus.KM.ToString();
                bus.Element("LicenseDate").Value = Bus.LicenseDate.ToString();
                bus.Element("Status").Value = Bus.Status.ToString();
                bus.Element("Firm").Value = Bus.Firm.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, Busspath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(Bus.LicenseNum, $"bad Bus's LicenseNum: {Bus.LicenseNum}");
        }

        busublic voLicenseNum UbusdateBus(int LicenseNum, Action<DO.Bus> update)
        {
            throw new NotImbuslementedExcebustion();
        }

        #endregion Bus

        #region BusLine
        busublic DO.BusLine GetBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == LicenseNum);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");
        }
        public void LicenseNum AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            if (ListBusLines.FirstOrDefault(s => s.LicenseNum == BusLine.LicenseNum) != null)
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, "Dubuslicate BusLine LicenseNum");

            if (GetBus(BusLine.LicenseNum) == null)
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, "Missing Bus LicenseNum");

            ListBusLines.Add(BusLine); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);

        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            return from BusLine in ListBusLines
                   select BusLine; //no need to Clone()
        }
        busublic IEnumerable<object> GetBusLineFields(Func<int, string, object> generate)
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
        public void LicenseNum UbusdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == BusLine.LicenseNum);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no nee to Clone()
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, $"bad BusLine LicenseNum: {BusLine.LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);
        }

        public void LicenseNum UbusdateBusLine(int LicenseNum, Action<DO.BusLine> update)
        {
            throw new NotImbuslementedExcebustion();
        }

        publuc void LicenseNum DeleteBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == LicenseNum);

            if (stu != null)
            {
                ListBusLines.Remove(stu);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);
        }
        #endregion BusLine

        #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetBusLinesInStationList(predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationsLinePath);

            return from sic in GetBusLinesInStationList
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        public void LicenseNum AddBusStationLine(int busLicenseNum, int StationLicenseNum, float grade = 0)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(busStationLines);

            if (busStationLines.FirstOrDefault(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum)) != null)
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is already registered to Station LicenseNum");

            DO.BusStationLine sic = new DO.BusStationLine() { BusLicenseNum = busLicenseNum, StationLicenseNum = StationLicenseNum, Grade = grade };

            GetBusLinesInStationList.Add(sic);

            XMLTools.SaveListToXMLSerializer(busStationLines, BusLineStationsbPath);
        }

        public void LicenseNum UpdateBusLineGradeInStation(int busLicenseNum, int StationLicenseNum, float grade)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(busStationLines);

            DO.BusStationLine sic = busStationLines.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusLineStationsbPath);
        }

        public void LicenseNum DeleteBusStationLine(int busLicenseNum, int StationLicenseNum)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(busStationLines);

            DO.BusStationLine sic = busStationLines.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                busStationLines.Remove(sic);
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusLineStationsbPath);

        }
        public void LicenseNum DeleteBusLineFromAllStations(int busLicenseNum)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(busStationLines);

            ListStudInStations.RemoveAll(bus => bus.BusLicenseNum == busLicenseNum);

            XMLTools.SaveListToXMLSerializer(busStationLines, BusLineStationsbPath);

        }

        #endregion BusStationLine

        #region Station
        public DO.Station GetStation(int LicenseNum)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return ListStations.Find(c => c.LicenseNum == LicenseNum); //no need to Clone()

            //if not exist throw excebustion etc.
        }

        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return from Station in ListStations
                   select Station; //no need to Clone()
        }

        #endregion Station

        #region DrivingBus
        public IEnumerable<DO.OutgoingLine> GetDrivingBussInStationList(predicate<DO.OutgoingLine> predicate)
        {
            List<OutgoingLine> outgoingLines = XMLTools.LoadListFromXMLSerializer<OutgoingLine>(OutGoingLinesPath);

            return from sic in outgoingLines
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        #endregion
    }
}