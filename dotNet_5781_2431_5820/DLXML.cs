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
    sealed class DLXML : IDL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
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
        public DO.Bus GetBus(int id)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            Bus p = (from per in BussRootElem.Elements()
                        where int.Parse(per.Element("ID").Value) == id
                        select new Bus()
                        {
                            ID = Int32.Parse(per.Element("ID").Value),
                            Name = per.Element("Name").Value,
                            Street = per.Element("Street").Value,
                            HouseNumber = Int32.Parse(per.Element("HouseNumber").Value),
                            City = per.Element("City").Value,
                            BirthDate = DateTime.Parse(per.Element("BirthDate").Value),
                            BusalStatus = (BusalStatus)Enum.Parse(typeof(BusalStatus), per.Element("BusalStatus").Value)
                        }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.BadBusIdException(id, $"bad Bus id: {id}");

            return p;
        }
        public IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            return (from p in BussRootElem.Elements()
                    select new Bus()
                    {
                        ID = Int32.Parse(p.Element("ID").Value),
                        Name = p.Element("Name").Value,
                        Street = p.Element("Street").Value,
                        HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
                        City = p.Element("City").Value,
                        BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
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
                       ID = Int32.Parse(p.Element("ID").Value),
                       Name = p.Element("Name").Value,
                       Street = p.Element("Street").Value,
                       HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
                       City = p.Element("City").Value,
                       BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
                       BusalStatus = (BusalStatus)Enum.Parse(typeof(BusalStatus), p.Element("BusalStatus").Value)
                   }
                   where predicate(p1)
                   select p1;
        }
        public void AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement per1 = (from p in BussRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == Bus.ID
                             select p).FirstOrDefault();

            if (per1 != null)
                throw new DO.BadBusIdException(Bus.ID, "Duplicate Bus ID");

            XElement BusElem = new XElement("Bus",
                                   new XElement("ID", Bus.ID),
                                   new XElement("", Bus.Name),
                                   new XElement("Street", Bus.Street),
                                   new XElement("HouseNumber", Bus.HouseNumber.ToString()),
                                   new XElement("City", Bus.City),
                                   new XElement("BirthDate", Bus.BirthDate),
                                   new XElement("BusalStatus", Bus.BusalStatus.ToString()));

            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
        }

        public void DeleteBus(int id)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement per = (from p in BussRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == id
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
            }
            else
                throw new DO.BadBusIdException(id, $"bad Bus id: {id}");
        }

        public void UpdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BussPath);

            XElement per = (from p in BussRootElem.Elements()
                            where int.Parse(p.Element("ID").Value) == Bus.ID
                            select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("ID").Value = Bus.ID.ToString();
                per.Element("Name").Value = Bus.Name;
                per.Element("Street").Value = Bus.Street;
                per.Element("HouseNumber").Value = Bus.HouseNumber.ToString();
                per.Element("City").Value = Bus.City;
                per.Element("BirthDate").Value = Bus.BirthDate.ToString();
                per.Element("BusalStatus").Value = Bus.BusalStatus.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, BussPath);
            }
            else
                throw new DO.BadBusIdException(Bus.ID, $"bad Bus id: {Bus.ID}");
        }

        public void UpdateBus(int id, Action<DO.Bus> update)
        {
            throw new NotImplementedException();
        }

        #endregion Bus

        #region BusLine
        public DO.BusLine GetBusLine(int id)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.ID == id);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusIdException(id, $"bad BusLine id: {id}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            if (ListBusLines.FirstOrDefault(s => s.ID == BusLine.ID) != null)
                throw new DO.BadBusIdException(BusLine.ID, "Duplicate BusLine ID");

            if (GetBus(BusLine.ID) == null)
                throw new DO.BadBusIdException(BusLine.ID, "Missing Bus ID");

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
                   select generate(BusLine.ID, GetBus(BusLine.ID).Name);
        }

        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            return from BusLine in ListBusLines
                   select generate(BusLine);
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.ID == BusLine.ID);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no nee to Clone()
            }
            else
                throw new DO.BadBusIdException(BusLine.ID, $"bad BusLine id: {BusLine.ID}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesPath);
        }

        public void UpdateBusLine(int id, Action<DO.BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int id)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesPath);

            DO.BusLine stu = ListBusLines.Find(p => p.ID == id);

            if (stu != null)
            {
                ListBusLines.Remove(stu);
            }
            else
                throw new DO.BadBusIdException(id, $"bad BusLine id: {id}");

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
        public void AddBusStationLine(int perID, int StationID, float grade = 0)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            if (ListStudInStations.FirstOrDefault(cis => (cis.BusId == perID && cis.StationId == StationID)) != null)
                throw new DO.BadBusIdStationIDException(perID, StationID, "Bus ID is already registered to Station ID");

            DO.BusStationLine sic = new DO.BusStationLine() { BusId = perID, StationId = StationID, Grade = grade };

            ListStudInStations.Add(sic);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);
        }

        public void UpdateBusLineGradeInStation(int perID, int StationID, float grade)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusId == perID && cis.StationId == StationID));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadBusIdStationIDException(perID, StationID, "Bus ID is NOT registered to Station ID");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);
        }

        public void DeleteBusStationLine(int perID, int StationID)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            DO.BusStationLine sic = ListStudInStations.Find(cis => (cis.BusId == perID && cis.StationId == StationID));

            if (sic != null)
            {
                ListStudInStations.Remove(sic);
            }
            else
                throw new DO.BadBusIdStationIDException(perID, StationID, "Bus ID is NOT registered to Station ID");

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);

        }
        public void DeleteBusLineFromAllStations(int perID)
        {
            List<BusStationLine> ListStudInStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(studInStationsPath);

            ListStudInStations.RemoveAll(p => p.BusId == perID);

            XMLTools.SaveListToXMLSerializer(ListStudInStations, studInStationsPath);

        }

        #endregion BusStationLine

        #region Station
        public DO.Station GetStation(int id)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return ListStations.Find(c => c.ID == id); //no need to Clone()

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