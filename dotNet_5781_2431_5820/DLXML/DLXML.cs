﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static DLXML Instance { get => instance; }// The busublic Instance busrobusty to use
        #endregion

        #region DS XML Files
        
        string BususPath = @"BusesXml.xml"; //XElement
        string BusLinesbusPath = @"BusLinesXml.xml"; //XMLSerializer
        string StationsPath = @"StationsXml.xml"; //XMLSerializer
       string DrivingBussbusath = @"DrivingBusesXml.xml"; //XMLSerializer
        //string OutGoingBusesPath = @"OutGoingBusesXml.xml"; //XMLSerializer
        string BusStationLinePath = @"BusStationLineXml.xml"; //XMLSerializer
        string UserPath = @"UserXml.xml"; //XElement
        string AccidentPath = @"AccidentXml.xml"; //XMLSerializer
        //string UserDrivePath = @"UserLineXml.xml"; //XMLSerializer

        #endregion

        #region Bus
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
                            // Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                            Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                        }
                        ).FirstOrDefault();

            if (bus1 == null)
                throw new DO.BadBusLicenseNumException(LicenseNum.ToString(), $"bad Bus License Num: {LicenseNum}");
            return bus1;
        }
        public IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate)
        {
            List<Bus> BussesList = XMLTools.LoadListFromXMLSerializer<Bus>(BususPath);

            return from bs in BussesList
                   select bs;
        }
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
                        // Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                        Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                    }
                   );
        }
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
                       //Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                       Firm = (Firm)Enum.Parse(typeof(Firm), bus.Element("Firm").Value)
                   }
                   where predicate(bus1)
                   select bus1;
        }
        public IEnumerable<object> GetAllBusListWithSelectedFields(Func<DO.Bus, object> generate)
        {
            List<Bus> BussesList = XMLTools.LoadListFromXMLSerializer<Bus>(BususPath);
            return from bs in BussesList
                   select bs;
        }
        public void AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            XElement bus1 = (from bus in BussRootElem.Elements()
                             where (bus.Element("LicenseNum").Value) == Bus.LicenseNum
                             select bus).FirstOrDefault();

            if (bus1 != null)
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, "Dubuslicate Bus LicenseNum");

            XElement BusElem = new XElement("Bus",
                                   new XElement("LicenseNum", Bus.LicenseNum.ToString()),
                                   new XElement("foul", Bus.foul.ToString()),
                                   // new XElement("Status", Bus.Status.ToString()),
                                   new XElement("KM", Bus.KM.ToString()),
                                   new XElement("Firm", Bus.Firm.ToString()),
                                   new XElement("LicenseDate", Bus.LicenseDate));

            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, BususPath);
        }
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
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus's LicenseNum: {LicenseNum}");
        }
        public void UpdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BususPath);

            XElement bus = (from bus1 in BussRootElem.Elements()
                            where (bus1.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus1).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("LicenseNum").Value = Bus.LicenseNum.ToString();
                bus.Element("Foul").Value = Bus.foul.ToString();
                bus.Element("KM").Value = Bus.KM.ToString();
                bus.Element("LicenseDate").Value = Bus.LicenseDate.ToString();
                //bus.Element("Status").Value = Bus.Status.ToString();
                bus.Element("Firm").Value = Bus.Firm.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, BususPath);
            }
            else
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, $"bad Bus's LicenseNum: {Bus.LicenseNum}");
        }

        #endregion Bus

        #region busline
        public DO.BusLine GetBusLine(int LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.BusNum == LicenseNum);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadBusLicenseNumException(LicenseNum.ToString(), $"bad BusLine LicenseNum: {LicenseNum}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            if (ListBusLines.FirstOrDefault(s => s.BusNum == BusLine.BusNum) != null)
                throw new DO.BadBusLicenseNumException(BusLine.BusNum.ToString(), "Dubuslicate BusLine LicenseNum");

            ListBusLines.Add(BusLine); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);

        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            return from BusLine in ListBusLines
                   select BusLine; //no need to Clone()
        }
        public IEnumerable<object> GetBusLineFields(Func<int, DO.BusLine, object> generate)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            return from busline in ListBusLines
                   select generate(busline.BusNum, GetBusLine(busline.BusNum));
        }
        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object, object> generate)
        {
            List<BusLine> BusLinesList = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);
            return from bs in BusLinesList
                   select bs;
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.BusNum == BusLine.BusNum);
            if (stu != null)
            {
                ListBusLines.Remove(stu);
                ListBusLines.Add(BusLine); //no nee to Clone()
            }
            else
                throw new DO.BadBusLicenseNumException(BusLine.BusNum.ToString(), $"bad BusLine LicenseNum: {BusLine.BusNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);
        }
        public void UpdateBusLine(int LicenseNum, Action<DO.BusLine> update)
        {
            //throw new NotImplementedExcebustion();
        }
        public void DeleteBusLine(string LicenseNum)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(BusLinesbusPath);

            DO.BusLine stu = ListBusLines.Find(bus => bus.BusNum.ToString() == LicenseNum);

            if (stu != null)
            {
                ListBusLines.Remove(stu);
            }
            else
                throw new DO.BadBusLicenseNumException(LicenseNum.ToString(), $"bad BusLine LicenseNum: {LicenseNum}");

            XMLTools.SaveListToXMLSerializer(ListBusLines, BusLinesbusPath);
        }
        #endregion BusLine

        #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetBusLinesInStationList(Predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            return from sic in busStationLines
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        public void AddBusStationLine(BusStationLine busStationLine)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            if (busStationLines.FirstOrDefault(cis => (cis.BusStationNum == busStationLine.BusStationNum.ToString() && cis.ID == busStationLine.ID.ToString())) != null)
                throw new DO.BadBusStationLineCodeException(busStationLine.BusStationNum, busStationLine.ID.ToString(), "Bus LicenseNum is already registered to Station LicenseNum");
            busStationLines.Add(busStationLine);

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);
        }
        public void UpdateBusStationLine(BusStationLine busStationLine)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            int index = busStationLines.FindIndex(cis => (cis.BusStationNum == busStationLine.BusStationNum && cis.ID == busStationLine.ID));


            if (index != -1)
            {
                busStationLines[index] = busStationLine;
            }
            else
                throw new DO.BadBusStationLineCodeException(busStationLine.BusStationNum, busStationLine.ID, "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);
        }
        public void DeleteBusStationLine(int LineNum, int StationCode)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            DO.BusStationLine sic = busStationLines.Find(cis => (cis.BusStationNum == StationCode.ToString() && cis.ID == LineNum.ToString()));

            if (sic != null)
            {
                busStationLines.Remove(sic);
            }
            else
                throw new DO.BadBusStationLineCodeException(StationCode.ToString(), LineNum.ToString(), "Bus LicenseNum is NOT registered to Station LicenseNum");

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);

        }
        public void DeleteBusLineFromAllStations(int busLinenum)
        {
            List<BusStationLine> busStationLines = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            busStationLines.RemoveAll(bus => bus.ID == busLinenum.ToString());

            XMLTools.SaveListToXMLSerializer(busStationLines, BusStationLinePath);

        }
        public IEnumerable<DO.BusStationLine> GetLineStationsListOfALine(string lineId)//returns a "line stations" list of the wanted line
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);

            return from bsl in BusStationsLineList
                   select bsl;
        }
        public IEnumerable<DO.BusStationLine> GetAllBusStationLines(string busstationline)
        {//returns all members in list
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList
                   select bs;
        }
        public IEnumerable<DO.BusStationLine> GetBusStationLineList(Predicate<DO.BusStationLine> predicate)
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList
                   select bs;
        }
        public DO.BusStationLine GetBusStationLine(string Id)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BusStationLinePath);
            BusStationLine bus1 = (from busstationline in BussRootElem.Elements()
                                   where (busstationline.Element("ID").Value) == Id
                                   select new BusStationLine()
                                   {
                                       BusStationNum = (busstationline.Element("BusStationNum").Value),
                                       IndexInLine = int.Parse((busstationline.Element("IndexInLine").Value)),
                                       NumOfPassingLines = int.Parse((busstationline.Element("NumOfPassingLines").Value)),
                                   }
                        ).FirstOrDefault();

            if (bus1 == null)
                throw new DO.BadBusStationLineCodeException(bus1.ID, $"bad Bus station line License Num: {Id}");
            return bus1;
        }
        public void DeleteBusStationLine(string stationNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BusStationLinePath);

            XElement bus1 = (from busstationline in BussRootElem.Elements()
                             where (busstationline.Element("BusStationNum").Value) == stationNum
                             select busstationline).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, BusStationLinePath);
            }
            else
                throw new DO.BadBusStationLineCodeException(stationNum, $"bad Bus's LicenseNum: {stationNum}");
        }
        public void DeleteBusStationLineFromAllStations(string StationID)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(BusStationLinePath);
            BussRootElem.RemoveAll();
        }
        public IEnumerable<object> GetBusStationsLineListWithSelectedFields(Func<DO.BusStationLine, object> generate)
        {
            List<BusStationLine> BusStationsLineList = XMLTools.LoadListFromXMLSerializer<BusStationLine>(BusStationLinePath);
            return from bs in BusStationsLineList
                   select bs;
        }

        #endregion BusStationLine

        #region Station
        public IEnumerable<DO.Station> GetAllStations(Predicate<DO.Station> predicate)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return from bs in StationsList
                   select bs;
        }
        public DO.Station GetStation(string code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return ListStations.FirstOrDefault(c => c.CodeStation == code.ToString()); //no need to Clone()

            //if not exist throw excebustion etc.
        }
        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);

            return from Station in ListStations
                   select Station; //no need to Clone()
        }
        public IEnumerable<object> GetAllStationListWithSelectedFields(Func<DO.Station, object> generate)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            return from bs in StationsList
                   select bs;
        }
        public void UpdateStation(DO.Station Station)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            int index = StationsList.FindIndex(i => i.CodeStation == Station.CodeStation);

            if (index == -1)
            {
                throw new DO.BadCodeStationException(Station.CodeStation, "wrong station code : { StationCode}");
            }
            else
            {
                StationsList[index] = Station;
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        public void AddStation(DO.Station station)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            DO.Station station1 = StationsList.Find(i => i.CodeStation == station.CodeStation);

            if (station1 != null)
            {
                throw new DO.BadCodeStationException(station1.CodeStation, "wrong station code : { StationCode}");
            }
            else
            {
                StationsList.Add(station);
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        public void DeleteStation(string stationCode)
        {
            List<Station> StationsList = XMLTools.LoadListFromXMLSerializer<Station>(StationsPath);
            DO.Station station1 = StationsList.Find(i => i.CodeStation == stationCode);

            if (station1 != null)
            {
                throw new DO.BadCodeStationException(station1.CodeStation, "wrong station code : { StationCode}");
            }
            else
            {

                StationsList.Remove(station1);
            }
            XMLTools.SaveListToXMLSerializer(StationsList, StationsPath);
        }
        #endregion Station

        /* #region DrivingBus
         public IEnumerable<DO.OutGoingLine> GetDrivingBussInStationList(Predicate<DO.OutGoingLine> predicate)
         {
           List<OutGoingLine> outgoingLines = XMLTools.LoadListFromXMLSerializer<OutGoingLine>(OutGoingBusesPath);

           return from sic in outgoingLines
              where predicate(sic)
                select sic; //no need to Clone()
          }

         #endregion*/

        #region User
        public IEnumerable<DO.User> GetAllUser()
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);

            return (from user in UserRootElem.Elements()
                    select new User()
                    {
                        UserName = (user.Element("Name").Value),
                        Password = (user.Element("Password").Value),
                    }
                   );
        }
        public IEnumerable<DO.User> GetAllUser(Predicate<User> predicate)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            return from user in UsersList
                   select user;
        }
        public User GetUser(string Name, string pass)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);
            User user1 = (from user in UserRootElem.Elements()
                          where (user.Element("Name").Value) == Name
                          select new User()
                          {
                              UserName = (user.Element("name").Value),
                              Password = (user.Element("password").Value),
                          }
                        ).FirstOrDefault();

            if (user1 == null)
                throw new DO.BadUserName_PasswordException(Name, pass, $"wrong user's name or password: {Name},{pass}");
            return user1;
        }
        public void AddUser(DO.User user)
        {
            XElement UserRootElem = XMLTools.LoadListFromXMLElement(UserPath);

            XElement user1 = (from user in UserRootElem.Elements()
                              where (user.Element("user's name").Value) == user.UserName
                              select user).FirstOrDefault();

            if (user1 != null)
                throw new DO.BadUserName_PasswordException(user.UserName, "wrong user's name");

            XElement UserRootElem = new XElement("user",
                                   new XElement("name", user.UserName),
                                   new XElement("password", user.Password));         
            UserRootElem.Add(UserRootElem);

            XMLTools.SaveListToXMLElement(UserRootElem, UserPath);
        }
        public IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);
            return from user in UsersList
                   select user;
        }
        public void UpdateUser(DO.User User)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            int index = UsersList.FindIndex(cis => (cis.UserName == User.UserName && cis.Password == User.Password));


            if (index != -1)
            {
                UsersList[index] = User;
            }
            else
                throw new DO.BadUserName_PasswordException(User.UserName, User.Password, "wrong user's details");

            XMLTools.SaveListToXMLSerializer(UsersList, UserPath);
        }
        public void DeleteUser(string Name, string password)
        {
            List<User> UsersList = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User u = UsersList.Find(user => user.UserName == Name);

            if (u.Password != null)
            {
                UsersList.Remove(u);
            }
            else
                throw new DO.BadUserName_PasswordException(Name, password, $"bad BusLine LicenseNum: {Name},{password}");

            XMLTools.SaveListToXMLSerializer(UsersList, UserPath);
        }
        #endregion

        #region DrivingBus
        public IEnumerable<DrivingBus> GetAllDrivingsBusLists()
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            return (from outgoingline in DrivingsListRootElem.Elements()
                    select new DrivingBus()
                    {
                        ID = int.Parse(outgoingline.Element("Id").Value),
                        LicenseNum = ((outgoingline.Element("LicenseNum").Value)),
                        AstimateTimeOut = TimeSpan.Parse((outgoingline.Element("AstimateTimeOut").Value)),
                        ActualTimeOut = TimeSpan.Parse((outgoingline.Element("ActualTimeOut").Value)),
                        LastestStation = (outgoingline.Element("LastestStation").Value),
                        TimePassFromLastestStation = TimeSpan.Parse((outgoingline.Element("TimePassFromLastestStation").Value)),
                        AstimateArrive = TimeSpan.Parse((outgoingline.Element("AstimateArrive").Value)),
                        ActualTimeOut = TimeSpan.Parse((outgoingline.Element("ActualTimeOut").Value)),
                    }
                   );
        }
        public DrivingBus GetDrivingBus(string Num)
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);
            DrivingBus drivingb1 = (from drivingb in DrivingsListRootElem.Elements()
                                        where (outgoingline.Element("LicensNum").Value) == Num
                                        select new DrivingBus()
                                        {
                                            ID = int.Parse((drivingb.Element("Id").Value)),
                                            LicenseNum = ((drivingb.Element("LicenseNum").Value)),
                                            AstimateTimeOut = TimeSpan.Parse((drivingb.Element("AstimateTimeOut").Value)),
                                            ActualTimeOut = TimeSpan.Parse((drivingb.Element("ActualTimeOut").Value)),
                                            LastestStation = (drivingb.Element("LastestStation").Value),
                                            TimePassFromLastestStation = TimeSpan.Parse((drivingb.Element("TimePassFromLastestStation").Value)),
                                            AstimateArrive = TimeSpan.Parse((outgoingline.Element("AstimateArrive").Value)),
                                            ActualTimeOut = TimeSpan.Parse((drivingb.Element("ActualTimeOut").Value)),
                                            
                                        }
                        ).FirstOrDefault();

            if (drivingb1 == null)
                throw new DO.BadBusLicenseNumException(Num, $"wrong user's name or password: {Num}");
            return drivingb1;
        }
        public IEnumerable<DO.DrivingBus> GetDrivingsListList(Predicate<DO.DrivingBus> predicate)
        {
            List<DO.DrivingBus> DrivingsList = XMLTools.LoadListFromXMLSerializer<DO.DrivingBus>(DrivingBussbusath);

            return from b in DrivingsList
                   select b;
        }
        public IEnumerable<object> GetDrivingsListListWithSelectedFields(Func<DrivingBus, object> generate)
        {
            List<DrivingBus> DrivingsList = XMLTools.LoadListFromXMLSerializer<DrivingBus>(DrivingBussbusath);
            return from outgoingline in DrivingsList
                   select outgoingline;
        }
        public void AddDrivingsList(DrivingBus OutGoingLine)
        {
            XElement DrivingsListRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement outgoingline1 = (from outgoingline in DrivingsListRootElem.Elements()
                                      where (outgoingline.Element("user's name").Value) == OutGoingLine.ID
                                      select outgoingline).FirstOrDefault();

            if (outgoingline1 != null)
                throw new DO.BadBusLicenseNumException(OutGoingLine.LicenseNum, "wrong outgoingline license num");

            XElement UserElem = new XElement("drivingbus",
                                   new XElement("ID", OutGoingLine.ID.ToString()));

            DrivingsListRootElem.Add(OutGoingLineRootElem);

            XMLTools.SaveListToXMLElement(DrivingsListRootElem, DrivingBussbusath);
        }
        public void UpdateDrivingBus(DrivingBus OutGoingLine)
        {
            XElement DrivingBRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement bus = (from bus1 in DrivingBRootElem.Elements()
                            where (bus1.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus1).FirstOrDefault();

            if (bus != null)
            {
                bus.Element("ID").Value = OutGoingLine.ID;
                bus.Element("AstimateTimeOut") = OutGoingLine.ActualTimeOut;
                bus.Element("ActualTimeOut") = OutGoingLine.ActualTimeOut;
                bus.Element("LastestStation") = OutGoingLine.LastestStation;
                bus.Element("TimePassFromLastestStation") = OutGoingLine.TimePassFromLastestStation;
                bus.Element("AstimateArrive") = OutGoingLine.AstimateArrive;
                bus.Element("LicenseNum").Value = OutGoingLine.LicenseNum.ToString();

                XMLTools.SaveListToXMLElement(DrivingBRootElem, DrivingBussbusath);
            }
            else
                throw new DO.BadBusLicenseNumException(Bus.LicenseNum, $"bad Bus's LicenseNum: {Bus.LicenseNum}");
        }
        public void DeleteDrivingBus(string Num)
        {
            XElement DrivingBRootElem = XMLTools.LoadListFromXMLElement(DrivingBussbusath);

            XElement bus1 = (from bus in DrivingBRootElem.Elements()
                             where (bus.Element("LicenseNum").Value) == LicenseNum
                             select bus).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(DrivingBRootElem, DrivingBussbusath);
            }
            else
                throw new DO.BadBusLicenseNumException(LicenseNum, $"bad Bus's LicenseNum: {LicenseNum}");
        }
        #endregion

        #region Accident
        public Accident GetAccident(int Accidentnum)
        {
            XElement AccidentRootElem = XMLTools.LoadListFromXMLElement(AccidentPath);
            Accident accident1 = (from accident in AccidentRootElem.Elements()
                                  where (accident.Element("num").Value) == Accidentnum.ToString()
                                  select new Accident()
                                  {
                                      LicenseNum = (accident.Element("LicenseNum").Value),
                                      AccidentDate = DateTime.Parse((accident.Element("date").Value)),
                                      AccidentNum = int.Parse((accident.Element("num").Value))
                                  }
                        ).FirstOrDefault();

            if (accident1 == null)
                throw new DO.BadLicenseNumException(Accidentnum.ToString(), $"wrong user's name or password: {Accidentnum}");
            return accident1;
        }

        public IEnumerable<Accident> GetAllAccidentsList(Predicate<Accident> predicate)
        {
            List<Accident> AccidentsList = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            return from accident in AccidentsList
                   select accident;
        }

        public IEnumerable<Accident> GetAllAccidents()
        {
            XElement AccidentRootElem = XMLTools.LoadListFromXMLElement(AccidentPath);

            return (from accident in AccidentRootElem.Elements()
                    select new Accident()
                    {
                        LicenseNum = (accident.Element("LicenseNum").Value),
                        AccidentDate = DateTime.Parse((accident.Element("date").Value)),
                        AccidentNum = int.Parse((accident.Element("num").Value))
                    }
                   );
        }

        public IEnumerable<object> GetAccidentListWithSelectedFields(Func<Accident, object> generate)
        {
            List<Accident> AccidentsList = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentsList);
            return from accident in AccidentsList
                   select accident;
        }

        public void AddAccident(Accident Accident)
        {
            XElement AccidentRootElem = XMLTools.LoadListFromXMLElement(AccidentPath);

            XElement accident1 = (from accident in AccidentRootElem.Elements()
                                  where (accident.Element("user's name").Value) == Accident.AccidentDate.ToString()
                                  select accident).FirstOrDefault();

            if (accident1 != null)
                throw new DO.BadBusLicenseNumException(Accident.AccidentDate.ToString(), "date");

            XElement UserElem = new XElement("accident",
                                   new XElement("name", Accident.AccidentDate.ToString()));

            AccidentRootElem.Add(AccidentRootElem);

            XMLTools.SaveListToXMLElement(AccidentRootElem, AccidentPath);
        }

        public void UpdateAccident(Accident Accident)
        {
            List<Accident> AccidentsList = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            int index = AccidentsList.FindIndex(cis => (cis.AccidentNum == Accident.AccidentNum && cis.AccidentDate == Accident.AccidentDate && cis.LicenseNum == Accident.LicenseNum));


            if (index != -1)
            {
                AccidentsList[index] = Accident;
            }
            else
                throw new DO.BadLicenseNumException(Accident.AccidentNum.ToString(), "wrong accident id details");

            XMLTools.SaveListToXMLSerializer(AccidentsList, OutGoingBusesPath);
        }

        public void DeleteAccident(int Accidentnum)
        {
            List<Accident> AccidentsList = XMLTools.LoadListFromXMLSerializer<Accident>(AccidentPath);

            DO.Accident a = AccidentsList.Find(accident => accident.AccidentNum == Accidentnum);

            if (a.AccidentNum != null)
            {
                AccidentsList.Remove(a);
            }
            else
                throw new DO.BadLicenseNumException(Accidentnum.ToString(), $"wrong outgoingline's ID : {Accidentnum}");

            XMLTools.SaveListToXMLSerializer(AccidentsList, AccidentPath);
        }
        #endregion
    }
}