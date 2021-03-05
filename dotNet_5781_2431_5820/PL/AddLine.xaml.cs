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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        IBL bl;
        public BO.BusLine addedLine;
        public List<BO.OutGoingLine> listTrips = new List<BO.OutGoingLine>();
        public bool AllFieldsWereFilled = false;
        public bool thereIsATrip = false;

        public AddLine(IBL _bl)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            bl = _bl;
            addedLine = new BO.BusLine();
            DataContext = addedLine;

            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            areaComboBox.SelectedIndex = 0; //index of the object to be selected

            firstStationComboBox.DisplayMemberPath = "Name";//show only specific Property of object
            firstStationComboBox.SelectedValuePath = "Code";//selection return only specific Property of object
            firstStationComboBox.SelectedIndex = 0; //index of the object to be selected
            lastStationComboBox.DisplayMemberPath = "Name";//show only specific Property of object
            lastStationComboBox.SelectedValuePath = "Code";//selection return only specific Property of object
            lastStationComboBox.SelectedIndex = 0; //index of the object to be selected

            firstStationComboBox.DataContext = bl.GetAllStations().ToList();//ObserListOfStations;
            lastStationComboBox.DataContext = bl.GetAllStations().ToList();//ObserListOfStations;
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

        private void firstStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addedLine.FirstStation = int.Parse((firstStationComboBox.SelectedItem as BO.Station).CodeStation);
        }

        private void lastStationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addedLine.LastStation = int.Parse((lastStationComboBox.SelectedItem as BO.Station).CodeStation);
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (busNumberTextBox.Text != "")
            {
                AllFieldsWereFilled = true;
                addedLine.BusNum = int.Parse(busNumberTextBox.Text);
                addedLine.Area = (BO.Area)areaComboBox.SelectedIndex;
            }

            MessageBoxResult res = MessageBox.Show("Add line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            this.Close();
        }

      /*  private bool CheckEqual()
        {
            if(lastStationComboBox.)
            {

            }
        }*/
    }
}
