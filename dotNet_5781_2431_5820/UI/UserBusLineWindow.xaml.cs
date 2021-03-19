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
using PL;
using BLAPI;
using System.Collections.ObjectModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserBusLineWindow.xaml
    /// </summary>
    public partial class UserBusLineWindow : Window
    {
        IBL bl;
        PO.BusLine MyBusLine;
        BO.BusStationLine MybusStation;
        public ObservableCollection<PO.BusLine> ts;
        public ObservableCollection<BO.BusStationLine> bs;
        public UserBusLineWindow(IBL _bl)
        {
            ts = new ObservableCollection<PO.BusLine>();
            InitializeComponent();
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = _bl;

            RefreshAllLinesComboBox();
            //area.DisplayMemberPath = "Area";
            //BusLineComboBox.DisplayMemberPath = "BusNumber";//show only specific Property of object
            //BusLineComboBox.SelectedValuePath = "LineId";//selection return only specific Property of object
            //BusLineComboBox.SelectedIndex = 0; //index of the object to be selected

            lineStationDataGrid.IsReadOnly = true;
        }
        void RefreshAllLinesComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.BusLine> busLes = bl.GetBusLines().ToList();
            for (int i = 0; i < busLes.Count; i++)
            {
                PO.BusLine busLes2 = new PO.BusLine();
                busLes[i].DeepCopyTo(busLes2);

                ts.Add(busLes2);
            }
            BusLineComboBox.ItemsSource = ts;
            BusLineComboBox.DisplayMemberPath = "BusNum";
            BusLineComboBox.SelectedIndex = 0;

            // IEnumerable<PO.BusLine> buslines = bl.GetBusLines().Cast<PO.BusLine>();
            // BusLineComboBox.ItemsSource = buslines;
        }
        void RefreshAllLineStationsOfLineGrid()
        {
            bs = new ObservableCollection<BO.BusStationLine>();
            foreach (var item in bl.GetBusStationLineList(MyBusLine.ID.ToString()))
            {
                bs.Add(item);
            }
            lineStationDataGrid.ItemsSource = bs;
            //IEnumerable<PO.BusLine> busLines;
            //lineStationDataGrid.ItemsSource = bl.GetBusStationLineList(MyBusLine.ID.ToString());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyBusLine = (PO.BusLine)BusLineComboBox.SelectedItem;
            BusLines.DataContext = MyBusLine;
            RefreshAllLineStationsOfLineGrid();
        }
        private void busNumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
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

        private void firstStationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
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

        private void lastStationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
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

        private void lineStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void lineStationDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void lineStationDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
