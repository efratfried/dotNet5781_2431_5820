using System;
using System.Collections.Generic;
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
        IBL bl;
        BO.FollowingStations tempFS;
        PO.BusLine tempBL;

        public AddStationToLine(BO.FollowingStations FStations, PO.BusLine bs)
        {
            InitializeComponent();
            tempBL = bs;
            tempFS = FStations;
           // IEnumerable<BO.BusStationLine> s = bl.GetAllBusStationLines(bs.BusNum).Where(sf => bs.stationsList.Contains(sf) == false);
            //stationList.ItemsSource = s;
        }

        private void addstation_Click(object sender, RoutedEventArgs e)
        {
            if(stationList.SelectedItem!=null)
            {
                    BO.BusStationLine AddedS = stationList.SelectedItem as BO.BusStationLine;
                    BO.FollowingStations first = bl.GetFollowingStation(AddedS.BusStationNum, tempFS.FirstStationCode);
                    BO.FollowingStations second = bl.GetFollowingStation(AddedS.BusStationNum, tempFS.FirstStationCode);

                if (first != null && second != null)
                {
                    //nothing happens because the bond is already exist
                }
                else//we are trying to make two new following stations & to savet 
                {
                   try
                    {//in the FollowingStationsDistace window we are going to take care of the new two following stations
                        FollowingStationsDistace win = new FollowingStationsDistace(first, second);
                        win.Show();
                    }
                  
                catch (BO.BadOpenWindow)
                    {
                        MessageBoxResult res = MessageBox.Show("Could not open the window!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

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
