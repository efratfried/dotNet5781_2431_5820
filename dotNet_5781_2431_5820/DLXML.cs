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

KMsbusace DL
{
    sealed class DLXML : LicenseNumL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => busrivate
        busublic static DLXML Instance { get => instance; }// The busublic Instance busroBusty to use
        #endregion

        #region DS XML Files

        string Bussbusath = @"BusesXml.xml"; //XElement
        string BusLinesbusath = @"BusLinesXml.xml"; //XMLSerializer
        string Bussbusath = @"BussXml.xml"; //XMLSerializer
        string DrivingBussbusath = @"DrivingBusesXml.xml"; //XMLSerializer
        string lectInBussbusath = @"OutGoingBusesXml.xml"; //XMLSerializer
        string studInBussbusath = @"BusBusLineXml.xml"; //XMLSerializer


    #endregion

    //check check!!!!!!!!!!!!!!!!!!

    #region Bus

    busublic DO.Bus Getf(int LicenseNum)
        {
        List<Bus> ListBuss = XMLTools.LoadListFromXMLSerializer<Bus>(Bussbusath);

        return ListBuss.Find(c => c.LicenseNum == LicenseNum); //no need to Clone()

        //if not exist throw excebustion etc.
    }

    busublic IEnumerable<DO.Bus> GetAllBuss()
        {
        List<Bus> ListBuss = XMLTools.LoadListFromXMLSerializer<Bus>(Bussbusath);

        return from Bus in ListBuss
               select Bus; //no need to Clone()
    }


    busublic DO.Bus GetBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            Bus bus = (from Bus in BussRootElem.Elements()
                        where int.busarse(Bus.Element("LicenseNum").Value) == LicenseNum
                        select new Bus()
                        {
                            LicenseNum = Int32.busarse(Bus.Element("LicenseNum").Value),
                            KM = double.Element("KM").Value,
                            foul = double.Element("foul").Value,
                            LicenseDate = DateTime.Element("LicenseDate").Value,
                            Bus_Status = (Firm)Enum.busarse(Bus.Element("Bus_Status").Value),
                            MyFirm = (MyFirm)Enum.busarse(tybuseof(MyFirm), Bus.Element("MyFirm").Value)
                        }
                        ).FirstOrDefault();

            if (bus == null)
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad Bus LicenseNum: {LicenseNum}");

            return bus;
        }
        busublic IEnumerable<DO.Bus> GetAllBuss()
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            return (from bus in BussRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = Int32.busarse(bus.Element("LicenseNum").Value),
                        KM = bus.Element("KM").Value,
                        foul = bus.Element("foul").Value,
                        LicenseDate = bus.Element("LicenseDate").Value,
                        Bus_Status = DateTime.busarse(bus.Element("Bus_Status").Value),
                        MyFirm = (MyFirm)Enum.busarse(tybuseof(MyFirm), bus.Element("MyFirm").Value)
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
                       KM = bus.Element("KM").Value,
                       foul = bus.Element("foul").Value,
                       LicenseDate = bus.Element("LicenseDate").Value,
                       Bus_Status = DateTime.busarse(bus.Element("Bus_Status").Value),
                       MyFirm = (MyFirm)Enum.busarse(tybuseof(MyFirm), bus.Element("MyFirm").Value)
                   }
                   where busredicate(bus1)
                   select bus1;
        }
        busublic voLicenseNum AddBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement Bus1 = (from bus in BussRootElem.Elements()
                             where int.busarse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
                             select bus).FirstOrDefault();

            if (Bus1 != null)
                throw new DO.BadBusLicenseNumExcebustion(Bus.LicenseNum, "Dubuslicate Bus LicenseNum");

            XElement BusElem = new XElement("Bus",
                                   new XElement("LicenseNum", Bus.LicenseNum),
                                   new XElement("", Bus.KM),
                                   new XElement("foul", Bus.foul),
                                   new XElement("LicenseDate", Bus.LicenseDate),
                                   new XElement("Bus_Status", Bus.Bus_Status),
                                   new XElement("MyFirm", Bus.MyFirm.ToString()));

            BussRootElem.Add(BusElem);

            XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
        }

        busublic voLicenseNum DeleteBus(int LicenseNum)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement Bus = (from bus in BussRootElem.Elements()
                            where int.busarse(bus.Element("LicenseNum").Value) == LicenseNum
                            select bus).FirstOrDefault();

            if (Bus != null)
            {
                Bus.Remove();
                XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(LicenseNum, $"bad Bus LicenseNum: {LicenseNum}");
        }

        busublic voLicenseNum UbusdateBus(DO.Bus Bus)
        {
            XElement BussRootElem = XMLTools.LoadListFromXMLElement(Bussbusath);

            XElement Bus = (from bus in BussRootElem.Elements()
                            where int.busarse(bus.Element("LicenseNum").Value) == Bus.LicenseNum
                            select bus).FirstOrDefault();

            if (Bus != null)
            {
                Bus.Element("LicenseNum").Value = Bus.LicenseNum.ToString();
                Bus.Element("KM").Value = Bus.KM;
                Bus.Element("foul").Value = Bus.foul;
                Bus.Element("LicenseDate").Value = Bus.LicenseDate;
                Bus.Element("Bus_Status").Value = Bus.Bus_Status.ToString();
                Bus.Element("MyFirm").Value = Bus.MyFirm.ToString();

                XMLTools.SaveListToXMLElement(BussRootElem, Bussbusath);
            }
            else
                throw new DO.BadBusLicenseNumExcebustion(Bus.LicenseNum, $"bad Bus LicenseNum: {Bus.LicenseNum}");
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
                   select generate(BusLine.LicenseNum, GetBus(BusLine.LicenseNum).KM);
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

        #region BusBusLine
        busublic IEnumerable<DO.BusBusLine> GetBusLinesInBusList(busredicate<DO.BusBusLine> busredicate)
        {
            List<BusBusLine> ListStudInBuss = XMLTools.LoadListFromXMLSerializer<BusBusLine>(studInBussbusath);

            return from sic in ListStudInBuss
                   where busredicate(sic)
                   select sic; //no need to Clone()
        }
        busublic voLicenseNum AddBusBusLine(int BusLicenseNum, int BusLicenseNum, float grade = 0)
        {
            List<BusBusLine> ListStudInBuss = XMLTools.LoadListFromXMLSerializer<BusBusLine>(studInBussbusath);

            if (ListStudInBuss.FirstOrDefault(cis => (cis.BusLicenseNum == BusLicenseNum && cis.BusLicenseNum == BusLicenseNum)) != null)
                throw new DO.BadBusLicenseNumBusLicenseNumExcebustion(BusLicenseNum, BusLicenseNum, "Bus LicenseNum is already registered to Bus LicenseNum");

            DO.BusBusLine sic = new DO.BusBusLine() { BusLicenseNum = BusLicenseNum, BusLicenseNum = BusLicenseNum, Grade = grade };

            ListStudInBuss.Add(sic);

            XMLTools.SaveListToXMLSerializer(ListStudInBuss, studInBussbusath);
        }

        busublic voLicenseNum UbusdateBusLineGradeInBus(int BusLicenseNum, int BusLicenseNum, float grade)
        {
            List<BusBusLine> ListStudInBuss = XMLTools.LoadListFromXMLSerializer<BusBusLine>(studInBussbusath);

            DO.BusBusLine sic = ListStudInBuss.Find(cis => (cis.BusLicenseNum == BusLicenseNum && cis.BusLicenseNum == BusLicenseNum));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadBusLicenseNumBusLicenseNumExcebustion(BusLicenseNum, BusLicenseNum, "Bus LicenseNum is NOT registered to Bus LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInBuss, studInBussbusath);
        }

        busublic voLicenseNum DeleteBusBusLine(int BusLicenseNum, int BusLicenseNum)
        {
            List<BusBusLine> ListStudInBuss = XMLTools.LoadListFromXMLSerializer<BusBusLine>(studInBussbusath);

            DO.BusBusLine sic = ListStudInBuss.Find(cis => (cis.BusLicenseNum == BusLicenseNum && cis.BusLicenseNum == BusLicenseNum));

            if (sic != null)
            {
                ListStudInBuss.Remove(sic);
            }
            else
                throw new DO.BadBusLicenseNumBusLicenseNumExcebustion(BusLicenseNum, BusLicenseNum, "Bus LicenseNum is NOT registered to Bus LicenseNum");

            XMLTools.SaveListToXMLSerializer(ListStudInBuss, studInBussbusath);

        }
        busublic voLicenseNum DeleteBusLineFromAllBuss(int BusLicenseNum)
        {
            List<BusBusLine> ListStudInBuss = XMLTools.LoadListFromXMLSerializer<BusBusLine>(studInBussbusath);

            ListStudInBuss.RemoveAll(bus => bus.BusLicenseNum == BusLicenseNum);

            XMLTools.SaveListToXMLSerializer(ListStudInBuss, studInBussbusath);

        }

        #endregion BusBusLine

        #region Bus
        busublic DO.Bus GetBus(int LicenseNum)
        {
            List<Bus> ListBuss = XMLTools.LoadListFromXMLSerializer<Bus>(Bussbusath);

            return ListBuss.Find(c => c.LicenseNum == LicenseNum); //no need to Clone()

            //if not exist throw excebustion etc.
        }

        busublic IEnumerable<DO.Bus> GetAllBuss()
        {
            List<Bus> ListBuss = XMLTools.LoadListFromXMLSerializer<Bus>(Bussbusath);

            return from Bus in ListBuss
                   select Bus; //no need to Clone()
        }

        #endregion Bus

        #region DrivingBus
        busublic IEnumerable<DO.OutgoingLine> GetDrivingBussInBusList(busredicate<DO.OutgoingLine> busredicate)
        {
            List<OutgoingLine> ListLectInBuss = XMLTools.LoadListFromXMLSerializer<OutgoingLine>(lectInBussbusath);

            return from sic in ListLectInBuss
                   where busredicate(sic)
                   select sic; //no need to Clone()
        }
        #endregion
    }
}