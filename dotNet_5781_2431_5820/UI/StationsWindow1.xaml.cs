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
using UI;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationsWindow1.xaml
    /// </summary>
    public partial class StationsWindow1 : Window
    {
        public IBL bl;
        PO.Station MyStation;
        public ObservableCollection<PO.Station> stationlist;
        public StationsWindow1(IBL _bl)
        {
            stationlist = new ObservableCollection<PO.Station>();
            InitializeComponent();
            bl = _bl;
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //StationComboBox.DisplayMemberPath = "StationName";//show only specific Property of object
            //StationComboBox.SelectedValuePath = "CodeStation";//selection return only specific Property of object
           // StationComboBox.SelectedItem=""
            //StationComboBox.SelectedIndex = 0; //index of the object to be selected
            RefreshAllStationsComboBox();
            RefreshAllLinesOfStationGrid();

            //linesDataGrid = true;
        }

        private void RefreshAllStationsComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.Station> sta = bl.GetAllStations().ToList();
            List<PO.Station> sta1 = new List<PO.Station>();
            for (int i = 0; i < sta.Count; i++)
            {
                PO.Station sta2 = new PO.Station();
                sta[i].DeepCopyTo(sta2);

                sta1.Add(sta2);
            }
            StationComboBox.ItemsSource = sta1;
            //StationComboBox.DisplayMemberPath = "CodeStation";
            StationComboBox.DisplayMemberPath = "StationName";
            StationComboBox.SelectedIndex = 0;
        }

        private void RefreshAllLinesOfStationGrid()
        {
            linesDataGrid.DataContext = bl.GetBusStationLineList(MyStation.CodeStation);
        }

        private void StationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyStation = (PO.Station)StationComboBox.SelectedItem;
            StationComboBox.DataContext = MyStation;
            RefreshAllLinesOfStationGrid();
        }
       /* private void RefreshgridOneStation()
        {
            gridOneStation.DataContext = bl.GetStation(MyStation.CodeStation);
        }*/

        private void StationUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.Station s = MyStation;
                if (addressTextBox.Text != "" && nameTextBox.Text != "" && longitudeTextBox.Text != "" && lattitudeTextBox.Text != "")
                {

                    //BO.Station newStat = new BO.Station();//a local station, to save the changes that the user made in station's fields.
                    s.CodeStation = MyStation.CodeStation;
                    s.Adress.Address = addressTextBox.Text;
                    s.StationName = nameTextBox.Text;
                    s.longitude = double.Parse(longitudeTextBox.Text);
                    s.Latitude = double.Parse(lattitudeTextBox.Text);
                    if (s != null)
                    {
                        BO.Station temp = new BO.Station();
                        s.DeepCopyTo(temp);
                        bl.UpdateStationPersonalDetails(temp);
                    }


                    MyStation = s;//if succeded, change MyStation fields to be as the new stat. if not- dont do that.
                    RefreshAllStationsComboBox();//to save the changes
                }
                else//if not all fields are full
                {
                    throw new BO.BadStationException("cannot update the station since not all fields were filled");
                }

            }
            catch (BO.BadStationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StationDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                if (MyStation != null)
                {
                    bl.DeleteStation(MyStation.CodeStation);

                    //RefreshAllLinesOfStationGrid();
                    RefreshAllStationsComboBox();
                }
            }
            catch (BO.BadStationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StationAdd_Click(object sender, RoutedEventArgs e)
        {
            BO.Station stat = new BO.Station();//a new Station

            AddStation addStationWindow = new AddStation(stat);//we sent the station Stat to a new window we created named AddStation 
            addStationWindow.Closing += addStationWindow_Closing;
            addStationWindow.ShowDialog();
        }

        private void addStationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!(sender as AddStation).AllFieldsWereFilled)
                { //if not all the fields are full
                    throw new BO.BadStationException("cannot add the station since not all fields were filled");
                }

                BO.Station newStationBO = (sender as AddStation).addedStat;
                bl.AddStation(newStationBO);

                RefreshAllStationsComboBox();
            }
            catch (BO.BadStationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btDeleteStationFromThisLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLine lineBO = ((sender as Button).DataContext as BO.BusLine);
                bl.DeleteStationFromLine(lineBO, MyStation.CodeStation);
                //RefreshAllLinesOfStationGrid();
            }
            catch (BO.BadBusStationLineCodeException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void longitudeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
            {
                return;
            }
            if (e.Key == Key.OemPeriod)//allow "." for decimal
            {
                return;
            }

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))//if c is a digit- we need to check it is not a char that apperas on the digit(when shift/alt/ctrl are down)
            {
                if (!(Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)
                  || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)
                  || Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    //if no one of them is down- its okay. its a number.
                    return;
                }
            }

            //no other keys are allowed
            e.Handled = true;//if handeled=true, the char wont be added to the pakad, since as we checked, it is not a number
        }

        private void lattitudeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
            {
                return;
            }
            if (e.Key == Key.OemPeriod)//allow "." for decimal
            {
                return;
            }

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))//if c is a digit- we need to check it is not a char that apperas on the digit(when shift/alt/ctrl are down)
            {
                if (!(Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)
                  || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)
                  || Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    //if no one of them is down- its okay. its a number.
                    return;
                }
            }

            //no other keys are allowed
            e.Handled = true;//if handeled=true, the char wont be added to the pakad, since as we checked, it is not a number

        }

        private void addressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
