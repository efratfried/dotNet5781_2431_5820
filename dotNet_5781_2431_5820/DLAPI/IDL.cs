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
        void AddBus(int BusNum);
        Bus GetBus(int BusNum);
        void UpdateBus(int BusNum);
        void DeleteBus(int BusNum);
        #endregion
        #region Station
        void AddStation(int CodeStation);
        void GetStation(int CodeStation);
        void UpdateStation(int CodeStation);
        void DeleteStation(int CodeStation);
        #endregion
        #region User
        void AddUser(string Name, int Password);
        User GetUserName(int password);
        User GetUserPassword(int PAssword);
        void UpdateUser(string Name, int Password);
        void DeleteUser(string Name, int Password);
        #endregion
        #region Adress
        void AddAdress(string Street, int num, string city);
        void GetAdress(string Street, int num, string city);
        void UpdateAdress(string Street, int num, string city);
        void DeleteAdress(string Street, int num, string city);
        #endregion

        //Create

        void AddBusLine(int BusNum);
        void AddBusStationLine(int BusNum, int CodeStation);
        void AddDrivingLine(int BusNum);
        void AddLocation(double latitude,double longtitude);
        void AddOutGoingLine(int BusNum);
        void AddUserDrive(string name,int password);

        //Request
        
        BusLine GetBusLine(int BusNum);
        BusStationLine GetBusStationLine(int BusNum, int CodeStation);
        DrivingBus GetDrivingBus(int BusNum);
        FollowingStations GetFollowingStations(int CodeStation);
        Location GetLocation(double latitude, double longtitude);
        OutGoingLine GetOutGoingLine(int BusNum);
        
       
        
        //Update
        
        
       
        
        void UpdateBusLine(int BusNum);
        void UpdateBusStationLine(int BusNum,int CodeStation);
        void UpdateDrivingLine(int BusNum);
        void UpdateLocation(double latitude, double longtitude);
        void UpdateOutGoingLine(int BusNum);
        void UpdateUserDrive(string name);

        //Remove
        
        
        
        
        void DeleteBusLine(int BusNum);
        void DeleteBusStationLine(int BusNum,int CodeStation);
        void DeleteDrivingLine(int BusNum);
        void DeleteLocation(double latitude, double longtitude);
        void DeleteOutGoingLine(int BusNum);
        void DeleteUserDrive(string name);
        
    }
}
