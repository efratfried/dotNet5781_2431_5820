using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;

namespace DLAPI
{//basic on the "CRUD" rule - Create_Request_Update_Delete
    public interface IDL
    {
        #region Bus
        int AddBus(int BusNum);
        Bus GetBus(int BusNum);
        void UpdateBus(int BusNum);
        void DeleteBus(int BusNum);
        #endregion
        #region Station
        int AddStation(int CodeStation);
        Station GetStation(int CodeStation);
        void UpdateStation(int CodeStation);
        void DeleteStation(int CodeStation);
        #endregion
        #region User
        int AddUser(string Name, int Password);
        User GetUserName(int password);
        User GetUserPassword(int PAssword);
        void UpdateUser(string Name, int Password);
        void DeleteUser(string Name, int Password);
        #endregion
        #region Adress
        int AddAdress(string Street, int num, string city);
        void GetAdress(string Street, int num, string city);
        void UpdateAdress(string Street, int num, string city);
        void DeleteAdress(string Street, int num, string city);
        #endregion
        #region BusLine
        int AddBusLine(int BusNum);
        BusLine GetBusLine(int BusNum);
        void UpdateBusLine(int BusNum);
        void DeleteBusLine(int BusNum);
        #endregion
        #region BusStationLine
        int AddBusStationLine(int BusNum, int CodeStation);
        BusStationLine GetBusStationLine(int BusNum, int CodeStation);
        void UpdateBusStationLine(int BusNum, int CodeStation);
        void DeleteBusStationLine(int BusNum, int CodeStation);
        #endregion
        #region DrivingLine
        int AddDrivingLine(int BusNum);
        DrivingBus GetDrivingBus(int BusNum);
        void UpdateDrivingLine(int BusNum);
        void DeleteDrivingLine(int BusNum);
        #endregion
        #region Location
        int AddLocation(double latitude, double longtitude);
        Location GetLocation(double latitude, double longtitude);
        void UpdateLocation(double latitude, double longtitude);
        void DeleteLocation(double latitude, double longtitude);
        #endregion
        #region OutGoingLine
        int AddOutGoingLine(int BusNum);
        OutGoingLine GetOutGoingLine(int BusNum);
        void UpdateOutGoingLine(int BusNum);
        void DeleteOutGoingLine(int BusNum);
        #endregion
        #region UserDrive
        int AddUserDrive(string name, int password);
        UserDrive GetUserDrive(string Name);
        void UpdateUserDrive(string name);
        void DeleteUserDrive(string name);

        #endregion
        #region FollowingStations
        int AddFolowingStations(int FirstCodeStation, int SecondCodeStation);
        FollowingStations GetFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void UpdateFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void DeleteFollowingStations(int FirstCodeStation, int SecondCodeStation);
        #endregion
    }
}
