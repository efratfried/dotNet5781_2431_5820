using BLAPI;
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
using BO;
using UI;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for busseswindow.xaml
    /// </summary>
    public partial class busseswindow : Window
    {
        IBL bl;
        PO.Bus currentbus;
        public ObservableCollection<BO.Accident> accident;
        public busseswindow(IBL _bl)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = _bl;
            Refreshbusses_listComboBox();
        }

        void RefreshBusDetailsGrid()
        {
            BusDetailsGrid.DataContext = bl.GetBus(currentbus.LicenseNum);
        }
    
        private void Refreshbusses_listComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.Bus> buses = bl.GetAllBuss().ToList();
            List<PO.Bus> buses1 = new List<PO.Bus>();
            for (int i = 0; i < buses.Count; i++)
            {
                PO.Bus buses2 = new PO.Bus();
                buses[i].DeepCopyTo(buses2);

               buses1.Add(buses2);
            }
            busses_list.ItemsSource = buses1;
            busses_list.DisplayMemberPath = "LicenseNum";
            busses_list.SelectedIndex = 0;
            Licensenumbus.Text = currentbus.LicenseNum;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentbus = (PO.Bus)busses_list.SelectedItem;
            ___Bus_Window_.DataContext = currentbus;
            Licensenumbus.Text = currentbus.LicenseNum;
        }


        private void start_driving_Click(object sender, RoutedEventArgs e)
        {
                delete_bus.IsEnabled = false;
                update_bus.IsEnabled = false;
                start_driving.IsEnabled = false;
                start_filling_foul.IsEnabled = false;
            AddOutGoingLine o = new AddOutGoingLine();
            o.Show();
        }

        private void start_filling_foul_Click(object sender, RoutedEventArgs e)
        {
            aviability_status.Text = "UnAvailable";
        }

        private void update_bus_Click(object sender, RoutedEventArgs e)
        {
            UpdateBus upbus = new UpdateBus(currentbus,bl);
            upbus.ShowDialog();
        }

        private void delete_bus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteBus(currentbus.LicenseNum.ToString());
                Refreshbusses_listComboBox();
            }
            catch (BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Could'nt delete the bus", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_bus_Click(object sender, RoutedEventArgs e)
        {
            AddBus ab = new AddBus(bl);
            ab.ShowDialog();

        }

        private void aviability_status_TextBlock(object sender, DependencyPropertyChangedEventArgs e)
        {
       //     if(currentbus.sts)
        }

        private void foul_status_TextBlock(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void Accident_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void add_accident_Click(object sender, RoutedEventArgs e)
        {
            AddAccident win = new AddAccident(currentbus,bl);
            win.ShowDialog();
        }

        //  currentbus = DataContext as Bus;
    }
}


