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
using PL;
namespace UI
{
    /// <summary>
    /// Interaction logic for UserStationWindow.xaml
    /// </summary>
    public partial class UserStationWindow : Window
    {
        public ObservableCollection<PO.Station> ts;
        public ObservableCollection<BO.BusLine> BS;
        public IBL bl;
        PO.Station MyStation;
        public UserStationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            ts = new ObservableCollection<PO.Station>();
            RefreshAllStationsComboBox();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // StationComboBox.DisplayMemberPath = "Name";//show only specific Property of object
            //StationComboBox.SelectedValuePath = "Code";//selection return only specific Property of object
            //StationComboBox.SelectedIndex = 0; //index of the object to be selected
           

            linesDataGrid.IsReadOnly = true;
        }
        private void RefreshAllStationsComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.Station> sta = bl.GetAllStations().ToList();
            for (int i = 0; i < sta.Count; i++)
            {
                PO.Station sta1 = new PO.Station();
                sta[i].DeepCopyTo(sta1);

                ts.Add(sta1);
            }
            StationComboBox.ItemsSource = ts;
            StationComboBox.DisplayMemberPath = "StationName";
            StationComboBox.SelectedIndex = 0;
        }
        private void RefreshAllLineStationsOfLineGrid(string code)
        {
            BS = new ObservableCollection<BO.BusLine>();
            foreach (var item in bl.GetAllLinesPerStation(int.Parse(code)))
            {
                BS.Add(item);
            }
            linesDataGrid.ItemsSource = BS;
        }
        /*private void RefreshAllLinesOfStationGrid()
        {
            IEnumerable<PO.Station> MyBusinstation = bl.GetAllLinesPerStation(int.Parse(MyStation.CodeStation)).Cast<PO.Station>().ToList();
            linesDataGrid.ItemsSource = MyBusinstation;
        }*/

        private void CBChosenStat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PO.Station MyStation1 = (PO.Station)StationComboBox.SelectedItem;
            stationgrid.DataContext = MyStation1;
            // RefreshAllLinesOfStationGrid();
            if (MyStation1.CodeStation != null)
            {
                linesDataGrid.DataContext = bl.GetAllLinesPerStation(int.Parse(MyStation1.CodeStation));
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

        private void linesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
