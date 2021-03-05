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
        PO.BusLine TempBusLine;
        public UpdateBusLineWindow(PO.BusLine MybusLine)
        {
            InitializeComponent();
            TempBusLine = MybusLine;
            areaComboBox.DataContext = TempBusLine.Area;
            busNumberTextBox.Text = TempBusLine.BusNum.ToString();
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
            //need to check what to do.
            areaComboBox.SelectedItem = areaComboBox;
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
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
    }
}
