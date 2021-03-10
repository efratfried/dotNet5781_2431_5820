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
        public UserBusLineWindow(IBL _bl)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = _bl;

            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            BusLineComboBox.DisplayMemberPath = "BusNumber";//show only specific Property of object
            BusLineComboBox.SelectedValuePath = "LineId";//selection return only specific Property of object
            BusLineComboBox.SelectedIndex = 0; //index of the object to be selected
            RefreshAllLinesComboBox();
            lineStationDataGrid.IsReadOnly = true;
        }
        void RefreshAllLinesComboBox()//refresh the combobox each time the user changes the selection 
        {
            IEnumerable<PO.BusLine> buslines = bl.GetBusLines().Cast<PO.BusLine>();
            BusLineComboBox.ItemsSource = buslines;
        }

        void RefreshAllLineStationsOfLineGrid()
        {
            IEnumerable<PO.BusLine> busLines;
            //    lineStationDataGrid.DataContext = bl.GetAllBusStationLinesPerLine(MyBusLine.BusNum);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyBusLine = BusLineComboBox.SelectedItem as PO.BusLine;

            gridOneLine.DataContext = MyBusLine;

            if (MyBusLine != null)
            {
                RefreshAllLineStationsOfLineGrid();
            }
        }
        void lineStationDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
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

    }
}
