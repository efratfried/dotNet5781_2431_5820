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
    /// Interaction logic for BusStation.xaml
    /// </summary>
    public partial class BusStation : Window
    {
        IBL bl;
        BO.Station currStat;
        public BusStation(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
           // CBChosenStat.DisplayMemberPath = "Name";//show only specific Property of object
            //CBChosenStat.SelectedValuePath = "Code";//selection return only specific Property of object
            //CBChosenStat.SelectedIndex = 0; //index of the object to be selected
            //RefreshAllStationsComboBox();

            //linesDataGrid.IsReadOnly = true;
        }

        private void add_bus_Click(object sender, RoutedEventArgs e)
        {
            BO.Station stat = new BO.Station();//a new Station

            AddStation addStationWindow = new AddStation(stat);//we sent the station Stat to a new window we created named AddStation 
            addStationWindow.Closing += addStationWindow_Closing;
            addStationWindow.ShowDialog();
        }

        private void update_bus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currStat != null)
                    bl.UpdateStationDetails(currStat);
            }
            catch (BO.StationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void delete_bus_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                if (currStat != null)
                {
                    bl.DeleteStation(currStat.Code);

                    RefreshAllLinesOfStationGrid();
                    RefreshAllStationsComboBox();
                }
            }
            catch (BO.StationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
