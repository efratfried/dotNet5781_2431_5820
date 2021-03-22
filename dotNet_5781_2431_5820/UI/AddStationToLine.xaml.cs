using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;
namespace PL
{
    /// <summary>
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        PL.BusLineWindow bw;
        IBL bl;
        BO.FollowingStations tempFS;
        PO.BusLine tempBL;
        int i;
        public ObservableCollection<PO.Station> ts=new ObservableCollection<PO.Station>();
        public AddStationToLine(IBL _bl,BO.FollowingStations FStations, PO.BusLine bs,int index ,PL.BusLineWindow busl)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = _bl;
            bw = busl;
            i = index;
            tempBL = bs;
            tempFS = FStations;
            refreshstationList();
            // IEnumerable<BO.BusStationLine> s = bl.GetAllBusStationLines(bs.BusNum).Where(sf => bs.stationsList.Contains(sf) == false);
            //stationList.ItemsSource = s;
        }
       void refreshstationList()
        {//getting all the existing stations + all the line's station ,checking which are the same & not adding them to the combobox list
            
            List<BO.Station> station = bl.GetAllStations().ToList();
            List<BO.BusStationLine> bs = bl.GetAllBusStationLines(tempBL.BusNum).ToList();
            List<BO.Station> s = new List<BO.Station>();//=station.Where(i=>i.CodeStation!=bs)
           
            for (int i = 0; i < station.Count; i++)
            { 
               bool flag = false;
                for (int j = 0; j < bs.Count; j++)
                {                  
                    if (station[i].CodeStation == bs[j].BusStationNum)
                    {
                        flag = true;
                    }
                    else { }
                }
                if (flag == false)
                {
                    s.Add(station[i]);                   
                }
                else { }
            }

            for (int i = 0; i < s.Count; i++)
            {
                PO.Station station2 = new PO.Station();
                s[i].DeepCopyTo(station2);

                ts.Add(station2);
            }
            stationList.ItemsSource = ts;
            stationList.DisplayMemberPath = "StationName";
        }
        private void addstation_Click(object sender, RoutedEventArgs e)
        {
            if(stationList.SelectedItem!=null)
            {

                BO.BusStationLine AddedS = new BO.BusStationLine();
                PO.Station s= stationList.SelectedItem as PO.Station;
                AddedS.BusStationNum = s.CodeStation;
                AddedS.IndexInLine = i;
                AddedS.ID = tempBL.ID.ToString();
               //  BO.FollowingStations first = bl.GetFollowingStation(AddedS.BusStationNum, tempFS.FirstStationCode);
               // BO.FollowingStations second = bl.GetFollowingStation(AddedS.BusStationNum, tempFS.SecondStationCode);
                
                bl.AddBusStationLine(AddedS);
                bl.AddFollowingStation(tempFS.FirstStationCode.ToString(), AddedS.BusStationNum);
                bl.AddFollowingStation(AddedS.BusStationNum, tempFS.SecondStationCode.ToString());
                bw.bs.Clear();
                foreach (var item in bl.GetBusStationLineList(tempBL.ID.ToString()))
                {
                    bw.bs.Add(item);
                }
                BO.FollowingStations ff = bl.GetFollowingStation(tempFS.FirstStationCode.ToString(), AddedS.BusStationNum);
                BO.FollowingStations ss = bl.GetFollowingStation(AddedS.BusStationNum, tempFS.SecondStationCode.ToString());
                /*if (ff.Distance ==0)
                {
                    FollowingStationsDistace win = new FollowingStationsDistace(ff,bl);              
                    win.Show();                  
                }
                if(ss.Distance==0)
                    FollowingStationsDistace win = new FollowingStationsDistace(ss, bl);
                    win.Show();
                }*/
              
                /* if ( != null && second != null)
                 {
                     //nothing happens because the bond is already exist
                 }
                 else//we are trying to make two new following stations & to savet 
                 {
                    try
                     {//in the FollowingStationsDistace window we are going to take care of the new two following stations
                        
                     }

                 catch (BO.BadOpenWindow)
                     {
                         MessageBoxResult res = MessageBox.Show("Could not open the window!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                     }
                 }*/

                /*int index = findIndex(tempFS);
                List<BO.BusStationLine> B=tempBL.stationsList.ToList();
                B.Insert(index, AddedS);
                tempBL.stationsList=B.AsEnumerable();*/
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Please choose on a station!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void stationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /* public int findIndex(BO.FollowingStations FS)
{//tools func
    BO.Station sta1 = bl.GetStation(FS.FirstStationCode);
    BO.Station sta2 = bl.GetStation(FS.SecondStationCode);
    int indexfirst = tempBL.stationsList.ToList().IndexOf(sta1);
    int indexsecond = tempBL.stationsList.ToList().IndexOf(sta2);
    if (indexfirst > indexsecond)
        return indexsecond;
    else
        return indexfirst;
}*/
    }
}
