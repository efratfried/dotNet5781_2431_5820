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
        int AddBus(Bus Bus);
        Bus GetBus(int BusNum);
        void UpdateBus(Bus Bus);
        void DeleteBus(Bus Bus);
        #endregion
        #region Station
        int AddStation(Station Station);
        Station GetStation(int CodeStation);
        void UpdateStation(Station Station);
        void DeleteStation(Station Station);
        #endregion
        #region User
        int AddUser(User User);
        User GetUserName(int password);
        User GetUserPassword(int PAssword);
        void UpdateUser(User Userr);
        void DeleteUser(User User);
        #endregion
        #region Adress
        int AddAdress(Adress Adress);
        void GetAdress(string Street, int num, string city);
        void UpdateAdress(Adress Adress);
        void DeleteAdress(Adress Adress);
        #endregion
        #region BusLine
        int AddBusLine(BusLine BusLine);
        BusLine GetBusLine(int BusNum);
        void UpdateBusLine(BusLine BusLine);
        void DeleteBusLine(BusLine BusLine);
        #endregion
        #region BusStationLine
        int AddBusStationLine(BusStationLine BusStationLine);
        BusStationLine GetBusStationLine(int BusNum, int CodeStation);
        void UpdateBusStationLine(BusStationLine BusStationLine);
        void DeleteBusStationLine(BusStationLine BusStationLine);
        #endregion
        #region DrivingBus
        int AddDrivingLine(DrivingBus DrivingBus);
        DrivingBus GetDrivingBus(int BusNum);
        void UpdateDrivingLine(DrivingBus DrivingBus);
        void DeleteDrivingLine(DrivingBus DrivingBus);
        #endregion
        #region Location
        int AddLocation(Location Location);
        Location GetLocation(double latitude, double longtitude);
        void UpdateLocation(Location Location);
        void DeleteLocation(Location Location);
        #endregion
        #region OutGoingLine
        int AddOutGoingLine(OutGoingLine OutGoingLine);
        OutGoingLine GetOutGoingLine(OutGoingLine OutGoingLine);
        void UpdateOutGoingLine(OutGoingLine OutGoingLine);
        void DeleteOutGoingLine(OutGoingLine OutGoingLine);
        #endregion
        #region UserDrive
        int AddUserDrive(UserDrive UserDrive);
        UserDrive GetUserDrive(UserDrive UserDrive);
        void UpdateUserDrive(UserDrive UserDrive);
        void DeleteUserDrive(UserDrive UserDrive);

        #endregion
        #region FollowingStations
        int AddFolowingStations(int FirstCodeStation, int SecondCodeStation);
        FollowingStations GetFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void UpdateFollowingStations(int FirstCodeStation, int SecondCodeStation);
        void DeleteFollowingStations(int FirstCodeStation, int SecondCodeStation);
        #endregion
    }
}
