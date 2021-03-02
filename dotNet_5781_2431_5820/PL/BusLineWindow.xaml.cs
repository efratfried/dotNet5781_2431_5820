﻿using System;
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
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
        PO.BusLine MyBusLine;
        //BO.Line saveTheCurrentDetails;//a line to save the original details of the bus in case the update is illegal:

        public BusLineWindow(IBL _bl)
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
            lineStationDataGrid.DataContext = bl.GetAllLineStationsPerLine(MyBusLine.LicenseNum);
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

        private void BusLineUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MyBusLine != null && busNumberTextBox.Text != "" && firstStationTextBox.Text != "" && lastStationTextBox.Text != "")
                {
                    UpdateWindow win = new UpdateWindow();
                    win.Show();
                    MyBusLine.BusNum = int.Parse(busNumberTextBox.Text);
                    MyBusLine.FirstStation = firstStationTextBox.Text;
                    MyBusLine.LastStation = lastStationTextBox.Text;
                    MyBusLine.Area = areaComboBox.;
                    /*PO.BusLine NewLine;//a local line, to save the changes that the user made in line's fields.
                    NewLine.BusNum = int.Parse(busNumberTextBox.Text);
                    NewLine.Area = (BO.Area)(areaComboBox.SelectedIndex);
                    NewLine.FirstStation = int.Parse(firstStationTextBox.Text);
                    NewLine.LastStation = int.Parse(lastStationTextBox.Text);*/

                    if (NewLine != null)
                        bl.UpdateBusLinePersonalDetails(NewLine);

                    MyBusLine = NewLine;//if succeded, change currLine fields to be as the line. if not- dont do that.
                    RefreshAllLinesComboBox();//refresh the combo box to save the changes!!!
                }
                else//if not all fields are full
                {
                    throw new BO.BadBusLineIdException("cannot update the line since not all fields were filled");
                }

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

                    RefreshAllLineStationsOfLineGrid();
                    RefreshAllLinesComboBox();
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
                AddLine addLineWindow = new AddLine(bl);//we sent the line to a new window we created named AddLine
                addLineWindow.Closing += addLineWindow_Closing;
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
                bl.AddBusLine(newLineBO, (sender as AddLine).listTrips);

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
                bl.DeleteBusStationLine(lineStationBO.BusStationNum, MyBusLine.BusNum);
                RefreshAllLineStationsOfLineGrid();
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

        private void lineStationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}