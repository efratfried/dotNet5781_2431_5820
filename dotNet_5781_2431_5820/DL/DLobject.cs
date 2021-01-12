using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLAPI;
//using DO;
using DS;

namespace DL
{
        sealed class DLObject : IDL    //internal

        {
            #region singelton
            static readonly DLObject instance = new DLObject();
            static DLObject() { }// static ctor to ensure instance init is done just before first usage
            DLObject() { } // default => private
            public static DLObject Instance { get => instance; }// The public Instance probusty to use
            #endregion

            //Implement IDL methods, CRUD
            #region Bus
            public IEnumerable<DO.Bus> GetAllBusses()
            {//returns all members in list
                return from Bus in DataSource.BusList
                       select Bus.Clone();
            }
            public IEnumerable<DO.Bus> GetAllBusses(Predicate<DO.Bus> predicate)
            {
                            throw new NotImplementedException();//it means we need to put exeption here;
            }
            public DO.Bus GetBus(int LicenseNum)
            {
                DO.Bus bus = DataSource.BusList.Find(B => B.LicenseNum == LicenseNum);

                if (bus != null)
                    return bus.Clone();
                else
                    throw new DO.BadBusNumException(LicenseNum, $"no Bus has License Num: {LicenseNum}");
            }
            public void AddBus(DO.Bus bus)
            { //need a check if actually it is ==bus.---- or only ==licensnum
                if (DataSource.BusList.FirstOrDefault(B => B.LicenseNum == bus.LicenseNum) != null)
                    throw new DO.BadLicenseNumException(bus.LicenseNum, "Duplicate bus LicenseNum");
                DataSource.BusList.Add(bus.Clone());
            }
            public void UpdateBus(DO.Bus Bus)
            {
                DO.Bus bus = DataSource.BusList.Find(b => b.LicenseNum == Bus.LicenseNum);

                if (bus != null)
                {
                    DataSource.BusList.Remove(bus);
                    DataSource.BusList.Add(Bus.Clone());
                }
                else
                    throw new DO.BadLicenseNumException(Bus.LicenseNum /, $"bad Bus id: {Bus.LicenseNum}");
            }
            public void UpdateBus(int Num, Action<DO.Bus> update) //method that knows to updt specific fields in Bus
            {
                            throw new NotImplementedException();//it means we need to put exeption here;
            }
            public void DeleteBus(int Num)
            {
                DO.Bus bus = DataSource.BusList.Find(p => p.LicenseNum == Num);

                if (bus != null)
                {
                    DataSource.BusList.Remove(bus);
                }
                else
                    throw new DO.BadLicenseNumException(Num, $"bad Bus id: {Num}");
            }

            #endregion Bus 
            #region station
            public DO.Station GetStation(int id)
            {
                return DataSource.StationLists.Find(c => c.CodeStation == id).Clone();
            }
            public IEnumerable<DO.Station> GetAllStations()
            {
            return from station in DataSource.StationLists
                   select station.Clone();
            }
        #endregion station
            #region BusLine
        public DO.BusLine GetBusLine(int id)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
            if (busl != null)
                return busl.Clone();
            else
                throw new DO.BadIDException(id, $"bad BusLine id: {id}");
        }
        public void AddBusLine(DO.BusLine BusLine)
        {
            if (DataSource.BusLineList.FirstOrDefault(s => s.ID == BusLine.ID) != null)
                throw new DO.BadIDException(BusLine.ID, "Duplicate BusLine ID");
            if (DataSource.ListBuss.FirstOrDefault(p => p.ID == BusLine.ID) == null)
                throw new DO.BadIDException(BusLine.ID, "Missing Bus ID");
            DataSource.BusLineList.Add(BusLine.Clone());
        }
        public IEnumerable<DO.BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.BusLineList
                   select BusLine.Clone();
        }
        public IEnumerable<object> GetBusLineFields(Func<int, string, object> generate)
        {
            return from BusLine in DataSource.BusLineList
                   select generate(BusLine.ID, GetBus(BusLine.ID).Name);
        }

        public IEnumerable<object> GetBusLineListWithSelectedFields(Func<DO.BusLine, object> generate)
        {
            return from BusLine in DataSource.BusLineList
                   select generate(BusLine);
        }
        public void UpdateBusLine(DO.BusLine BusLine)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == BusLine.ID);
            if (busl != null)
            {
                DataSource.BusLineList.Remove(busl);
                DataSource.BusLineList.Add(BusLine.Clone());
            }
            else
                throw new DO.BadIDException(BusLine.ID, $"bad BusLine id: {BusLine.ID}");
        }

        public void UpdateBusLine(int id, Action<DO.BusLine> update)
        {
                        throw new NotImplementedException();//it means we need to put exeption here;//it means we need to put exeption here
        }

        public void DeleteBusLine(int id)
        {
            DO.BusLine busl = DataSource.BusLineList.Find(p => p.ID == id);

            if (busl != null)
            {
                DataSource.BusLineList.Remove(busl);
            }
            else
                throw new DO.BadIDException(id, $"bad BusLine id: {id}");
        }
        #endregion
            #region BusStationLine
        public IEnumerable<DO.BusStationLine> GetBusLinesInStationList(Predicate<DO.BusStationLine> predicate)
        {
            //option A - not good!!! 
            //produces final list instead of deferred query and does not allow probus cloning:
            // return DataSource.BusStationLineList.FindAll(predicate);

            // option B - ok!!
            //Returns deferred query + clone:
            //return DataSource.BusStationLineList.Where(sic => predicate(sic)).Select(sic => sic.Clone());

            // option c - ok!!
            //Returns deferred query + clone:
            return from sic in DataSource.BusStationLineList
                   where predicate(sic)
                   select sic.Clone();
        }
        public void AddBusStationLine(int ID, int BusStationNum, float IndexInLine = 0)
        {
            if (DataSource.BusStationLineList.FirstOrDefault(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum)) != null)
                throw new DO.BadIDBusStationNumException(ID, BusStationNum, "Bus ID is already registered to Station ID");
            DO.BusStationLine sic = new DO.BusStationLine() { ID = ID, BusStationNum = BusStationNum, IndexInLine = IndexInLine };
            DataSource.BusStationLineList.Add(sic);
        }

        public void UpdateBusLineIndexInLineInStation(int ID, int BusStationNum, int IndexInLine)
        {
            DO.BusStationLine sic = DataSource.BusStationLineList.Find(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                sic.IndexInLine = IndexInLine;
            }
            else
                throw new DO.BadIDBusStationNumException(ID, BusStationNum, "Bus ID is NOT registered to Station ID");
        }

        public void DeleteBusStationLine(int ID, int BusStationNum)
        {
            DO.BusStationLine sic = DataSource.BusStationLineList.Find(cis => (cis.ID == ID && cis.BusStationNum == BusStationNum));

            if (sic != null)
            {
                DataSource.BusStationLineList.Remove(sic);
            }
            else
                throw new DO.BadIDBusStationNumException(ID, BusStationNum, "Bus ID is NOT registered to Station ID");
        }
        public void DeleteBusLineFromAllStations(int ID)
        {
            DataSource.BusStationLineList.RemoveAll(p => p.ID == ID);
        }

        #endregion BusStationLine
            #region DrivingBus
        //yes we use outgoinglist in it 
        public IEnumerable<DO.OutGoingLine> GetOutGoingLineList(Predicate<DO.GetOutGoingLine> predicate)
        {
            //Returns deferred query + clone:
            return from sic in DataSource.OutGoingLineList
                   where predicate(sic)
                   select sic.Clone();
        }
        #endregion
            
        }
}
