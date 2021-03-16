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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateBusLineWindow : Window
    {
        IBL bl;
        PO.BusLine TempBusLine;
        public UpdateBusLineWindow(PO.BusLine MybusLine,IBL _bl)
        {
            bl = _bl;
            InitializeComponent();
            TempBusLine = MybusLine;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            areaComboBox.SelectedIndex = 0;
            //busNumberTextBox.Text = TempBusLine.BusNum.ToString();
            firstStationComboBox.DataContext = TempBusLine.FirstStation;
            lastStationComboBox.DataContext = TempBusLine.LastStation;
        }

        /*try
                    {
                        if (busNumberTextBox.Text!="" && firstStationTextBox.Text!="" && lastStationTextBox.Text!="")
                        {
                            BO.Line NewLine = new BO.Line();//a local line, to save the changes that the user made in line's fields.
                            NewLine.BusNumber = int.Parse(busNumberTextBox.Text);
                            NewLine.Area = (BO.Areas)(areaComboBox.SelectedIndex);
                            NewLine.FirstStation = int.Parse(firstStationTextBox.Text);
                            NewLine.LastStation = int.Parse(lastStationTextBox.Text);
                            NewLine.LineId = currLine.LineId;

                            if (NewLine != null)
                                bl.UpdateLineDetails(NewLine);

                            currLine = NewLine;//if succeded, change currLine fields to be as the line. if not- dont do that.
                            RefreshAllLinesComboBox();//refresh the combo box to save the changes!!!
                        }
                        else//if not all fields are full
                        {
                            throw new BO.LineException("cannot update the line since not all fields were filled");
                        }

                    }
                    catch (BO.LineException ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.StationException ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }*/
        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void lastStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void firstStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void busNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
        private void UpdateLineButton_Click(object sender, RoutedEventArgs e)
        {
            BO.BusLine bs = new BO.BusLine();
            PO.BusLine pl = new PO.BusLine();
            if (busNumberTextBox.Text != "" && areaComboBox.SelectedItem != null)/*firstStationComboBox.Text != "" &&  && lastStationComboBox.SelectedItem != null*/
            {
                TempBusLine.Area = (BO.Area)areaComboBox.SelectedIndex;
                TempBusLine.BusNum = int.Parse(busNumberTextBox.Text);

               // TempBusLine.FirstStation = int.Parse(((BO.Station)firstStationComboBox.SelectedItem).CodeStation);
               // TempBusLine.LastStation = int.Parse(((BO.Station)lastStationComboBox.SelectedItem).CodeStation);

               /* if (TempBusLine.FirstStation == TempBusLine.LastStation)
                {
                    MessageBox.Show("ERROR", "Verification", MessageBoxButton.OK);
                }*/
                //bn.stationsList
            }
            else
            {

            }
            TempBusLine.DeepCopyTo(bs);
            MessageBoxResult res = MessageBox.Show("Update line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (res)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    bl.UpdateBusLinePersonalDetails(bs);
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
