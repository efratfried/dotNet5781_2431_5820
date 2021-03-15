using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using DO;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> BussesList;
        public static List<Station> StationsList;
        public static List<BusLine> BusLinesList;
        public static List<BusStationLine> BusStationsLineList;
        public static List<OutGoingLine> OutGoingLinesList;
        public static List<User> UsersList;
        //public static List<UserDrive> UserDrivesList;
        public static List<Accident> AccidentsList;
        public static List<Treat> TreatsList;
        public static List<DrivingBus> DrivingsList;
        public static List<FollowingStations> followingStations;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            StationsList = new List<Station>
            {
                #region Station_List
                        new Station
                        {
                          CodeStation = "73",
                          StationName = "שדרות גולדה מאיר/המשורר אצ''ג",
                          Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
                          Latitude = 31.825302,
                          longitude = 35.188624,
                          DisableAccess=true
                        },
                         new Station
                        {
                            CodeStation = "76",
                            StationName = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                            Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                            Latitude = 31.738425,
                            longitude = 35.228765,
                            DisableAccess=true,
                        },
                        new Station
                        {
                            CodeStation = "77",
                            StationName = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                            Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                            Latitude = 31.738676,
                            longitude = 35.226704,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "78",
                            StationName = "שרי ישראל/יפו",
                            Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                            Latitude = 31.789128,
                            longitude = 35.206146,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "83",
                            StationName = "בטן אלהווא/חוש אל מרג",
                            Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                            Latitude = 31.766358,
                            longitude = 35.240417,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "84",
                            StationName = "מלכי ישראל/הטורים",
                            Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                            Latitude = 31.790758,
                            longitude = 35.209791,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "85",
                            StationName = "בית ספר לבנים/אלמדארס",
                            Address = "רחוב:אלמדארס  עיר: ירושלים",
                            Latitude = 31.768643,
                            longitude = 35.238509,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "86",
                            StationName = "מגרש כדורגל/אלמדארס",
                            Address = "רחוב:אלמדארס  עיר: ירושלים",
                            Latitude = 31.769899,
                            longitude = 35.23973,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "88",
                            StationName = "בית ספר לבנות/בטן אלהוא",
                            Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                            Latitude = 31.767064,
                            longitude = 35.238443,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "89",
                            StationName = "דרך בית לחם הישה/ואדי קדום",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                            Latitude = 31.765863,
                            longitude = 35.247198,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "90",
                            StationName = "גולדה/הרטום",
                            Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                            Latitude = 31.799804,
                            longitude = 35.213021,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "91",
                            StationName = "דרך בית לחם הישה/ואדי קדום",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                            Latitude = 31.765717,
                            longitude = 35.247102,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "93",
                            StationName = "חוש סלימה 1",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                            Latitude = 31.767265,
                            longitude = 35.246594,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "94",
                            StationName = "דרך בית לחם הישנה ב",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                            Latitude = 31.767084,
                            longitude = 35.246655,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "95",
                            StationName = "דרך בית לחם הישנה א",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                            Latitude = 31.768759,
                            longitude = 31.768759,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "97",
                            StationName = "שכונת בזבז 2",
                            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                            Latitude = 31.77002,
                            longitude = 35.24348,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "102",
                            StationName = "גולדה/שלמה הלוי",
                            Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                            Latitude = 31.8003,
                            longitude = 35.208257,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "103",
                            StationName = "גולדה/הרטום",
                            Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                            Latitude = 31.8,
                            longitude = 35.214106,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "105",
                            StationName = "גבעת משה",
                            Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                            Latitude = 31.797708,
                            longitude = 35.217133,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "106",
                            StationName = "גבעת משה",
                            Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                            Latitude = 31.797535,
                            longitude = 35.217057,
                            DisableAccess=true
                        },
                        //20
                        new Station
                        {
                            CodeStation = "108",
                            StationName = "עזרת תורה/עלי הכהן",
                            Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                            Latitude = 31.797535,
                            longitude = 35.213728,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "109",
                            StationName = "עזרת תורה/דורש טוב",
                            Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                            Latitude = 31.796818,
                            longitude = 35.212936,
                             DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "110",
                            StationName = "עזרת תורה/דורש טוב",
                            Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                            Latitude = 31.796129,
                            longitude = 35.212698,
                             DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "111",
                            StationName = "יעקובזון/עזרת תורה",
                            Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                            Latitude = 31.794631,
                            longitude = 35.21161,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "112",
                            StationName = "יעקובזון/עזרת תורה",
                            Address = " רחוב:יעקובזון  עיר: ירושלים",
                            Latitude = 31.79508,
                            longitude = 35.211684,
                            DisableAccess=true
                        },
                        //25
                        new Station
                        {
                            CodeStation = "113",
                            StationName = "זית רענן/אוהל יהושע",
                            Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                            Latitude = 31.796255,
                            longitude = 35.211065,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "115",
                            StationName = "זית רענן/תורת חסד",
                            Address = " רחוב:זית רענן  עיר: ירושלים",
                            Latitude = 31.798423,
                            longitude = 35.209575,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "116",
                            StationName = "זית רענן/תורת חסד",
                            Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                            Latitude = 31.798689,
                            longitude = 35.208878,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "117",
                            StationName = "קרית הילד/סורוצקין",
                            Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                            Latitude = 31.799165,
                            longitude = 35.206918,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "119",
                            StationName = "סורוצקין/שנירר",
                            Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                            Latitude = 31.797829,
                            longitude = 35.205601,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1485",
                            StationName = "שדרות נווה יעקוב/הרב פרדס ",
                            Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                            Latitude = 31.840063,
                            longitude = 35.240062,
                            DisableAccess=true

                        },
                        new Station
                        {
                            CodeStation = "1486",
                            StationName = "מרכז קהילתי /שדרות נווה יעקוב",
                            Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                            Latitude = 31.838481,
                            longitude = 35.23972,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "1487",
                            StationName = " מסוף 700 /שדרות נווה יעקוב ",
                            Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
                            Latitude = 31.837748,
                            longitude = 35.231598,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "1488",
                            StationName = " הרב פרדס/אסטורהב ",
                            Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                            Latitude = 31.840279,
                            longitude = 35.246272,
                            DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "1490",
                            StationName = "הרב פרדס/צוקרמן ",
                            Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                            Latitude = 31.843598,
                            longitude = 35.243639,
                            DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1491",
                            StationName = "ברזיל ",
                            Address = "רחוב:ברזיל 14 עיר: ירושלים",
                            Latitude = 31.766256,
                            longitude = 35.173,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1492",
                            StationName = "בית וגן/הרב שאג ",
                            Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                            Latitude = 31.76736,
                            longitude = 35.184771,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1493",
                            StationName = "בית וגן/עוזיאל ",
                            Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                            Latitude = 31.770543,
                            longitude = 35.183999,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1494",
                            StationName = " קרית יובל/שמריהו לוין ",
                            Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                            Latitude = 31.768465,
                            longitude = 35.178701,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1510",
                            StationName = " קורצ'אק / רינגלבלום ",
                            Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                            Latitude = 31.759534,
                            longitude = 35.173688,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "1511",
                            StationName = " טהון/גולומב ",
                            Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                            Latitude = 31.761447,
                            longitude = 35.175929
                        },
                        new Station
                        {
                            CodeStation = "1512",
                            StationName = "הרב הרצוג/שח''ל ",
                            Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                            Latitude = 31.761447,
                            longitude = 35.199936,
                             DisableAccess=false
                        },
                        new Station
                        {
                            CodeStation = "1514",
                            StationName = "פרץ ברנשטיין/נזר דוד ",
                            Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                            Latitude = 31.759186,
                            longitude = 35.189336,
                            DisableAccess=false
                        },
                         new Station
                         {
                             CodeStation = "1518",
                             StationName = "פרץ ברנשטיין/נזר דוד",
                             Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
                             Latitude = 31.759121,
                             longitude = 35.189178,
                             DisableAccess=false
                         },
                      new Station
                      {
                          CodeStation = "1522",
                          StationName = "מוזיאון ישראל/רופין",
                          Address = "  רחוב:דרך רופין  עיר: ירושלים ",
                          Latitude = 31.774484,
                          longitude = 35.204882,
                          DisableAccess=false
                      },

                     new Station
                     {
                         CodeStation = "1523",
                         StationName = "הרצוג/טשרניחובסקי",
                         Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
                         Latitude = 31.769652,
                         longitude = 35.208248,
                         DisableAccess=true
                     },
                      new Station
                      {
                          CodeStation = "1524",
                          StationName = "רופין/שד' הזז",
                          Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
                          Latitude = 31.769652,
                          longitude = 35.208248,
                           DisableAccess=true
                      },
                        new Station
                        {
                            CodeStation = "121",
                            StationName = "מרכז סולם/סורוצקין ",
                            Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                            Latitude = 31.796033,
                            longitude = 35.206094,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "123",
                            StationName = "אוהל דוד/סורוצקין ",
                            Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                            Latitude = 31.794958,
                            longitude = 35.205216,
                             DisableAccess=true
                        },
                        new Station
                        {
                            CodeStation = "122",
                            StationName = "מרכז סולם/סורוצקין ",
                            Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                            Latitude = 31.79617,
                            longitude = 35.206158,
                             DisableAccess=true
                        }
                  #endregion
            };

            BusLinesList = new List<BusLine>
            {
                #region BusLine_list
                new BusLine
                {
                    ID = 1,
                    BusNum = 33,
                    Area = Area.Jerusalem,
                    FirstStation = 77,
                    LastStation = 78,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 2,
                    BusNum = 74,
                    Area = Area.Jerusalem,
                    FirstStation = 83,
                    LastStation = 86,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 3,
                    BusNum = 75,
                    Area = Area.Jerusalem,
                    FirstStation = 88,
                    LastStation = 83,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 4,
                    BusNum = 55,
                    Area = Area.Jerusalem,
                    FirstStation = 90,
                    LastStation = 94,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 5,
                    BusNum = 52,
                    Area = Area.Jerusalem,
                    FirstStation = 102,
                    LastStation = 97,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 6,
                    BusNum = 67,
                    Area = Area.Jerusalem,
                    FirstStation = 76,
                    LastStation = 73,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 7,
                    BusNum = 69,
                    Area = Area.Jerusalem,
                    FirstStation = 110,
                    LastStation = 115,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 8,
                    BusNum = 64,
                    Area = Area.Jerusalem,
                    FirstStation = 117,
                    LastStation = 1491,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 9,
                    BusNum = 31,
                    Area = Area.Jerusalem,
                    FirstStation = 88,
                    LastStation = 89,
                    IsDeleted = false
                },
                new BusLine
                {
                    ID = 10,
                    BusNum = 32,
                    Area = Area.Jerusalem,
                    FirstStation = 122,
                    LastStation = 1523,
                    IsDeleted = false
                }
                #endregion
            };

            BussesList = new List<Bus>
            {
                #region Busses_List
                new Bus
                {//public enum Firm { Toyota, Mersedes, Hunda, Temsa, Saularis }
                    LicenseNum ="'12343576",
                    LicenseDate = new DateTime(2018, 7, 8),
                    KM = 100000,
                    foul = 80.2,
                    Firm=Firm.Toyota,
                },
                new Bus
                {
                    LicenseNum = "87654567",
                    LicenseDate = new DateTime(2020, 10, 22),
                    KM = 10000,
                    foul = 50.3,
                   // Status = Status.Available
                   Firm=Firm.Hunda,
                },
                new Bus
                {
                    LicenseNum = "20090890",
                    LicenseDate = new DateTime(2015, 12, 1),
                    KM = 120000,
                    foul = 23.5,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "98074231",
                    LicenseDate = new DateTime(2019, 11, 3),
                    KM = 15000,
                    foul = 45.9,
                    //Status = Status.Available
                    Firm=Firm.Temsa,
                },
                new Bus
                {
                    LicenseNum = "40506066",
                    LicenseDate = new DateTime(2010, 10, 16),
                    KM = 200540,
                    foul = 10,
                    //Status = Status.Available
                },
                new Bus
                {
                    LicenseNum = "17846530",
                    LicenseDate = new DateTime(2013, 7, 27),
                    KM = 151234,
                    foul = 67,
                    //Status = Status.Available
                     Firm=Firm.Toyota,
                },
                new Bus
                {
                    LicenseNum = "87348787",
                    LicenseDate = new DateTime(2020, 4, 14),
                    KM = 4567,
                    foul = 22.6,
                    //Status = Status.Available
                     Firm=Firm.Toyota,
                },
                new Bus
                {
                    LicenseNum = "54546575",
                    LicenseDate = new DateTime(2018, 3, 14),
                    KM = 40980,
                    foul = 0,
                    //Status = Status.Available
                    Firm=Firm.Hunda,
                },
                new Bus
                {
                    LicenseNum = "23453496",
                    LicenseDate = new DateTime(2019, 5, 17),
                    KM = 20003,
                    foul = 50,
                    //Status = Status.Available
                    Firm=Firm.Hunda,
                },
                new Bus
                {
                    LicenseNum = "87687625",
                    LicenseDate = new DateTime(2016, 4, 19),
                    KM = 80450,
                    foul = 27.6,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "43247698",
                    LicenseDate = new DateTime(1999, 12, 17),
                    KM = 300000,
                    foul = 0,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "45457638",
                    LicenseDate = new DateTime(2019, 11, 18),
                    KM = 15330,
                    foul = 39.6,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "98763650",
                    LicenseDate = new DateTime(2020, 9, 28),
                    KM = 2500,
                    foul = 17.8,
                    //Status = Status.Available
                     Firm=Firm.Temsa,
                },
                new Bus
                {
                    LicenseNum = "45382957",
                    LicenseDate = new DateTime(2020, 1, 12),
                    KM = 89045,
                    foul = 34.8,
                    //Status = Status.Available
                     Firm=Firm.Temsa,
                },
                new Bus
                {
                    LicenseNum = "90080098",
                    LicenseDate = new DateTime(2019, 8, 18),
                    KM = 54098,
                    foul = 43.9,
                    //Status = Status.Available
                     Firm=Firm.Temsa,
                },
                new Bus
                {
                    LicenseNum = "70006593",
                    LicenseDate = new DateTime(2018, 6, 26),
                    KM = 101089,
                    foul = 11.4,
                    // = Status.Available
                    Firm=Firm.Hunda,
                },
                new Bus
                {
                    LicenseNum = "24240746",
                    LicenseDate = new DateTime(2017, 10, 6),
                    KM = 109670,
                    foul = 28.7,
                    //Status = Status.Available
                    Firm=Firm.Hunda,
                },
                new Bus
                {
                    LicenseNum = "38602756",
                    LicenseDate = new DateTime(2019, 8, 19),
                    KM = 57892,
                    foul = 38.2,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "78945665",
                    LicenseDate = new DateTime(2016, 5, 15),
                    KM = 178954,
                    foul = 11.2,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                },
                new Bus
                {
                    LicenseNum = "12312300",
                    LicenseDate = new DateTime(2020, 12, 29),
                    KM = 0,
                    foul = 50,
                    //Status = Status.Available
                    Firm=Firm.Mersedes,
                }
            #endregion
            };
            UsersList = new List<User>
            {
                #region users
                new User
                {
                    UserName="Efrat",
                    Password="A1",
                    Me= Access.Manager                 
                },
                  new User
                {
                    UserName="Tamar",
                    Password="TP",
                    Me= Access.Manager
                }
                #endregion
            };
            BusStationsLineList = new List<BusStationLine>
            {
                      
                new BusStationLine
                {
                    ID="3",
                    BusStationNum="73",
                    IndexInLine=0,
                    //NumOfPassingLines=1
                },
            new BusStationLine
            {
                ID = "3",
                BusStationNum = "76",
                IndexInLine = 1,
                //NumOfPassingLines = 2
            }
            };


            /**    public string ID { set; get; }
        public TimeSpan Startime { set; get; }
        public TimeSpan Prequency { set; get; }
        public TimeSpan EndTime { set; get; }*/
        }
           
    }
}





