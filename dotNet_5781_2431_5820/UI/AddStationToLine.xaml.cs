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
        BO.BusLine tempBL;

        public AddStationToLine(BO.FollowingStations FStations, BO.BusLine bs)
        {
            InitializeComponent();
            tempBL = bs;
            tempFS = FStations;
            IEnumerable<BO.Station> s = bl.GetAllStations().Where(sf => bs.stationsList.Contains(sf) == false);
            stationList.ItemsSource = s;
        }

        private void addstation_Click(object sender, RoutedEventArgs e)
        {
            if(stationList.SelectedItem!=null)
            {
                    BO.Station AddedS = stationList.SelectedItem as BO.Station;
                    BO.FollowingStations first = bl.GetFollowingStation(AddedS.CodeStation, tempFS.FirstStationCode);
                    BO.FollowingStations second = bl.GetFollowingStation(AddedS.CodeStation, tempFS.FirstStationCode);
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
                        FollowingStationsDistace win = new FollowingStationsDistace(first, second);
                        win.Show();
                    }
                }
                tempBL.stationsList.
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Please choose on a station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
        }
    }
}
