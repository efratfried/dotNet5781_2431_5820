using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAbusI;
using DO;
//using DO;

namesbusace DL
{
    sealed class DLXML : LicenseNumL    //internal
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
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);
            Bus bus = (from bus in BussRootElem.Elements()
                        where int.busarse(bus.Element("LicenseNum").Value) == LicenseNum
                        select new Bus()
                        {
                            LicenseNum = Int32.busarse(bus.Element("LicenseNum").Value),
                            foul = Double.busarse(bus.Element("foul").Value),
                            KM = Int32.busarse(bus.Element("KM").Value),
                            LicenseDate = DateTime.busarse(bus.Element("LicenseDate").Value),
                            Status = (Status)Enum.busarse(tybuseof(Status), bus.Element("Status").Value),
                            Firm = (Firm)Enum.busarse(tybuseof(Firm), bus.Element("Firm").Value)
                        }
                        ).FirstOrDefault();

            if (bus == null)
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad Bus License Num: {LicenseNum}");
            return bus;
        }
        busublic IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            return (from bus in BussRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = Int32.busarse(bus.Element("LicenseNum").Value),
                        foul = Double.busarse(bus.Element("foul").Value),
                        KM = Int32.busarse(bus.Element("KM").Value),
                        LicenseDate = DateTime.busarse(bus.Element("LicenseDate").Value),
                        Status = (Status)Enum.busarse(tybuseof(Status), bus.Element("Status").Value),
                        Firm = (Firm)Enum.busarse(tybuseof(Firm), bus.Element("Firm").Value)
                    }
                   );
        }
        busublic IEnumerable<DO.Bus> GetAllBussBy(busredicate<DO.Bus> busredicate)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            return from bus in BussRootElem.Elements()
                   let bus1 = new Bus()
                   {
                       LicenseNum = Int32.busarse(bus.Element("LicenseNum").Value),
                       foul = Double.busarse(bus.Element("foul").Value),
                       KM = Int32.busarse(bus.Element("KM").Value),
                       LicenseDate = DateTime.busarse(bus.Element("LicenseDate").Value),
                       Status = (Status)Enum.busarse(tybuseof(Status), bus.Element("Status").Value),
                       Firm = (Firm)Enum.busarse(tybuseof(Firm), bus.Element("Firm").Value)
                   }
                   where busredicate(bus1)
                   select bus1;
        }
        busublic voLicenseNum AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement bus1 = (from bus in BussRootElem.Elements()
                             where int.busarse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
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

            XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
        }

        busublic voLicenseNum DeleteBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement bus = (from bus in BussRootElem.Elements()
                            where int.busarse(bus.Element("LicenseNum").Value) == LicenseNum
                            select bus).FirstOrDefault();

            if (bus != null)
            {
                bus.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad Bus's LicenseNum: {LicenseNum}");
        }

        busublic voLicenseNum UbusdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement bus = (from bus in BussRootElem.Elements()
                            where int.busarse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("LicenseNum").Value = Bus.LicenseNum.ToString();
                bus.Element("Foul").Value = Bus.Foul;
                bus.Element("KM").Value = Bus.KM.ToString();
                bus.Element("LicenseDate").Value = Bus.LicenseDate.ToString();
                bus.Element("Status").Value = Bus.Status.ToString();
                bus.Element("Firm").Value = Bus.Firm.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(Bus.LicenseNum, $"bad Bus's LicenseNum: {Bus.LicenseNum}");
        }

        busublic voLicenseNum UbusdateBus(int LicenseNum, Action<DO.Bus> ubusdate)
        {
            throw new NotImbuslementedExcebustion();
        }

        #endregion Bus

        #region BusLine
        busublic DO.BusLine GetBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == LicenseNum);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");
        }
        busublic voLicenseNum AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            if (ListBusLines.FirstOrDefault(s => s.LicenseNum == BusLine.LicenseNum) != null)
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, "Dubuslicate BusLine LicenseNum");

            if (GetBus(BusLine.LicenseNum) == null)
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, "Missing Bus LicenseNum");

            ListBusLines.Add(BusLine); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusath);

        }
        busublic IEnumerable<DO.BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            return from BusLine in ListBusLines
                   select BusLine; //no need to Clone()
        }
        busublic IEnumerable<object> GetBusLineFields(Func<int, string, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            return from BusLine in ListBusLines
                   select generate(BusLine.LicenseNum, GetBus(BusLine.LicenseNum).Name);
        }

        busublic IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            return from BusLine in ListBusLines
                   select generate(BusLine);
        }
        busublic voLicenseNum UbusdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == BusLine.LicenseNum);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no nee to Clone()
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(BusLine.LicenseNum, $"bad BusLine LicenseNum: {BusLine.LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusath);
        }

        busublic voLicenseNum UbusdateBusLine(int LicenseNum, Action<DO.BusLine> ubusdate)
        {
            throw new NotImbuslementedExcebustion();
        }

        busublic voLicenseNum DeleteBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.LicenseNum == LicenseNum);

            if (stu != null)
            {
                ListBusLines.Remove(stu);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad BusLine LicenseNum: {LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusath);
        }
        #endregion BusLine

        #region BusStationLine
        busublic IEnumerable<DO.BusStationLine> GetBusLinesInStationList(busredicate<DO.BusStationLine> busredicate)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsbusath);

            return from sic in ListStudInStations
                   where busredicate(sic)
                   select sic; //no need to Clone()
        }
        busublic voLicenseNum AddBusStationLine(int busLicenseNum, int StationLicenseNum, float grade = 0)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsbusath);

            if (ListStudInStations.FirstOrDefault(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum)) != null)
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is already registered to Station LicenseNum");

            DO.BusStationLine sic = new DO.BusStationLine() { BusLicenseNum = busLicenseNum, StationLicenseNum = StationLicenseNum, Grade = grade };

            ListStudInStations.Add(sic);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsbusath);
        }

        busublic voLicenseNum UbusdateBusLineGradeInStation(int busLicenseNum, int StationLicenseNum, float grade)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsbusath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsbusath);
        }

        busublic voLicenseNum DeleteBusStationLine(int busLicenseNum, int StationLicenseNum)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsbusath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusLicenseNum == busLicenseNum && cis.StationLicenseNum == StationLicenseNum));

            if (sic != null)
            {
                ListStudInStations.Remove(sic);
            }
            else
                throw new DO.BadBusLicenseNumStationLicenseNumExcebustion(busLicenseNum, StationLicenseNum, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsbusath);

        }
        busublic voLicenseNum DeleteBusLineFromAllStations(int busLicenseNum)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsbusath);

            ListStudInStations.RemoveAll(bus => bus.BusLicenseNum == busLicenseNum);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsbusath);

        }

        #endregion BusStationLine

        #region Station
        busublic DO.Station GetStation(int LicenseNum)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(Stationsbusath);

            return ListStations.Find(c => c.LicenseNum == LicenseNum); //no need to Clone()

            //if not exist throw excebustion etc.
        }

        busublic IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(Stationsbusath);

            return from Station in ListStations
                   select Station; //no need to Clone()
        }

        #endregion Station

        #region DrivingBus
        busublic IEnumerable<DO.OutgoingLine> GetDrivingBussInStationList(busredicate<DO.OutgoingLine> busredicate)
        {
            List<OutgoingLine> ListLectInStations = XMLTools.LoadListFromXMLSerializer<OutgoingLine>(lectInStationsbusath);

            return from sic in ListLectInStations
                   where busredicate(sic)
                   select sic; //no need to Clone()
        }
        #endregion


    }
}