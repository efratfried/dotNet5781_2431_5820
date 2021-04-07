using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
//using DS;
namespace DL
{
    sealed class DLXML : IDL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }

        // static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use        
        #endregion

        #region DS XML Files
        static string AccidentPath = @"AccidentXml.xml"; //XMLSerializer
        static string BususPath = @"BusesXml.xml"; //XElement
        static string BusLinesbusPath = @"BusLinesXml.xml"; //XMLSerializer
        static string StationsPath = @"StationsXml.xml"; //XMLSerializer
        static string DrivingBussbusath = @"DrivingBusesXml.xml"; //XMLSerializer
                                                                  //static string OutGoingBusesPath = @"OutGoingBusesXml.xml"; //XMLSerializer
        static string BusStationLinePath = @"BusStationLineXml.xml"; //XMLSerializer
        static string UserPath = @"UserXml.xml"; //XElement
        //static string AccidentPath = @"AccidentXml.xml"; //XMLSerializer
                                                         //string UserDrivePath = @"UserLineXml.xml"; //XMLSerializer
        static string FollowingStationsPath = @"@FollowingStationssXml.xml";
        //static string runningNumbersPath = "@RunningNumbers.xml";

        //public List<FollowingStations> followingStations = new List<FollowingStations>();
        //public static List<User> UsersList;
        //public static List<BusStationLine> BusStationsLineList;
        /*List<DO.User> list1 = DS.DataSource.Users;
        file = new FileStream(@"..\..\..\bin\xml\UserXml.xml", FileMode.Create);
        x = new XmlSerializer(list1.GetType());
        x.Serialize(file, list1);
            file.Close();*/

        /* public static List<Bus> BussesList;
         public static List<Station> StationsList;
         public static List<BusLine> BusLinesList;

         public static List<OutGoingLine> OutGoingLinesList;

         //public static List<UserDrive> UserDrivesList;
         public static List<Accident> AccidentsList;
         public static List<Treat> TreatsList;
         public static List<DrivingBus> DrivingsList;
         public List<FollowingStations> followingStations;*/

        #endregion

        #region Bus
        /// <summary>
        /// this function gets the bus's license num & checks if it axist ,case it is it return & match apear in the xml file with all the bus's details
        /// else there is an exception.
        /// </summary>
        /// <param name="LicenseNum"></param>
        /// <returns></returns>
        public DO.Bus GetBus(string LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);
            Bus bus1 = (from bus in BussRootElem.Elements()
                        where (bus.Element("LicenseNum").Value) == LicenseNum
                        select new Bus()
                        {
                            LicenseNum = (bus.Element("LicenseNum").Value),
                            foul = Double.Parse(bus.Element("foul").Value),
                            KM = Int32.Parse(bus.Element("KM").Value),
                            LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                            Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value),

                            //Status is a property in bo made by a function
                        }
                        ).FirstOrDefault();

            if (bus1 == null)
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus License Num: {LicenseNum}");
            return bus1;
        }
        /// <summary>
        /// that function return all the exist buss in the xml file with all their details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Bus> GetAllBusses()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            return (from bus in BussRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = (bus.Element("LicenseNum").Value),
                        foul = Double.Parse(bus.Element("foul").Value),
                        KM = Int32.Parse(bus.Element("KM").Value),
                        LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                        Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                        //Status is a property in bo made by a function
                    }
                   );
        }
        /// <summary>
        /// this function gets a predifate & return accroding to it the correct bus.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Bus> GetAllBussBy(Predicate<DO.Bus> predicate)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            return from bus in BussRootElem.Elements()
                   let bus1 = new Bus()
                   {
                       LicenseNum = (bus.Element("LicenseNum").Value),
                       foul = Double.Parse(bus.Element("foul").Value),
                       KM = Int32.Parse(bus.Element("KM").Value),
                       LicenseDate = DateTime.Parse(bus.Element("LicenseDate").Value),
                       Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                       //Status is a property in bo made by a function
                   }
                   where predicate(bus1)
                   select bus1;
        }
        /// <summary>
        /// this func gets a generate variable ,it gets all the buss list & return the wanted bus
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate)
        {
            List<Bus> BussesList = XMLTools.LoadListFromXMLSerializer<Bus>(BususPath);
            return from bs in BussesList
                   select bs;
        }
        /// <summary>
        /// this function gets an accurance of a bus ,checks if this bus is already exist in the list if not it add it to the bus's list/file with all it's fields.
        /// else exception .
        /// </summary>
        /// <param name="Bus"></param>
        public void AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            XElement bus1 = (from bus in BussRootElem.Elements()
                             where (bus.Element("LicenseNum").Value) == Bus.LicenseNum
                             select bus).FirstOrDefault();

            if (bus1 != null)
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, "Duplicate Bus's LicenseNums");

            XElement BusElem = new XElement("Bus",
                                   new XElement("LicenseNum", Bus.LicenseNum.ToString()),
                                   new XElement("foul", Bus.foul.ToString()),
                                   new XElement("KM", Bus.KM.ToString()),
                                   new XElement("Firm", Bus.Firm.ToString()),
                                   new XElement("LicenseDate", Bus.LicenseDate));
            //Status is a property in bo made by a function
            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, BususPath);
        }
        /// <summary>
        /// this finction gets a license num of a bus ,checks if it exist already in the bus's list if it does so it earesed it
        /// else exception.
        /// </summary>
        /// <param name="LicenseNum"></param>
        public void DeleteBus(string LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            XElement bus1 = (from bus in BussRootElem.Elements()
                             where (bus.Element("LicenseNum").Value) == LicenseNum
                             select bus).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, BususPath);
            }
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"wrong Bus's LicenseNum: {LicenseNum}");
        }
        /// <summary>
        /// this function gets a bus's accurance ,first checks if it exist if it does we can update the bus's fields.
        /// else exception
        /// </summary>
        /// <param name="Bus"></param>
        public void UpdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            XElement bus = (from bus1 in BussRootElem.Elements()
                            where (bus1.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus1).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("LicenseNum").Value = Bus.LicenseNum;
                bus.Element("foul").Value = Bus.foul.ToString();
                bus.Element("KM").Value = Bus.KM.ToString();
                bus.Element("LicenseDate").Value = Bus.LicenseDate.ToString();
                bus.Element("Firm").Value = Bus.Firm.ToString();
                //Status is a property in bo made by a function

                XMLTools.SaveListToXMLElement(BussRootElem, BususPath);
            }
            else
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, $"wrong Bus's LicenseNum: {Bus.LicenseNum}");
        }

        #endregion Bus

        #region busline
        /// <summary>
        /// the function gets the id of the bus line (it's checks by the id because this is a runinug number ,which means that each line has a different id)
        /// ,checks if it exist if it does so it returns it.
        /// else exception.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DO.BusLine GetBusLine(int ID)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.ID == ID);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumException(ID.ToString(), $"wrong BusLine id: {ID}");
        }
        /// <summary>
        /// the function gets an accuarance of a busline. no need to check if it exist because we want to add a new accurance so the id will be any way different.
        /// & reuttn the busline's id.
        /// </summary>
        /// <param name="BusLine"></param>
        /// <returns></returns>

        public int AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);
            BusLine.ID = ListBusLines.Max(I => I.ID) + 1;
            //if (ListBusLines.FirstOrDefault(s => s.BusNum == BusLine.BusNum) != null)
            // throw new DO.BadBusLicenseNumException(BusLine.BusNum.ToString(), "Duplicate BusLine LicenseNum");

            ListBusLines.Add(BusLine); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);
            return BusLine.ID;
        }
        /// <summary>
        /// the funtion return all hte exist buslines accroding to the xml file.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            return from BusLine in ListBusLines
                   select BusLine; //no need to Clone()
        }
        /// <summary>
        /// the function gets a generate variable & returns the wanted busline's num.
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusLineFields(Func<int, DO.BusLine, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            return from busline in ListBusLines
                   select generate(busline.BusNum, GetBusLine(busline.BusNum));
        }
        /// <summary>
        /// the function gets a generate variable & returns the wanted busline
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object, object> generate)
        {
            List<BusLine> BusLinesList = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);
            return from bs in BusLinesList
                   select bs;
        }
        /// <summary>
        /// the function gets an accurance of a busline,check if it exist.
        /// if it does so we can update ut's fields ,else exception.
        /// </summary>
        /// <param name="BusLine"></param>
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.ID == BusLine.ID);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no need to Clone()
            }
            else
                throw new DO.BadBusLicenseNumException(BusLine.BusNum.ToString(), $"wrong BusLine id: {BusLine.BusNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);
        }
        /// <summary>
        /// no use in this function
        /// </summary>
        /// <param name="LicenseNum"></param>
        /// <param name="update"></param>
        public void UpdateBusLine(int LicenseNum, Action<DO.BusLine> update)
        {
            //throw new NotImplementedExcebustion();
        }
        /// <summary>
        /// the function gets the 
        /// </summary>
        /// <param name="BusNum"></param>
        public void DeleteBusLine(int BusNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);
            DO.BusLine bls = ListBusLines.Find(bus => bus.BusNum == BusNum);

            if (bls != null)
            {
                
                ListBusLines.Remove(bls);
            }
            else
                throw new DO.BadBusLicenseNumException(BusNum.ToString(), $"wring BusLine LicenseNum: {BusNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);
        }
        #endregion BusLine

        #region BusStationLine
        /// <summary>
        /// the function gets a predicate variable so it returns frin the hwole busstationline list the wanted busstation line.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusStationLine> GetBusLinesInStationList(Predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            return from sic in busStationLines
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        /// <summary>
        /// the function gets an accurance of a busstationline ,checks if it is already exist.
        /// if not it add the wanted busstationline. else exception.
        /// </summary>
        /// <param name="busStationLine"></param>
        public void AddBusStationLine(BusStationLine busStationLine)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            if (busStationLines.FirstOrDefault(cis => (cis.BusStationNum == busStationLine.BusStationNum.ToString() && cis.ID == busStationLine.ID.ToString())) != null)
                throw new DO.BadBusStationLineCodeException(busStationLine.BusStationNum, busStationLine.ID.ToString(), "station code is already exist");
            busStationLines.Add(busStationLine);

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);
        }
        /// <summary>
        /// the function gets an accurance of a busstationline check if it already exist in the busstationline list.
        /// if it does exception , else we can update it fields.
        /// </summary>
        /// <param name="busStationLine"></param>
        public void UpdateBusStationLine(BusStationLine busStationLine)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            int index = busStationLines.FindIndex(cis => (cis.BusStationNum == busStationLine.BusStationNum && cis.ID == busStationLine.ID));


            if (index != -1)
            {
                busStationLines[index] = busStationLine;
            }
            else
                throw new DO.BadBusStationLineCodeException(busStationLine.BusStationNum, busStationLine.ID, "This station's code is not exist");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);
        }
        /// <summary>
        /// the function gets the id & the busstation num of the busstationline & chcks by those 2 variables if it exist.
        /// if it does so we delete that ,else exception. the deleting in specific to a specific line.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="BusStationNum"></param>
        public void DeleteBusStationLine(int id, string BusStationNum)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            DO.BusStationLine sic = busStationLines.Find(cis => (cis.BusStationNum == BusStationNum && cis.ID == id.ToString()));

            if (sic != null)
            {
                busStationLines.Remove(sic);
            }
            else
                throw new DO.BadBusStationLineCodeException(BusStationNum, id.ToString(), "This station's code is not exist");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);

        }
        public void DeleteBusStationLine(string id)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            busStationLines.RemoveAll(i => i.ID == id);
            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);

        }
        /// <summary>
        /// the function gets only the busstationline num because we need to delete it from all the lines.
        /// </summary>
        /// <param name="busLinenum"></param>
        public void DeleteBusLineFromAllStations(int busLinenum)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            busStationLines.RemoveAll(bus => bus.ID == busLinenum.ToString());

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);

        }
        /// <summary>
        /// the function gets the line id to return all it's busstationline. (it wouldnt return null because when we add a busline there are at least 3 busstationlines).
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusStationLine> GetLineStationsListOfALine(string lineId)//returns a "line stations" list of the wanted line
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            return from bsl in BusStationsLineList
                   select bsl;
        }
        /// <summary>
        /// the function returns all the existig busstationlines.
        /// </summary>
        /// <param name="busstationline"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusStationLine> GetAllBusStationLines(string busstationline)
        {//returns all members in list
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList

                   select bs;
        }
        /// <summary>
        /// the function gets a predicate that accroding to it ,it return all the wanted busstationlines.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList
                   where predicate(bs)
                   select bs;
        }
        /// <summary>
        /// the function gets the id of a busstationline checks if it already exist.
        /// if it is,it return it,else exceptiom.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DO.BusStationLine GetBusStationLine(string Id)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BusStationLinePath);
            BusStationLine bus1 = (from busstationline in BussRootElem.Elements()
                                   where (busstationline.Element("ID").Value) == Id
                                   select new BusStationLine()
                                   {
                                       BusStationNum = (busstationline.Element("BusStationNum").Value),
                                       IndexInLine = int.Parse((busstationline.Element("IndexInLine").Value)),
                                       //NumOfPassingLines = int.Parse((busstationline.Element("NumOfPassingLines").Value)),
                                   }
                        ).FirstOrDefault();

            if (bus1 == null)
                throw new DO.BadBusStationLineCodeException(bus1.ID, $"This station's code is not exist: {Id}");
            return bus1;
        }
        /// <summary>
        /// the function gets the id & the line's num os we can delete the wanted busstationline from the specific busstationline.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LineNum"></param>
        public void DeleteBusStationLine(string id, string LineNum)
        {
            List<BusStationLine> BussRootElem = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            BussRootElem.RemoveAt(BussRootElem.FindIndex(l => l.ID == id && l.BusStationNum == LineNum));
            XMLTools.SaveListToXMLSerializer(BussRootElem, BusStationLinePath);
        }
        /// <summary>
        /// gets the busstaiton line num to delete it from the entire busstation list.
        /// </summary>
        /// <param name="StationID"></param>
        public void DeleteBusStationLineFromAllStations(string StationID)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BusStationLinePath);
            BussRootElem.RemoveAll();
        }
        /// <summary>
        /// the function gets a generate variable & reutrn accroding to it the wanted accurance
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetBusStationsLineListWithSelectedFields(Func<DO.BusStationLine, object> generate)
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList
                   select bs;
        }
        /// <summary>
        /// get the station code & reutnr all the match stations
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IEnumerable<DO.BusLine> GetLineStationsListThatMatchAStation(int code)//returns a list of the logical stations (line stations) that match a physical station with a given code.
        {
            List<BusStationLine> listLineStations = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            List<DO.BusLine> bs = new List<BusLine>();

            foreach (var item in listLineStations.Where(i => i.BusStationNum==code.ToString()))
            {
                if(GetBusLine(int.Parse(item.ID)) != null)
                {
                    bs.Add(GetBusLine(int.Parse(item.ID)));
                }
            }
            return bs;
            /*return from ls in listLineStations
                   where ls.BusStationNum == code.ToString()
                   let p = GetBusLine(int.Parse(ls.ID))
                   select p;*/
        }

        #endregion BusStationLine

        #region Station
        /// <summary>
        /// the function gets a predicate variable & returns the wanted accurance of the station from the station list.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStations(Predicate<DO.Station> predicate)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return from bs in StationsList
                   where predicate(bs)
                   select bs;
        }
        /// <summary>
        /// the function gets a station code, checks if it exist if it is ,it return the wanted station, else exception.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DO.Station GetStation(string code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            DO.Station stu = ListStations.Find(s => s.CodeStation == code);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadStationNumException(code, $"This station's code is not exist: {code}");
            /*return ListStations.FirstOrDefault(c => c.CodeStation == code.ToString()); //no need to Clone()*/

            //if not exist throw excebustion etc.
        }
        /// <summary>
        /// the function reutrn all the existing stations in the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return from Station in ListStations
                   select Station; //no need to Clone()
        }
        /// <summary>
        /// the function gtes a generate variable & reutrn the wanted accurance form the entire station's list.
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAllStationListWithSelectedFields(Func<DO.Station, object> generate)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return from bs in StationsList
                   select bs;
        }
        /// <summary>
        /// the function gets an accurance of a station ,checks if it already exsist ,if it does so we can update it's details.
        /// else exception
        /// </summary>
        /// <param name="Station"></param>
        public void UpdateStation(DO.Station Station)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            int index = StationsList.FindIndex(i => i.CodeStation == Station.CodeStation);

            if (index == -1)
            {
                throw new DO.BadCodeStationException(Station.CodeStation, "This station's code is not exist : { StationCode}");
            }
            else
            {
                StationsList[index] = Station;
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        /// <summary>
        /// the function get an accurance of a station,cheacks if it already exsist in the list.
        /// if it does - exception. else we can add it to the list.
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(DO.Station station)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            DO.Station station1 = StationsList.Find(i => i.CodeStation == station.CodeStation);

            if (station1 != null)
            {
                throw new DO.BadCodeStationException(station1.CodeStation, "This station's code is already exist: { StationCode}");
            }
            else
            {
                StationsList.Add(station);
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        /// <summary>
        /// the function gets the code of the station & checks if it alreadt exist if it does so we can delete it.
        /// else exception.
        /// </summary>
        /// <param name="stationCode"></param>
        public void DeleteStation(string stationCode)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
       //     List<BusStationLine> BusStationsList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath).Where(s=>s.BusStationNum==stationCode).ToList();
         //   List<FollowingStations> FollowingStationsList = XMLTools.LoadListFromXMLSerializer<FollowingStations>(FollowingStationsPath).Where(s => s.FirstStationCode == stationCode||s.SecondStationCode==stationCode).ToList();
            DO.Station station1 = StationsList.Find(i => i.CodeStation == stationCode);

            if (station1 == null)
            {
                throw new DO.BadCodeStationException(station1.CodeStation, "This station's code is not exist : { StationCode}");
            }
            else
            {
                //foreach(var sl in BusStationsList)
                //{
                //    DeleteBusStationLine(sl.ID,sl.BusStationNum);
                //}
                //foreach (var fs in FollowingStationsList)
                //{
                //    DeleteFollowingStation(fs);
                //}
                StationsList.Remove(station1);
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        #endregion Station

        #region User
        /// <summary>
        /// the fnuction gets all the users from the file & return all it data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUser()
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);

            return (from user in UserRootElem.Elements()
                    select new User()
                    {
                        Me = (Access)Enum.Parse(typeof(Access), user.Element("Me").Value),
                        UserName = (user.Element("UserName").Value),
                        Password = (user.Element("Password").Value)
                    }
                   );
        }
        /// <summary>
        /// the function gets a predicate & return accroding to it the match wanted accurance.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUser(Predicate<User> predicate)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            return from user in UsersList
                   select user;
        }
        /// <summary>
        /// the function gets the user's name & password & checks if it exsist in the system if it does so it return it.
        /// else exception.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public User GetUser(string Name, string pass)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);
            User user1 = (from user in UserRootElem.Elements()
                          where (user.Element("UserName").Value) == Name
                          select new User()
                          {
                              Me = (Access)Enum.Parse(typeof(Access), user.Element("Me").Value),
                              UserName = (user.Element("UserName").Value),
                              Password = (user.Element("Password").Value),
                          }
                        ).FirstOrDefault();

            if (user1 == null)
                throw new DO.BadUserName_PasswordException(Name, pass, $"This user's name/password is not exist: {Name},{pass}");
            return user1;
        }
        /// <summary>
        /// the function gets an accurance of user,checks if it exsist if it does so it throw an exception.
        /// else we can add the user to the user's list with all it data.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);

            XElement user1 = (from User in UserRootElem.Elements()
                              where ((User.Element("UserName").Value) == user.UserName && (User.Element("Password").Value) == user.Password)
                              select User).FirstOrDefault();

            if (user1 != null)
                throw new DO.BadUserName_PasswordException(user.UserName, "The user's name/password is already exsist");

            XElement EserElem = new XElement("user",
                                   new XElement("UserName", user.UserName),
                                   new XElement("Password", user.Password),
                                   new XElement("Me", user.Me));
            UserRootElem.Add(EserElem);


            XMLTools.SaveListToXMLElement(UserRootElem, UserPath);
        }
        /// <summary>
        /// the function gets a generate variable & returns the wanted accurance from the entire list.
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            return from user in UsersList
                   select user;
        }
        /// <summary>
        /// the function gets an accurance of user,checks if it exsist if it does so we can update it's details.
        /// else exception.
        /// </summary>
        /// <param name="User"></param>
        public void UpdateUser(DO.User User)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            int index = UsersList.FindIndex(cis => (cis.UserName == User.UserName && cis.Password == User.Password));


            if (index != -1)
            {
                UsersList[index] = User;
            }
            else
                throw new DO.BadUserName_PasswordException(User.UserName, User.Password, "This user's name/password is not exist");

            XMLTools.SaveListToXMLSerializer(UsersList, UserPath);
        }
        /// <summary>
        /// the function gets the user's name & password ,check if there is a match if it is so we delete the accurance
        /// else exception.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="password"></param>
        public void DeleteUser(string Name, string password)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User u = UsersList.Find(user => user.UserName == Name);

            if (u.Password != null)
            {
                UsersList.Remove(u);
            }
            else
                throw new DO.BadUserName_PasswordException(Name, password, $"This user's name/password is not exist: {Name},{password}");

            XMLTools.SaveListToXMLSerializer(UsersList, UserPath);
        }
        #endregion

        #region DrivingBus
        /// <summary>
        /// the function return all the driving busses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DrivingBus> GetAllDrivingsBusLists()
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            return (from p in DrivingsListRootElem.Elements()
                    select new DrivingBus()
                    {
                        ID = int.Parse(p.Element("Id").Value),
                        LineFrequencyTime = TimeSpan.Parse((p.Element("LineFrequencyTime").Value)),

                        AstimateTimeOut = TimeSpan.ParseExact(p.Element("AstimateTimeOut").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                        ActualTimeOut = TimeSpan.ParseExact(p.Element("ActualTimeOut").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                        LastestStation = (p.Element("LastestStation").Value),
                        TimePassFromLastestStation = TimeSpan.ParseExact(p.Element("TimePassFromLastestStation").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                        AstimateArrive = TimeSpan.ParseExact(p.Element("AstimateArrive").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                    }
                   );
        }
        /// <summary>
        /// the function gets a drivingbus's id & checks if it exsiist if it does so it return all it's fields.
        /// else exception.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public DrivingBus GetDrivingBus(string Num)
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);
            DrivingBus drivingb1 = (from p in DrivingsListRootElem.Elements()
                                    where (p.Element("Id").Value) == Num
                                    select new DrivingBus()
                                    {
                                        ID = int.Parse(p.Element("Id").Value),
                                        LineFrequencyTime = TimeSpan.Parse((p.Element("LineFrequencyTime").Value)),
                                        AstimateTimeOut = TimeSpan.ParseExact(p.Element("AstimateTimeOut").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                        ActualTimeOut = TimeSpan.ParseExact(p.Element("ActualTimeOut").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                        LastestStation = (p.Element("LastestStation").Value),
                                        TimePassFromLastestStation = TimeSpan.ParseExact(p.Element("TimePassFromLastestStation").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                        AstimateArrive = TimeSpan.ParseExact(p.Element("AstimateArrive").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                                    }
                        ).FirstOrDefault();

            if (drivingb1 == null)
                throw new DO.BadBusLicenseNumException(Num, $"wrong bus's id: {Num}");
            return drivingb1;
        }
        /// <summary>
        /// the function get a predicate variable & return the wanted accurance accroding to the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.DrivingBus> GetDrivingsListList(Predicate<DO.DrivingBus> predicate)
        {
            List<DO.DrivingBus> DrivingsList = XMLTools.LoadListFromXMLSerializer<DO.DrivingBus>(DrivingBussbusath);

            return from b in DrivingsList
                   select b;
        }
        /// <summary>
        /// the function gets a generate variable & return the wanted accurance driving bus accroding to the variable.
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetDrivingsListListWithSelectedFields(Func<DrivingBus, object> generate)
        {
            List<DrivingBus> DrivingsList = XMLTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBussbusath);
            return from outgoingline in DrivingsList
                   select outgoingline;
        }
        /// <summary>
        /// the function gets an accurance of drivingbus checks if it already exsist, if it does so we cant add it & exception.
        /// else we can add it with all it's fields.
        /// </summary>
        /// <param name="OutGoingLine"></param>
        public void AddDrivingsList(DrivingBus OutGoingLine)
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement outgoingline1 = (from outgoingline in DrivingsListRootElem.Elements()
                                      where (outgoingline.Element("user's name").Value) == OutGoingLine.ID.ToString()
                                      select outgoingline).FirstOrDefault();

            if (outgoingline1 != null)
                throw new DO.BadBusLicenseNumException(OutGoingLine.LicenseNum, "wrong outgoingline license num");

            XElement UserElem = new XElement("drivingbus",
                                   new XElement("ID", OutGoingLine.ID.ToString()));

            DrivingsListRootElem.Add(outgoingline1);

            XMLTools.SaveListToXMLElement(DrivingsListRootElem, DrivingBussbusath);
        }
        /// <summary>
        /// the function  gets an accurance of driving bus ,checks if it already exsist if it does so we can update it's fiels
        /// else exception.
        /// </summary>
        /// <param name="OutGoingLine"></param>
        public void UpdateDrivingBus(DrivingBus OutGoingLine)
        {
            XElement DrivingBRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement bus = (from bus1 in DrivingBRootElem.Elements()
                            where (bus1.Element("LicenseNum").Value) == OutGoingLine.LicenseNum
                            select bus1).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("ID").Value = OutGoingLine.ID.ToString();
                bus.Element("AstimateTimeOut").Value = OutGoingLine.ActualTimeOut.ToString();
                bus.Element("LastestStation").Value = OutGoingLine.LastestStation;
                bus.Element("TimePassFromLastestStation").Value = OutGoingLine.TimePassFromLastestStation.ToString();
                bus.Element("AstimateArrive").Value = OutGoingLine.AstimateArrive.ToString();
                bus.Element("LineFrequencyTime").Value = OutGoingLine.LineFrequencyTime.ToString();

                //bus.Element("LicenseNum").Value = OutGoingLine.LicenseNum.ToString();

                XMLTools.SaveListToXMLElement(DrivingBRootElem, DrivingBussbusath);
            }
            else
                throw new DO.BadBusLicenseNumException(OutGoingLine.LicenseNum, $"bad Bus's LicenseNum: {OutGoingLine.LicenseNum}");
        }
        /// <summary>
        /// the function gets the drivingbus num ,checks if it exsist if it does so we can dalete it.
        /// else exception.
        /// </summary>
        /// <param name="Num"></param>
        public void DeleteDrivingBus(string Num)
        {
            XElement DrivingBRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement bus1 = (from bus in DrivingBRootElem.Elements()
                             where (bus.Element("LicenseNum").Value) == Num
                             select bus).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(DrivingBRootElem, DrivingBussbusath);
            }
            else
                throw new DO.BadBusLicenseNumException(Num, $"bad Bus's LicenseNum: {Num}");
        }
        #endregion
        
        /*#region Accident
        /// <summary>
        /// the function gets the licensenum of the bus that did the accident 
        /// & return if the license num exsist the other accident's data.
        /// else exception.
        /// </summary>
        /// <param name="LicenseNum"></param>
        /// <returns></returns>
        public DO.Accident GetAccident(string Num)
        {
            List<Accident> accident = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            DO.Accident stu = accident.Find(a => a.LicenseNum == Num);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumException(Num, $"bad accident num: {Num}");
        }
        /// <summary>
        /// the function return all the accident of all  the list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Accident> GetAllAccidentsList()
        {
            List<Accident> accidents = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            return from accident in accidents
                   select accident; //no need to Clone()
        }
        /// <summary>
        /// the function gets an accurance of accident ,check if it already exsist.
        /// if it does so we can add it , else yes. 
        /// we check accroding to the bus's licensenum & the date & num of the accident that they are all have the same accurance.
        /// </summary>
        /// <param name="Accident"></param>
        public void AddAccident(Accident Accident)
        {
            List<Accident> Accidents = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);
            Accident.AccidentNum = Accidents[Accidents.Count - 1].AccidentNum;
            Accident.AccidentNum++;
            if (Accidents.FirstOrDefault(cis => (cis.LicenseNum == Accident.LicenseNum && cis.AccidentDate == Accident.AccidentDate && cis.AccidentNum == Accident.AccidentNum)) != null)
                throw new DO.BadBusStationLineCodeException(Accident.LicenseNum, Accident.AccidentDate.ToString(), "accident details are already exist");
            Accidents.Add(Accident);

            XMLTools.SaveListToXMLSerializer(Accidents, AccidentPath);
        }
        /// <summary>
        /// the function gets an accurance of accident & checks if it already exsist ,if it is it deleted.
        /// else exception.
        /// </summary>
        /// <param name="Accidentnum"></param>
        public void DeleteAccident(int Accidentnum)
        {
            List<Accident> AccidentsList = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            DO.Accident a = AccidentsList.Find(accident => accident.AccidentNum == Accidentnum);

            if (a.AccidentNum != 0)
            {
                AccidentsList.Remove(a);
            }
            else
                throw new DO.BadLicenseNumException(Accidentnum.ToString(), $"Wrong accident's num : {Accidentnum}");

            XMLTools.SaveListToXMLSerializer(AccidentsList, AccidentPath);
        }
        #endregion
        */
        #region FollowingStations
        /// <summary>
        /// the function gets two codes of busstationline ,checks if they are match to one of the items in the followingstation file  -that ine of them match the first station & the other the seconf.
        /// if it does so we return the match accurance in the followingstation file.
        /// else exception.
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <returns></returns>
        public DO.FollowingStations GetFollowingStation(string code1, string code2)
        {
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);
            FollowingStations s = (from station in FollowingSElem.Elements()
                                   where ((station.Element("FirstStationCode").Value == code1 && (station.Element("SecondStationCode").Value)
                                   == code2)||( station.Element("FirstStationCode").Value) == code2 && (station.Element("SecondStationCode").Value) == code1)
                                   select new FollowingStations()
                                   {
                                       FirstStationCode = (station.Element("FirstStationCode").Value),
                                       SecondStationCode = (station.Element("SecondStationCode").Value),
                                       Distance = double.Parse(station.Element("Distance").Value),
                                       AverageDrivingTime = TimeSpan.ParseExact(station.Element("AverageDrivingTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                   }
                        ).FirstOrDefault();
               
             if (s == null)
             {
                 throw new DO.BadBusStationLineCodeException(code1 + code2, $"wrong stations's codes: {code1 + code2}");
             }
            return s;
        }
        /// <summary>
        /// the function gets a predicate variable of followingstation type & returns the wanted accurance accroding to the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.FollowingStations> GetAllFollowingStationss(Predicate<DO.FollowingStations> predicate)
        {
            List<FollowingStations> FollowingStationss = XMLTools.LoadListFromXMLSerializer<FollowingStations>(FollowingStationsPath);

            return from fs in FollowingStationss
                   select fs;
        }
        /// <summary>
        /// the function returns all the exists followingstations in the mxl file.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.FollowingStations> GetAllFollowingStationss()
        {
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);

            return (from fs in FollowingSElem.Elements()
                    select new FollowingStations()
                    {
                        FirstStationCode = (fs.Element("FirstStationCode").Value),
                        SecondStationCode = (fs.Element("SecondStationCode").Value),
                        Distance = double.Parse(fs.Element("Distance").Value),
                        AverageDrivingTime = TimeSpan.ParseExact(fs.Element("AverageDrivingTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                        //WalkingTime = TimeSpan.Parse(fs.Element("DrivingTimeBetween").Value),
                    }
                   );
        }
        /// <summary>
        /// the function gets a generate variable & returns the wanted accurance accroding to the variable.
        /// </summary>
        /// <param name="generate"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAllFollowingStationsListWithSelectedFields(Func<DO.FollowingStations, object> generate)
        {
            List<FollowingStations> FollowingStationss = XMLTools.LoadListFromXMLSerializer<FollowingStations>(FollowingStationsPath);
            return from fs in FollowingStationss
                   select fs;
        }
        /// <summary>
        /// the function gets a followingstation accurance ,check if it already exsist, the check is by checking the first & last stations codes straight & backwards 
        /// if it does so we cant add the accurance.
        /// else yes.
        /// </summary>
        /// <param name="FollowingStations"></param>
        public void AddFollowingStations(DO.FollowingStations FollowingStations)
        {
            //XMLTools.SaveListToXMLSerializer(followingStations, FollowingStationsPath);
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);

            XElement followings = (from fs in FollowingSElem.Elements()
                                   where (fs.Element("FirstStationCode").Value) == FollowingStations.FirstStationCode && fs.Element("SecondStationCode").Value == FollowingStations.SecondStationCode || (fs.Element("SecondStationCode").Value) == FollowingStations.FirstStationCode && fs.Element("FirstStationCode").Value == FollowingStations.SecondStationCode
                                   select fs).FirstOrDefault();

            if (followings == null)
            {
                XElement fsElem = new XElement("FollowingStations",
                                       new XElement("FirstStationCode", FollowingStations.FirstStationCode.ToString()),
                                       new XElement("SecondStationCode", FollowingStations.SecondStationCode.ToString()),
                                       //new XElement("FirstStationName", FollowingStations.FirstStationName.ToString()),
                                       // new XElement("SecondStationName", FollowingStations.SecondStationName.ToString()),
                                       new XElement("Distance", FollowingStations.Distance.ToString()),
                                       new XElement("AverageDrivingTime", FollowingStations.AverageDrivingTime.ToString()));
                //new XElement("WalkingTime", FollowingStations.WalkingTime.ToString()));

                FollowingSElem.Add(fsElem);

                XMLTools.SaveListToXMLElement(FollowingSElem, FollowingStationsPath);
            }
        }
        /// <summary>
        /// the function gets an accurance of followingstation type ,check if it already exsist in the followingstations file.
        /// if it does we can delete it.
        /// else exception.
        /// </summary>
        /// <param name="followingstation"></param>
        public void DeleteFollowingStation(DO.FollowingStations followingstation)
        {
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);
            XElement fsElem = (from fs in FollowingSElem.Elements()
                               where (fs.Element("LicenseNum").Value) == followingstation.ToString()
                               select fs).FirstOrDefault();

            if (fsElem != null)
            {
                fsElem.Remove();
                XMLTools.SaveListToXMLElement(FollowingSElem, FollowingStationsPath);
            }
            else
                throw new DO.BadBusLineException(followingstation.FirstStationCode, followingstation.SecondStationCode, $"wrong stations's codes: {followingstation.FirstStationCode},{followingstation.SecondStationCode}");
        }
        /// <summary>
        /// the function gets a code & check if there is a mtch in the followingstations xml file ,if it match to the first/second stations
        /// if it does so we earse it from all the file.
        /// else exception.
        /// </summary>
        /// <param name="code"></param>
        public void DeleteFollowingStation(string code)
        {
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);
            FollowingSElem.Elements().ToList().RemoveAll(fs => fs.Element("FirstStationCode").Value 
            == code || fs.Element("SecondStationCode").Value == code );
        }
        /// <summary>
        /// the function gets a followingstation accurance if this accurance exsist so we can update it;s fields. the check is by over on all the file checking if there is a mtch of the first/last station's codes.
        /// else exception.
        /// </summary>
        /// <param name="FollowingStations"></param>
        public void UpdateFollowingStations(DO.FollowingStations FollowingStations)
        {
            XElement FollowingSElem = XMLTools.LoadListFromXMLElement(FollowingStationsPath);

            XElement fsElem = (from fs in FollowingSElem.Elements()
                               where (fs.Element("FirstStationCode").Value) == FollowingStations.FirstStationCode && fs.Element("SecondStationCode").Value == FollowingStations.SecondStationCode || (fs.Element("SecondStationCode").Value) == FollowingStations.FirstStationCode && fs.Element("FirstStationCode").Value == FollowingStations.SecondStationCode
                               select fs).FirstOrDefault();

            if (fsElem != null)
            {
                fsElem.Element("FirstStationCode").Value = FollowingStations.FirstStationCode.ToString();
                fsElem.Element("SecondStationCode").Value = FollowingStations.SecondStationCode.ToString();
                // fsElem.Element("FirstStationName").Value = FollowingStations.FirstStationName.ToString();
                // fsElem.Element("SecondStationName").Value = FollowingStations.SecondStationName.ToString();
                fsElem.Element("Distance").Value = FollowingStations.Distance.ToString();
                fsElem.Element("AverageDrivingTime").Value = FollowingStations.AverageDrivingTime.ToString();
                //fsElem.Element("WalkingTime").Value = FollowingStations.WalkingTime.ToString();

                XMLTools.SaveListToXMLElement(FollowingSElem, FollowingStationsPath);
            }
            else
                throw new DO.BadBusLineException(FollowingStations.FirstStationCode, FollowingStations.SecondStationCode, $"wrong stations's codes: {FollowingStations.FirstStationCode},{FollowingStations.SecondStationCode}");
        }
        #endregion

        #region LineExit
        string LineExitXml = @"LineExit.xml";
        /// <summary>
        /// the function gets an outgoingline accurance ,checks if it already exsist, if it does so we cant add the wanted one & throw an exception.
        /// else yes.
        /// the checking is by the busline's id & it start time.
        /// </summary>
        /// <param name="lineExit"></param>
        public void AddLineExit(OutGoingLine lineExit)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("Id").Value == lineExit.Id.ToString() && p.Element("LineStartTime").Value == lineExit.LineStartTime.ToString()
                                  select p).FirstOrDefault();
            if (lineExit1 != null)
            {
                throw new BadOutGoingLineException(lineExit.Id, lineExit.LineStartTime, "the Exit alrdy exist in the list in the same time");
            }

            XElement lineExit2 = new XElement("OutGoingLine", new XElement("Id", lineExit.Id),
                                   new XElement("LineStartTime", lineExit.LineStartTime.ToString()),
                                   new XElement("LineFinishTime", lineExit.LineFinishTime.ToString()),
                                   new XElement("LineFrequencyTime", lineExit.LineFrequencyTime.ToString()),
                                   new XElement("LineFrequency", lineExit.LineFrequency));

            element.Add(lineExit2);
            XMLTools.SaveListToXMLElement(element, LineExitXml);

            //List<LineExit> lineExits = XMLTools.LoadListFromXMLSerializer<LineExit>(LineExitXml);
            //if (lineExits.Exists(lineExit1 => lineExit1.BusLineID1 == lineExit.BusLineID1 && lineExit1.LineStartTime == lineExit.LineStartTime))
            //{
            //    throw new ExceptionLineExit(lineExit.BusLineID1, lineExit.LineStartTime, "the LineExit alrdy exist in the list in the same time");
            //}
            //else
            //{
            //    lineExits.Add(lineExit);
            //    XMLTools.SaveListToXMLSerializer(lineExits, LineExitXml);
            //}
        }
        /// <summary>
        /// th efunction gets an accurance of outgoing line ,check if it already exsist.
        /// if it is so we can delete it. else exception.
        /// the check is by the busline's id & it's start time.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="StartTime"></param>
        public void DeleteLineExit(int lineNumber, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("Id").Value == lineNumber.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                  select p).FirstOrDefault();
            if (lineExit1 == null)
            {
                throw new DO.BadOutGoingLineException(lineNumber, StartTime, "the Exit not found!!!");
            }
            lineExit1.Remove();
            XMLTools.SaveListToXMLElement(element, LineExitXml);
        }
        /// <summary>
        /// the function gets an accurance of outgoingline ,checks if it already exsist in the outgoingline xml file.
        /// if it is we can update it's fields. else exception.
        /// the chack id by the busline's id & it's start time.
        /// </summary>
        /// <param name="lineExit"></param>
        public void UpdatingLineExit(OutGoingLine lineExit)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            XElement lineExit1 = (from p in element.Elements()
                                  where p.Element("Id").Value == lineExit.Id.ToString() && p.Element("LineStartTime").Value == lineExit.LineStartTime.ToString()
                                  select p).FirstOrDefault();

            if (lineExit1 != null)
            {
                lineExit1.Element("Id").Value = lineExit.Id.ToString();
                lineExit1.Element("LineStartTime").Value = lineExit.LineStartTime.ToString();
                lineExit1.Element("LineFinishTime").Value = lineExit.LineFinishTime.ToString();
                lineExit1.Element("LineFrequency").Value = lineExit.LineFrequency.ToString();
                lineExit1.Element("LineFrequencyTime").Value = lineExit.LineFrequencyTime.ToString();
                XMLTools.SaveListToXMLElement(element, LineExitXml);
            }

            else
            {
                throw new BadOutGoingLineException(lineExit.Id, lineExit.LineStartTime, "The Exit not exist in the compny");

            }
        }
        /// <summary>
        /// the function gets the busline's num & it start time if there is a match in the xml file so we return the wanted outgoingline with all it's fields.
        /// else exception.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public OutGoingLine ReturnLineExit(int lineNumber, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            OutGoingLine lineExit1 = (from p in element.Elements()
                                      where p.Element("Id").Value == lineNumber.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                      select new OutGoingLine()
                                      {
                                          Id = int.Parse(p.Element("Id").Value),
                                          LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                                      }).FirstOrDefault();
            return lineExit1 ?? throw new BadOutGoingLineException(lineNumber, StartTime, "the Exit not exist in the list");
        }
        /// <summary>
        /// the function gets the line num & it start time if there is a match in the outgoingline's xml file with both the variables so we return thr match accurance.
        /// else exception.
        /// </summary>
        /// <param name="numberLine"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public OutGoingLine OneLineExitFromList(int numberLine, TimeSpan StartTime)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            OutGoingLine lineExit1 = (from p in element.Elements()
                                      where p.Element("Id").Value == numberLine.ToString() && p.Element("LineStartTime").Value == StartTime.ToString()
                                      select new OutGoingLine()
                                      {
                                          Id = int.Parse(p.Element("Id").Value),
                                          LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                                      }).FirstOrDefault();
            return lineExit1 ?? throw new BadOutGoingLineException(numberLine, StartTime, "the Exit not exist in the list");
        }
        /// <summary>
        /// the function gets the line's num & reutnr all it's exits by it's num.
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public IEnumerable<OutGoingLine> LineExitList(int numberLine)
        {
            XElement element = XMLTools.LoadListFromXMLElement(LineExitXml);
            return from p in element.Elements()
                   where p.Element("Id").Value == numberLine.ToString()
                   select new OutGoingLine()
                   {
                       Id = int.Parse(p.Element("Id").Value),
                       LineStartTime = TimeSpan.ParseExact(p.Element("LineStartTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFinishTime = TimeSpan.ParseExact(p.Element("LineFinishTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFrequencyTime = TimeSpan.ParseExact(p.Element("LineFrequencyTime").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                       LineFrequency = int.Parse(p.Element("LineFrequency").Value)
                   }; 
        }
        #endregion
    }
}
 