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
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
        PO.BusLine MyBusLine;
        public ObservableCollection<PO.BusLine> ts;
        public ObservableCollection<BO.BusStationLine> bs;
       
        //BO.Line saveTheCurrentDetails;//a line to save the original details of the bus in case the update is illegal:
        
        public BusLineWindow(IBL _bl)
        {     
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ts = new ObservableCollection<PO.BusLine>();
            bl = _bl;
                      RefreshAllLinesComboBox();        
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
        }
                
        void RefreshAllLineStationsOfLineGrid()
        {
            bs = new ObservableCollection<BO.BusStationLine>();
            foreach (var item in bl.GetBusStationLineList(MyBusLine.ID.ToString()))
            {
                bs.Add(item);
            }
            lineStationDataGrid.ItemsSource = bs;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            MyBusLine = (PO.BusLine)BusLineComboBox.SelectedItem;
            if (MyBusLine!= null)
            {
                BusLines.DataContext = MyBusLine;
                RefreshAllLineStationsOfLineGrid();
            }
        }

        private void BusLineUpdate_Click(object sender, RoutedEventArgs e)
        {
                MyBusLine = (PO.BusLine)BusLineComboBox.SelectedItem;
                UpdateBusLineWindow win = new UpdateBusLineWindow(MyBusLine,bl,this);
                win.Show();
        }

        private void BusLineDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                if (MyBusLine != null)
                {
                    bl.DeleteBusLine(MyBusLine.BusNum);
                    PO.BusLine g = ts.ToList().Find(i => i.ID == MyBusLine.ID);
                    ts.Remove(g);
                }
            }
            catch (BO.BadStationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BusLineAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddLine addLineWindow = new AddLine(bl,this);//we sent the line to a new window we created named AddLine
               // addLineWindow.Closing += addLineWindow_Closing;
                addLineWindow.ShowDialog();
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addLineWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!(sender as AddLine).AllFieldsWereFilled)
                    throw new BO.BadBusLineIdException("cannot add the line since not all fields were filled");

                if (!(sender as AddLine).thereIsATrip)
                    throw new BO.BadBusLineIdException("cannot add the line, add at least one trip of the line!");

                BO.BusLine newLineBO = (sender as AddLine).addedLine;
                bl.AddBusLine(newLineBO);

                RefreshAllLinesComboBox();
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStationException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void lineStationDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void btDeleteLineStationFromThisLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusStationLine lineStationBO = ((sender as Button).DataContext as BO.BusStationLine);
                bl.DeleteBusStationLine(lineStationBO.BusStationNum, int.Parse(lineStationBO.ID), lineStationBO.IndexInLine);
                
                bs.Clear();
                foreach (var item in bl.GetBusStationLineList(lineStationBO.ID))
                {
                    bs.Add(item);
                }
            }
            catch (BO.BadBusStationLineCodeException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void AddStationToLine_Click(object sender, RoutedEventArgs e)
        {
 
            if(lineStationDataGrid.SelectedItem!=null)
            {
                BO.BusStationLine bs = lineStationDataGrid.SelectedItem as BO.BusStationLine;                
                BO.BusStationLine bs1 = this.bs[bs.IndexInLine + 1];
                 BO.FollowingStations s = lineStationDataGrid.SelectedItem as BO.FollowingStations;
                try
                {
                    BO.FollowingStations tempS =bl.GetFollowingStation(bs.BusStationNum,bs1.BusStationNum);
                    AddStationToLine win = new AddStationToLine(bl,tempS, MyBusLine,bs.IndexInLine+1,this);

                    win.Show();
                }
                catch(BO.BadStationNumException)
                {
                    MessageBoxResult res = MessageBox.Show("The Station doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }            
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Please press on a station", "Error", MessageBoxButton.OK);
            }
        }
        private void update_click(object sender, RoutedEventArgs e)
        {
            

            FollowingStationsDistace fss = new FollowingStationsDistace
                (new BO.FollowingStations { FirstStationCode = ((sender as Button).DataContext as BO.BusStationLine).BusStationNum,
                    SecondStationCode = bs[ bs.ToList().FindIndex(i => i.IndexInLine == ((sender as Button)
                    .DataContext as BO.BusStationLine).IndexInLine + 1)].BusStationNum,
                    AverageDrivingTime = ((sender as Button)
                    .DataContext as BO.BusStationLine).AverageDrivingTime, Distance =
                    ((sender as Button).DataContext as BO.BusStationLine).Distance } ,bl ,this);
            fss.Show();
        }

        private void area_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lineStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
