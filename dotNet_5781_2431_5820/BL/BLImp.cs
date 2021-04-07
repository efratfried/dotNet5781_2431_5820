using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;
//using DocumentFormat.OpenXml.Office2010.Excel;

namespace BL
{
    class BLImp : IBL//internal
    {
        IDL dl = DLFactory.GetDL();
        static Random rand = new Random();

        #region DigitalPanl
        public IEnumerable<BO.DigitalPanel> DigitalPaneles(int NumberOfStation, TimeSpan time)
        {
            List<BO.DigitalPanel> digitalPanels = new List<DigitalPanel>();
            try
            {
                List<BO.DigitalPanel> digitalPanels1 = new List<DigitalPanel>();

                foreach (BO.BusLine item in GetAllLinesPerStation(NumberOfStation))
                {//passing on all the lines which cross a specific busstationline
                    TimeSpan timeSpan = new TimeSpan();
                    TimeSpan timeSpan1 = new TimeSpan();

                    List<BO.BusStationLine> busStationLines = GetBusStationLineList(item.ID.ToString()).ToList();

                    double Distance = 0;
                    int index = busStationLines.FindIndex(i => i.BusStationNum == NumberOfStation.ToString());//finding the wanted BusStationLine's index

                    for (int i = 0; i < index; i++)//calculate the driving time from the begining until the wanted station
                    {
                        timeSpan += busStationLines[i].AverageDrivingTime;
                        Distance += busStationLines[i].Distance;
                    }

                    for (int i = index; i < busStationLines.Count - 1; i++)//calculate how much driving time left from the wanted station to the last station 
                    {
                        timeSpan1 += busStationLines[i].AverageDrivingTime;
                    }

                    // 6:00 ---- 30:00 ------6:30--------- 6:20 = 10
                    // 6:15 ---- 30:00 ------6:45--------- 6:20 = 25
                    
                    foreach (var item1 in GetAllfrequencies(item.ID).OrderBy(i => i.LineStartTime))
                    {
                        foreach (var item2 in item1.DepartureTimes)
                        {
                            if (item2 + timeSpan > time && time > item2)
                            {//get in to the digital panel only the correct & relevant time. if the astimated time is bigger than the current time , also if the current time bigger than the line's exit time.
                             //(the line has already left the start station but didnt arrived yet)
                                BO.DigitalPanel digitalPanel = new DigitalPanel();
                                digitalPanel.BusLineNumber = item.BusNum;
                                digitalPanel.NameOfStation = busStationLines[busStationLines.Count - 1].StationName;
                                digitalPanel.TimeComeToStation = item2 + timeSpan - time;//the math is -the time that the line left the first station + the driving time between teh first station to the current -the actual time
                                digitalPanel.TimeComeToDistanation = item2 + timeSpan + timeSpan1;//the math is - the time that the line left the first station +the driving time between teh first station to the current + the driving time from currect station to the last station.
                                digitalPanel.DistanceFromStation = Distance / (item2 + timeSpan).TotalSeconds;
                                digitalPanels.Add(digitalPanel);
                            }
                        }
                    }
                }
                return digitalPanels;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Bus
        /// <summary>
        /// this is a convert function. each of the bus's functions use that convert function.
        /// the function gets an accurance of DO.bus & return na accurance of BO.bus by sending to the lower layout &  copyProperties.
        /// the function send the license num of the bus ,if the licensenum is found in the lower layout os the copy is done. else exception.
        /// </summary>
        /// <param name="BusDO"></param>
        /// <returns></returns>
        BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();
            try
            {
                BusDO = dl.GetBus(BusDO.LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus LicenseNum is illegal", Ex);
            }

            BusDO.CopyPropertiesTo(BusBO);
            BusBO.Status = status(BusBO);
            BusBO.Foul_Status = foul_Status(BusBO);
            return BusBO;
        }
        /// <summary>
        /// the function gets a bus's licensenum ,send it to DO -if it's param found so ew return the the BO bus by sending to the convertfunction
        /// else exception.
        /// </summary>
        /// <param name="LicenseNum"></param>
        /// <returns></returns>
        public BO.Bus GetBus(string LicenseNum)
        {
            DO.Bus BusDO;
            try
            {
                BusDO = dl.GetBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Buss' LicenseNum does not exist or it is not a Bus", Ex);
            }
            return BusDoBoAdapter(BusDO);
        }
        /// <summary>
        /// the function returns the whole exsisting buss in the xml file by sending to the DO layout & after ot the convertfunction.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetAllBuss()
        {
            return from BusDO in dl.GetAllBusses()
                   orderby BusDO.LicenseNum
                   select BusDoBoAdapter(BusDO);
        }
        /// <summary>
        /// the function returns the licensnum list of all the busses by sending to the DO layout & order it by theirs licensenums.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetBusIDList()
        {
            return from BusDO in dl.GetAllBusses()
                   orderby BusDO.LicenseNum
                   select BusDoBoAdapter(BusDO);
        }
        /// <summary>
        /// this function isn't using.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetBussBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// the function returns the licensenums of the entire busses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetBusLicenseNumList()
        {
            return from item in dl.GetAllBusListWithSelectedFields((BusDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Bus() { LicenseNum = BusDO.LicenseNum };
            })
                   let BusBO = item as BO.Bus
                   //orderby Bus.LicenseNum
                   select BusBO;
        }
        /// <summary>
        /// the function get an accurance of BO.Bus ,check if it exsist.if it does so we can update it's fields by copying & sending the accurance to the DO.Updatebus.
        /// else exception.
        /// </summary>
        /// <param name="bus"></param>
        public void UpdateBusPersonalDetails(BO.Bus bus)
        {
            //Update DO.Bus            
            DO.Bus BusDO = new DO.Bus();
            bus.CopyPropertiesTo(BusDO);
            try
            {
                dl.UpdateBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus's LicenseNum is illegal", Ex);
            }
        }
        /// <summary>
        /// the function gets a bus's licensenum -send it ot the DO layout, if it exsist it baing earesed.
        /// else exception.
        /// </summary>
        /// <param name="LicenseNum"></param>
        public void DeleteBus(string LicenseNum)
        {
            try
            {
                dl.DeleteBus(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }
        /// <summary>
        /// the function gets an accurance of a BO.Bus checks the validation of it's licensenum - case it's inncorrect -exception.
        /// case the date & the lengh of the licensenum are'nt synchronised - exception.
        /// the adding is by copy & sending the accurance to the DO layout.
        /// if it already exsist -exception.
        /// else add it.
        /// </summary>
        /// <param name="bus"></param>
        public void AddBus(BO.Bus bus)
        {
            DO.Bus BusDO = new DO.Bus();
            if (bus.LicenseNum.Length != 7 && bus.LicenseNum.Length != 8)
            {

                string Ex = "The lengh of the License isnt corrent";
                throw new BO.BadBusIdException("Incorrect details", Ex);
            }

            if ((bus.LicenseDate.Year < 2018 && bus.LicenseNum.Length != 7) || (bus.LicenseDate.Year >= 2018 && bus.LicenseDate < DateTime.Now && bus.LicenseNum.Length != 8))
            {//check the validation of the license num accroding to it date
                string Ex = "the date or the license number don't match";
                throw new BO.BadBusIdException("Incorrect details", Ex);
            }
            bus.CopyPropertiesTo(BusDO);
            //BusDO.CopyPropertiesToNew(typeof(BO.Bus));
            try
            {
                dl.AddBus(BusDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }
        }
        /// <summary>
        /// the function gets an accurance of BO.Bus -checks the bus's foul & accroding to the results so it's foul status.
        /// which accroding to that the bus's avilibility status is decieded.
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public BO.Foul_Status foul_Status(BO.Bus bus)
        {
            if (bus.foul == 0)
            {
                bus.Foul_Status = Foul_Status.empty;
            }

            if (bus.foul == 100)
            {//as we check in some websisites
                bus.Foul_Status = Foul_Status.full_tank;
            }

            if (bus.foul < 100 && bus.foul > 50)
            {
                bus.Foul_Status = Foul_Status.avrage;
            }

            if (bus.foul < 50 && bus.foul > 0)
            {
                bus.Foul_Status = Foul_Status.low;
            }
            return bus.Foul_Status;
        }
        /// <summary>
        /// the funtion deciede the aviability status of the bus accroding to it's foul status & how much km it's already pass.
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public BO.Status status(BO.Bus bus)
        {
            if (bus.KM > 300000 || bus.foul < 0)
            {
                bus.Status = Status.UnAvailable;
            }
            else if (bus.drivingBusesDuco != null)
            {
                if (bus.drivingBusesDuco.Last().finish == false)
                {
                    bus.Status = Status.OnDrive;
                }
            }
            else
            {
                bus.Status = Status.Available;
            }
            return bus.Status;
        }


        #endregion Bus

        #region Station
        /// <summary>
        /// the function is a convert function which all the station's functions are using it.
        /// </summary>
        /// <param name="StationDO"></param>
        /// <returns></returns>
        BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();
            string CodeStation = StationDO.CodeStation;
            try
            {

                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("wring station's code", Ex);
            }

            StationDO.CopyPropertiesTo(StationBO);

            return StationBO;
        }
        /// <summary>
        /// the function gets the code of a station,send it to the DO layout which checks if it exsist.
        /// if it is so this function return it & send it to the convert function.
        /// else excetion.
        /// </summary>
        /// <param name="CodeStation"></param>
        /// <returns></returns>
        public BO.Station GetStation(string CodeStation)
        {
            DO.Station StationDO;
            try
            {
                StationDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("Station's code don't exist or it is not a station", Ex);
            }
            return StationDoBoAdapter(StationDO);
        }
        /// <summary>
        /// the function returns all the exsisting stations by returning from the DO layout - & send it to the convert function.
        /// the stations will by order by theirs codes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetAllStations()
        {
            //return from item in dl.GetBusListWithSelectedFields( (stud) => { return GetBus(stud.ID); } )
            //       let Bus = item as BO.Bus
            //       orderby Bus.ID
            //       select Bus;
            return from StationDO in dl.GetAllStations()
                   orderby StationDO.CodeStation
                   select StationDoBoAdapter(StationDO);
        }
        /* public IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate)
         {
             throw new NotImplementedException();
         }*/
        /// <summary>
        /// the function returns all the codes of the stations. by sending to the DO layout. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Station> GetStationLicenseNumList()
        {
            return from item in dl.GetAllStationListWithSelectedFields((StationDO) =>
            {
                try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                return new BO.Station() { CodeStation = StationDO.CodeStation };
            })
                   let StationBO = item as BO.Station
                   //orderby StationBO.CodeStation
                   select StationBO;
        }
        /// <summary>
        /// the function gets an accurance of BO.Station ,copy  send it to the DO layout. there it checks if it exsist if it is os we can uupdate it's feils.
        /// else exception.
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStationPersonalDetails(BO.Station station)
        {
            //Update DO.Bus            
            DO.Station StationDO = new DO.Station();
            station.CopyPropertiesTo(StationDO);
            try
            {
                dl.UpdateStation(StationDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationNumException("Station num is illegal", Ex);
            }
        }
        /// <summary>
        /// the function gets the station's code -that code is sent to the DO layout which checks if it already exsist .
        /// if it is so we can earese it .
        /// else exception.
        /// </summary>
        /// <param name="Codestation"></param>
        public void DeleteStation(string Codestation)
        {
            try
            {
                dl.DeleteStation(Codestation);
                /* dl.DeleteFollowingStation(Codestation);
                 foreach (var item in GetAllLinesPerStation(int.Parse(Codestation)))
                 {

                     List<DO.BusStationLine> busStationLines = dl.GetAllBusStationLines(item.ID.ToString()).OrderBy(i => i.IndexInLine).ToList();
                     if (busStationLines[0].BusStationNum != Codestation
                         && busStationLines[busStationLines.Count - 1].BusStationNum != Codestation)
                     {
                         DeleteBusStationLine(busStationLines[busStationLines.FindIndex(i => i.BusStationNum == Codestation)].BusStationNum,
                            int.Parse(busStationLines[0].ID), busStationLines[busStationLines.FindIndex(i => i.BusStationNum == Codestation)].IndexInLine
                             );
                     }
                     else
                     {
                         dl.DeleteBusStationLine(busStationLines[busStationLines.FindIndex(i => i.BusStationNum == Codestation)].BusStationNum, busStationLines[0].ID);
                     }
                 }*/
            }
            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("station's code does not exist or it is not a correct code", Ex);
            }
        }
        /// <summary>
        /// the function gets an accurance of BO.Statin send it to check if it already exsist.
        /// if it is so exception.
        /// else we cana add it. but we first check the validation of the entered coordinates  ,the station's code.
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(BO.Station station)
        {
            //the random number in in the begining
            DO.Station StationDO = new DO.Station();
            bool exist = bool.Parse((GetAllStations().Contains(station)).ToString());
            if (exist)
            {
                throw new Exception("The wanted station is already exist in the data");
            }
            if (station.Latitude > 33.3 || station.Latitude < 31 || station.longitude < 33.3 || station.longitude > 35.5)
            {
                throw new Exception("the cordinates are wrong");
            }
            if (station.CodeStation.Length > 6 || station.CodeStation.Length < 0)
            {
                throw new Exception("incorrect station's code");
            }
            station.CopyPropertiesTo(StationDO);
            try
            {
                dl.AddStation(StationDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadStationException("station num is illegal", Ex);
            }
        }
        /// <summary>
        /// the function gets an accurance of BO.Station & the currenttime & return it.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="CurrentTime"></param>
        /// <returns></returns>
        public TimeSpan GetLineTimingPerStation(BO.Station station, TimeSpan CurrentTime)
        {
            return CurrentTime;//need fix
        }


        #endregion

        #region BusStationLine
        /// <summary>
        /// this is the convert function which all the busstationline's function use.
        /// it convert from DO.Busstationline to BO.Busstationline.
        /// </summary>
        /// <param name="BLSDO"></param>
        /// <returns></returns>
        BO.BusStationLine BusStationLineDoBoAdapter(DO.BusStationLine BLSDO)
        {
            BO.BusStationLine BLSBO = new BO.BusStationLine();

            //copy all relevant properties
            BLSDO.CopyPropertiesTo(BLSBO);

            return BLSBO;
        }
        /// <summary>
        /// the function gets a BusStatoinLine's code ,check if it exsist by sending it to the DO layout.
        /// if it exsist so we return the match accurance.
        /// else exception.
        /// </summary>
        /// <param name="CodeStation"></param>
        /// <returns></returns>
        public BO.Station GetBusStationLine(string CodeStation)
        {
            DO.Station BusStationLineDO;
            try
            {
                BusStationLineDO = dl.GetStation(CodeStation);
            }
            catch (DO.BadBusStationLineCodeException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Buss' LicenseNum does not exist or it is not a Bus", Ex);
            }
            return StationDoBoAdapter(BusStationLineDO);
        }
        /// <summary>
        /// the function gets the id of the BusLine & returns all it's BusStatopnLines (at least 2) 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IEnumerable<BO.BusStationLine> GetAllBusStationLines(int num)
        {
            return from BusStationLineDO in dl.GetBusStationLineList(b => b.ID == num.ToString())
                   orderby BusStationLineDO.IndexInLine
                   select BusStationLineDoBoAdapter(BusStationLineDO);
        }
        /* public IEnumerable<BO.BusStationLine> GetBusStationLinesBy(Predicate<BO.BusStationLine> predicate)
         {//need fill field 
             throw new NotImplementedException();
         }*/
        /// <summary>
        /// the function gets a BusStationLine code, send & check if it exsist.
        /// case it is so we get all it's accurance in the busstationline xml file.
        /// than we get all the distance & the driving time between each 2 following stations -busstationline.
        /// return the new list with the new details of the BusStationLine.
        /// </summary>
        /// <param name="BusStationLineNum"></param>
        /// <returns></returns>
        public IEnumerable<BO.BusStationLine> GetBusStationLineList(string BusStationLineNum)
        {
            List<BO.BusStationLine> bsl = new List<BusStationLine>();
            List<DO.BusStationLine> bs = dl.GetBusStationLineList(i => i.ID.ToString() == BusStationLineNum).OrderBy(i => i.IndexInLine).ToList();
            for (int i = 0; i < bs.Count; i++)
            {
                BO.BusStationLine b = new BO.BusStationLine();

                bs[i].CopyPropertiesTo(b);
                bsl.Add(b);
                bsl[i].StationName = dl.GetStation(bsl[i].BusStationNum).StationName;
            }
            for (int i = 0; i < bs.Count - 1; i++)
            {
                bsl[i].Distance = dl.GetFollowingStation(bs[i].BusStationNum, bs[i + 1].BusStationNum).Distance;
                bsl[i].AverageDrivingTime = dl.GetFollowingStation(bs[i].BusStationNum, bs[i + 1].BusStationNum).AverageDrivingTime;
            }
            return bsl;
            //return from item in dl.GetBusStationsLineListWithSelectedFields((BusStationLineDO) =>
            //{
            //    try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
            //    return new BO.BusStationLine() { ID = BusStationLineDO.ID };
            //})
            //       let BusStationLineBo = item as BO.BusStationLine
            //       //orderby Bus.LicenseNum
            //       select BusStationLineBo;
        }
        /// <summary>
        /// the function gets an accurance of BusStationLine ,send & check in the DO layout if it exsist & return all it's accurance by their's id & index on the line.
        /// we calculate the index on the line by a running num.
        /// if the accurance is already exsist so there is an exception.
        /// </summary>
        /// <param name="busStationLine"></param>
        public void AddBusStationLine(BusStationLine busStationLine)
        {
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            try
            {
                foreach (DO.BusStationLine item in dl.GetBusStationLineList(i => i.ID == busStationLine.ID && i.IndexInLine >= busStationLine.IndexInLine).ToList())
                {
                    item.IndexInLine++;
                    dl.UpdateBusStationLine(item);
                }
                busStationLine.CopyPropertiesTo(BusStationLineDO);
                dl.AddBusStationLine(BusStationLineDO);
            }
            catch (DO.BadBusStationLineCodeException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("The details of the bus station are'nt wrong", Ex);
            }
        }
        /// <summary>
        /// the function gets an accurance of BusStationLine copy & send it to the DO layout -check if it exsist.
        /// case it is so we can update it's fields.
        /// else exception.
        /// </summary>
        /// <param name="bus_station_num"></param>
        public void UpdateBusStationLinePersonalDetails(BO.BusStationLine bus_station_num)
        {
            DO.BusStationLine BusStationLineDO = new DO.BusStationLine();
            bus_station_num.CopyPropertiesTo(BusStationLineDO);
            try
            {
                dl.UpdateBusStationLine(BusStationLineDO);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus's LicenseNum is illegal", Ex);
            }
        }
        /// <summary>
        /// the function gets the BusStationLine's id,index in line.
        /// with those we check if it exsist by looking fot the correct match in the xml files which has the 2 arguments.
        /// case it isnt exsist -exception.
        /// else we can delete it by sending to the DO layout it's BusStationNum. we also update the all the busstationlines that after the wanted one to be insexinline -1.
        /// than we add the new couple of the followingstatons - the one before the wanted one ,& the one which next.
        /// if the wanted busstationline isnt exsist -exception.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        public void DeleteBusStationLine(string num, int ID, int index)
        {
            try
            {
                List<DO.BusStationLine> bs = dl.GetBusStationLineList(i => i.ID == ID.ToString()).OrderBy(i => i.IndexInLine).ToList();
                foreach (DO.BusStationLine item in dl.GetBusStationLineList(i => i.ID == ID.ToString() && i.IndexInLine > index).ToList())
                {
                    item.IndexInLine--;
                    dl.UpdateBusStationLine(item);
                }
                DO.FollowingStations f = new DO.FollowingStations();

                int number = int.Parse(bs[index - 1].BusStationNum);
                int number1 = int.Parse(bs[index + 1].BusStationNum);
                f.FirstStationCode = number.ToString();
                f.SecondStationCode = number1.ToString();
                AddFollowingStation(f.FirstStationCode, f.SecondStationCode);

                string BusStationLine = num;
                dl.DeleteBusStationLine(ID.ToString(), BusStationLine);
            }

            catch (DO.BadCodeStationException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }
        /*public IEnumerable<BO.BusStationLine> GetAllBusStationLinesPerLine(int lineId)
        {
            return from DOlineStation in dl.GetBusStationLinesListThatMatchAStation(lineId.ToString())
                   let BOlineStation = BusStationLineDoBoAdapter(DOlineStation)
                   select BOlineStation;
        }*/

        public void DeleteStationFromLine(BO.BusLine busline, string code)
        {
            try
            {
                /* if(busline.stationsList.Contains(code)==false)
                 {
                     throw new BO.BadBusStationLineCodeException(busline.ID.ToString(),code);
                 }*/

                // dl.DeleteBusStationLine(code);
            }
            catch (DO.BadStationNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusStationLineCodeException("Bus LicenseNum does not exist or it is not a Bus", Ex);
            }
        }

        #endregion

        #region BusLine
        /// <summary>
        /// a converting function of busline on the way from database to ui
        /// the function gets a DO busline and valid it by
        /// asking the database if such an instance indeed exits,
        /// than copy the instance to a new bo busline and calculate the miising fields- if there are
        /// and returns the bo busline
        /// </summary>
        BO.BusLine BuslineDoBoAdapter(DO.BusLine blDO)
        {
            BO.BusLine blBO = new BO.BusLine();
            DO.BusLine newblDO;//before copying lineDO to lineBO, we need to ensure that lineDO is legal- legal busNumber.
            //sometimes we get here after the user filled lineDO fields. thats why we copy the given lineDO to a new lineDO and check if it is legal.
            try
            {
                newblDO = dl.GetBusLine(blDO.ID);//if code is legal, returns a new lineStationDO. if not- ecxeption.
            }
            catch (DO.BadBusLineException ex)
            {
                throw new BO.BadBusLineIdException("Line bus number is illegal\n", ex);
            }
            newblDO.CopyPropertiesTo(blBO);//copies- only flat properties.
            //now we need to restart the "lineStations" list of each line.
            return blBO;
        }
        /// <summary>
        /// the functions gets a station code and returns all the lines who drive through the station
        /// </summary>
        /// <param name="GetAllLinesPerStation"></param>
        /// <returns> IEnumerable<BO.BusLine> </returns>
        public IEnumerable<BO.BusLine> GetAllLinesPerStation(int code)
        {
            return from lineStation in dl.GetLineStationsListThatMatchAStation(code)
                   select BuslineDoBoAdapter(lineStation);
        }
        /// <summary>
        /// the functions gets an area and returns all the lines who drive in this area
        /// and ordering it by busline number
        /// </summary>
        /// <param name="GetAllLinesByArea"></param>
        /// <returns> IEnumerable<BO.BusLine> </returns>
        public IEnumerable<BO.BusLine> GetAllLinesByArea(BO.Area area)
        {
            return from LineDO in dl.GetAllBusLines()
                   where LineDO.Area.CompareTo((DO.Area)area) == 0//if the erea is equal to the given area
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        /// <summary>
        /// the functions returns all buslines in database and order it by id
        /// </summary>
        /// <param name="GetBusLines"></param>
        /// <returns> IEnumerable<BO.BusLine> </returns>
        public IEnumerable<BO.BusLine> GetBusLines()
        {
            return from BusLineDO in dl.GetAllBusLines()
                   orderby BusLineDO.ID
                   select BuslineDoBoAdapter(BusLineDO);
        }
        /// <summary>
        /// the functions gets an id and returns a bo busline 
        /// in the way up from data base to ui
        /// she search in the dl layer than send it to BuslineDoBoAdapter so it 
        /// returns the wanted bo busline
        /// </summary>
        /// <param name="GetBusLine"></param>
        /// <returns>BO.BusLine </returns>
        public BO.BusLine GetBusLine(int ID)
        {
            DO.BusLine BusLineDO;
            try
            {
                BusLineDO = dl.GetBusLine(ID);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("Bus's ID does not exist or it is not a Bus", ex);
            }
            return BuslineDoBoAdapter(BusLineDO);
        }
        // NEEDS A CHECKUP ! ! ! 
        //public IEnumerable<BO.BusLine> GetBusLineIDList()
        //{
        //    return from BusLineDO in dl.GetAllBusLines()
        //           orderby BusLineDO.IsDeleted
        //           select BuslineDoBoAdapter(BusLineDO);
        //}
        /// <summary>
        /// the functions gets a Predicate and returns all the lines who consent the predicate 
        /// </summary>
        /// <param name="GetBusLinesBy"></param>
        /// <returns> IEnumerable<BO.BusLine> </returns>
        public IEnumerable<BO.BusLine> GetBusLinesBy(Predicate<BO.BusLine> predicate)
        {

            List<BO.BusLine> busLines = new List<BusLine>();
            List<DO.BusLine> busLines1 = dl.GetAllBusLines().ToList();
            for (int i = 0; i < busLines1.Count; i++)
            {
                busLines.Add(BuslineDoBoAdapter(busLines1[i]));
                //  busLines1[i].CopyPropertiesTo(busLines[i]);
            }
            return from l in busLines
                   where predicate(l)
                   select l;
        }
        /// <summary>
        /// the functions returns all the lines ordered by busnum
        /// </summary>
        /// <param name="GetBusLineLicenseNumList"></param>
        /// <returns> IEnumerable<BO.BusLine> </returns>
        public IEnumerable<BO.BusLine> GetBusLineLicenseNumList()
        {
            return from LineDO in dl.GetAllBusLines()
                       //where LineDO.ID.CompareTo((DO.BusLine)busLine) == 0
                   orderby LineDO.BusNum           //order it by their bus number
                   select BuslineDoBoAdapter(LineDO);
        }
        /// <summary>
        /// the functions gets a bo busline,
        /// if the line is valid it send it to dl layer for update in the files
        /// then delete the old stationline and followingstations and implement the updated ones 
        /// </summary>
        /// <param name="UpdateBusLinePersonalDetails"></param>
        /// <returns> void </returns>
        public void UpdateBusLinePersonalDetails(BO.BusLine BusLine)
        {
            //Update DO.Bus            
            DO.BusLine BusLineDO = new DO.BusLine();
            BusLine.CopyPropertiesTo(BusLineDO);
            try
            {
                dl.UpdateBusLine(BusLineDO);
                List<DO.BusStationLine> bs = dl.GetBusStationLineList(B => B.ID == BusLineDO.ID.ToString()).OrderBy(I => I.IndexInLine).ToList();
                if (bs[bs.FindIndex(i => i.IndexInLine == 0)].BusStationNum != BusLineDO.FirstStation.ToString())
                {//checkiing which station did it update
                    dl.DeleteBusStationLine(bs[bs.FindIndex(i => i.IndexInLine == 0)].ID, bs[bs.FindIndex(i => i.IndexInLine == 0)].BusStationNum);
                    bs[bs.FindIndex(i => i.IndexInLine == 0)].BusStationNum = BusLineDO.FirstStation.ToString();
                    dl.AddBusStationLine(bs[bs.FindIndex(i => i.IndexInLine == 0)]);
                    DO.FollowingStations fw = new DO.FollowingStations();
                    fw.FirstStationCode = bs[bs.FindIndex(i => i.IndexInLine == 0)].BusStationNum;
                    fw.SecondStationCode = bs[bs.FindIndex(i => i.IndexInLine == 1)].BusStationNum;
                    dl.AddFollowingStations(fw);
                }

                if (bs[bs.Count - 1].BusStationNum != BusLineDO.LastStation.ToString())
                {
                    dl.DeleteBusStationLine(bs[bs.Count - 1].ID, bs[bs.Count - 1].BusStationNum);
                    bs[bs.Count - 1].BusStationNum = BusLineDO.LastStation.ToString();
                    dl.AddBusStationLine(bs[bs.Count - 1]);

                    DO.FollowingStations fw = new DO.FollowingStations();
                    fw.FirstStationCode = bs[bs.Count - 2].BusStationNum;
                    fw.SecondStationCode = bs[bs.Count - 1].BusStationNum;
                    dl.AddFollowingStations(fw);
                }
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("BusLine's LicenseNum is illegal", ex);
            }
        }
        /// <summary>
        /// the function gets a busline id and calls the dl layer to delete it from the database
        /// </summary>
        /// <param name="DeleteBusLine"></param>
        public void DeleteBusLine(int ID)
        {//delete all the stations
            try
            {
                dl.DeleteBusLine(ID);
                dl.DeleteBusStationLine(ID.ToString());
                foreach (var item in GetAllfrequencies(ID))
                {
                    dl.DeleteLineExit(item.Id, item.LineStartTime);
                }
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadBusLineIdException("BusLine's ID does not exist or it is not a Bus", ex);
            }
        }
        /// <summary>
        /// the func gets a bo busline and send it to the dl
        /// then two buslinestations are created for the two must-have stations 
        /// and so the FollowingStation
        /// </summary>
        /// <param name="AddBusLine"></param>
        public void AddBusLine(BO.BusLine busline)
        {
            DO.BusLine BusLineDO = new DO.BusLine();

            busline.CopyPropertiesTo(BusLineDO);

            try
            {
                DO.BusStationLine first = new DO.BusStationLine();
                DO.BusStationLine second = new DO.BusStationLine();

                busline.ID = dl.AddBusLine(BusLineDO);
                first.BusStationNum = busline.FirstStation.ToString();
                second.BusStationNum = busline.LastStation.ToString();
                first.IndexInLine = 0;
                second.IndexInLine = 1;
                first.ID = busline.ID.ToString();
                second.ID = busline.ID.ToString();
                dl.AddBusStationLine(first);
                dl.AddBusStationLine(second);
                DO.FollowingStations fs = new DO.FollowingStations();
                fs.FirstStationCode = first.BusStationNum;
                fs.SecondStationCode = second.BusStationNum;
                dl.AddFollowingStations(fs);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Bus ID is illegal", Ex);
            }
        }

        #endregion

        #region User
        /// <summary>
        /// a converting function of user on the way from database to ui
        /// the function gets a DO user and valid it by
        /// asking the database if such an instance indeed exits,
        /// than copy the instance to a new bo busline and calculate the missing fields- if there are some
        /// and returns the bo user
        /// </summary>
        public BO.User userBoDoAdapter(DO.User userDO)
        {
            BO.User userBO = new BO.User();
            DO.User newUserDO = userDO;

            newUserDO.CopyPropertiesTo(userBO);

            //userDO.CopyPropertiesTo(userBO);

            return userBO;
        }

        /// <summary>
        /// returns the user with the given name and password
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>user bo</returns>
        public BO.User GetUser(string name, string password)
        {
            DO.User userDO;
            try
            {
                userDO = dl.GetUser(name, password);
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                throw new BO.BadUserName_PasswordException("The user with this password wasn't found\n", ex);
            }
            return userBoDoAdapter(userDO);
        }

        /// <summary>
        /// add a user
        /// </summary>
        /// <param name="user">user</param>
        public void AddUser(BO.User user)
        {
            try
            {
                dl.AddUser(userDoBOAdapter(user));
            }
            catch (DO.BadUserName_PasswordException ex)
            {
                throw new BO.BadUserName_PasswordException("couldn't add the user\n", ex);
            }
        }

        /// <summary>
        /// adopt BO.user to DO.user and return it
        /// </summary>
        /// <param name="userBO"></param>
        /// <returns></returns>
        private DO.User userDoBOAdapter(BO.User userBO)
        {
            DO.User userDO = new DO.User();
            userBO.CopyPropertiesTo(userDO);
            return userDO;
        }

        /// <summary>
        /// returns IEnumerable of all the users
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<BO.User> GetAllUsers()
        {
            return from item in dl.GetAllUser()
                   select userBoDoAdapter(item);
        }

        #endregion

        #region FollowingStation
        /// <summary>
        /// a converting function of FollowingStation on the way from database to ui
        /// the function gets a DO FollowingStation and valid it by
        /// asking the database if such an instance indeed exits,
        /// than copy the instance to a new bo FollowingStation and calculate the missing fields- if there are some
        /// and returns the bo FollowingStation
        /// </summary>
        BO.FollowingStations FollowingSDoBoAdapter(DO.FollowingStations fsDO)
        {
            BO.FollowingStations fsBO = new BO.FollowingStations();

            fsDO.CopyPropertiesTo(fsBO);

            return fsBO;
        }
        /// <summary>
        /// the function gets 2 stations code and returns a 
        /// bo FollowingStation conction if there is one in the database
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="code2"></param>
        /// <returns>BO.FollowingStations</returns>
        public BO.FollowingStations GetFollowingStation(string code1, string code2)
        {
            DO.FollowingStations fsDO;
            try
            {
                fsDO = dl.GetFollowingStation(code1, code2);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("station's codes are wrong", Ex);
            }
            return FollowingSDoBoAdapter(fsDO);
        }
        /// <summary>
        /// the function returns all followingstations
        /// ordered by their first stationcode 
        /// </summary>
        /// <returns>IEnumerable<BO.FollowingStations></returns>
        public IEnumerable<BO.FollowingStations> GetAllFollowingStations()
        {
            return from fsDO in dl.GetAllFollowingStationss()
                   orderby fsDO.FirstStationCode
                   select FollowingSDoBoAdapter(fsDO);
        }
        //here
        public IEnumerable<BO.FollowingStations> GetFollowingStationSBy(Predicate<BO.FollowingStations> predicate)
        {
            List<BO.FollowingStations> fs = new List<FollowingStations>();
            List<DO.FollowingStations> fs1 = dl.GetAllFollowingStationss().ToList();

            for (int i = 0; i < fs1.Count; i++)
            {
                fs.Add(FollowingSDoBoAdapter(fs1[i]));
                //  busLines1[i].CopyPropertiesTo(busLines[i]);
            }
            return from f in fs
                   where predicate(f)
                   select f;
        }
        public void UpdateFollowingStationPersonalDetails(BO.FollowingStations FollowingStation)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            FollowingStation.CopyPropertiesTo(fsDO);
            try
            {
                dl.UpdateFollowingStations(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("wrong details", Ex);
            }
        }
        public void DeleteFollowingStation(string code)
        {
            try
            {
                dl.DeleteFollowingStation(code);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("deleting failed", Ex);
            }
        }
        public void DeleteFollowingStations(BO.FollowingStations followingStation)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            followingStation.CopyPropertiesTo(fsDO);
            try
            {
                dl.DeleteFollowingStation(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("deleting failed", Ex);
            }
        }
        public void AddFollowingStation(string code1, string code2)
        {
            DO.FollowingStations fsDO = new DO.FollowingStations();
            fsDO.FirstStationCode = code1;
            fsDO.SecondStationCode = code2;


            try
            {
                dl.AddFollowingStations(fsDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("wrong details", Ex);
            }
        }

        public double DistancefromPriviouStation(string station1, string station2)
        {
            DO.Station st1 = dl.GetStation(station1);
            DO.Station st2 = dl.GetStation(station2);
            //returns the distance by equation sqrt( (x-x)^2+(y-y)^2)
            double Distance = Math.Sqrt((Math.Pow(st1.longitude - st2.longitude, 2) + (Math.Pow(st1.Latitude - st2.Latitude, 2))));//the minimal distance between two station
            return Distance;
        }
        public TimeSpan DrivingTimeBetweenTwoStations(string station1, string station2)
        {
            double Dis = DistancefromPriviouStation(station1, station2);
            Dis = Dis * 60 / 75;//75 km per hour is a avrage of the able speed on the road for busses - 50 in the city and 100 out of the city
            int dis = Convert.ToInt32(Dis);//dont care to loose a little bit info because it is not exact but evaluieted time
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }
        public TimeSpan WalkingTimeBetweenTwoStations(string station1, string station2)
        {
            double Dis = DistancefromPriviouStation(station1, station2);
            Dis = Dis * 60 / 4;//75 km per hour is a avrage of the able speed on the road for busses - 50 in the city and 100 out of the city
            int dis = Convert.ToInt32(Dis);//dont care to loose a little bit info because it is not exact but evaluieted time
            TimeSpan dt = new TimeSpan(dis);
            return dt;
        }
        #endregion

        #region OutGoingLine
        public IEnumerable<BO.OutGoingLine> GetAllfrequencies(int lineNum)
        {
            List<BO.OutGoingLine> outGoingLine = new List<OutGoingLine>();
            try
            {
                List<DO.OutGoingLine> outGoingLine1 = dl.LineExitList(lineNum).ToList();
                List<BO.BusStationLine> busStationLines = GetBusStationLineList(lineNum.ToString()).ToList();
                TimeSpan timeSpan = new TimeSpan(busStationLines.Sum(i => i.AverageDrivingTime.Hours),
                    busStationLines.Sum(i => i.AverageDrivingTime.Minutes),
                    busStationLines.Sum(i => i.AverageDrivingTime.Seconds));

                for (int i = 0; i < outGoingLine1.Count; i++)
                {

                    outGoingLine.Add(new OutGoingLine { LineStartTime = outGoingLine1[i].LineStartTime, LineFinishTime = outGoingLine1[i].LineFinishTime, LineFrequencyTime=outGoingLine1[i].LineFrequencyTime, Id = outGoingLine1[i].Id });
                    for (TimeSpan j = outGoingLine1[i].LineStartTime; j <= outGoingLine1[i].LineFinishTime; j += outGoingLine1[i].LineFrequencyTime)
                    {
                        outGoingLine[i].DepartureTimes.Add(j);
                        outGoingLine[i].TimeFinishTrval.Add(j + timeSpan);
                    }
                }
            }
            catch
            {

            }
            return outGoingLine;
        }

        public void AddLineExit(BO.OutGoingLine Line)
        {
            DO.OutGoingLine outGoingLine = new DO.OutGoingLine();
            Line.CopyPropertiesTo(outGoingLine);
            dl.AddLineExit(outGoingLine);
            try
            {
               /* if (Line.LineStartTime > Line.LineFinishTime || Line.LineFrequencyTime > Line.LineFinishTime - Line.LineStartTime)
                {
                    return;
                }
                else
                {
                    DO.OutGoingLine outGoingLine = new DO.OutGoingLine();
                    Line.CopyPropertiesTo(outGoingLine);
                    dl.AddLineExit(outGoingLine);
                }*/
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        /*
        #region Accident
        BO.Accident AccidentDoBoAdapter(DO.Accident AccidentDO)
        {
            BO.Accident AccidentBO = new BO.Accident();
            string LicenseNum = AccidentDO.LicenseNum;
            try
            {
                AccidentDO = dl.GetAccident(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("Wrong Accident's details", Ex);
            }

            AccidentDO.CopyPropertiesTo(AccidentBO);
            return AccidentBO;
        }
        public IEnumerable<BO.Accident> GetAllAccident()
        {
            return from AccidentDO in dl.GetAllAccidentsList()
                   orderby AccidentDO.AccidentDate
                   select AccidentDoBoAdapter(AccidentDO);
        }
        public void AddAccident(BO.Accident Accident)
        {
            DO.Accident AccidentDO = new DO.Accident();

            Accident.CopyPropertiesTo(AccidentDO);

            try
            {
                dl.AddAccident(AccidentDO);
            }
            catch (DO.BadAccident ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadAccident("accident details are wrong", Ex);
            }
        }
        public void DeleteAccident(int Accidentnum)
        {
            try
            {
                int id = Accidentnum;
                dl.DeleteAccident(id);
                //  dl.DeleteBusStationLine(id);
            }
            catch (DO.BadAccident ex)
            {
                throw new BO.BadAccident("Accident wring details", ex.ToString());
            }
        }
        public IEnumerable<BO.Accident> GetAccident(string LicenseNum)
        {
            IEnumerable<DO.Accident> AccidentDO;
            try
            {
                AccidentDO = dl.GetAllAccidentsList(LicenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                string Ex = ex.ToString();
                throw new BO.BadBusIdException("There arent any exist acciddent , thanx god", Ex);
            }
            return AccidentDoBoAdapter(AccidentDO);
        }

        #endregion
        */


    }
}