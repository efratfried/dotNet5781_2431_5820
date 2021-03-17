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
        public BusLineWindow BS;

        public AddLine(IBL _bl, BusLineWindow _BS)
        {
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            BS = _BS;
            bl = _bl;
            addedLine = new BO.BusLine();
            DataContext = addedLine;

            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));

            firstStationComboBox.ItemsSource = bl.GetAllStations();//ObserListOfStations;
            lastStationComboBox.ItemsSource = bl.GetAllStations();//ObserListOfStations;
            areaComboBox.SelectedIndex = 0; //index of the object to be selected
            firstStationComboBox.DisplayMemberPath = "StationName";//show only specific Property of object
            firstStationComboBox.SelectedValuePath = "Code";//selection return only specific Property of object
            firstStationComboBox.SelectedIndex = 0; //index of the object to be selected
            lastStationComboBox.DisplayMemberPath = "StationName";//show only specific Property of object
            lastStationComboBox.SelectedValuePath = "Code";//selection return only specific Property of object
            lastStationComboBox.SelectedIndex = 0; //index of the object to be selected
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

            BO.BusLine bn=new BO.BusLine();
            PO.BusLine pl=new PO.BusLine();
            if (busNumberTextBox.Text != ""&& IDtextbox.Text!=""&& areaComboBox.SelectedItem!=null&& firstStationComboBox.SelectedItem!=null&& lastStationComboBox.SelectedItem!=null)
            {
               // bn = bl.GetBusLine(int.Parse(busNumberTextBox.Text));
                bn.Area= (BO.Area)areaComboBox.SelectedIndex;
                bn.BusNum= int.Parse(busNumberTextBox.Text);
                //bn.ID=int.Parse(IDtextbox.Text);
                bn.FirstStation = int.Parse(((BO.Station)firstStationComboBox.SelectedItem).CodeStation);
                bn.LastStation=int.Parse(((BO.Station)lastStationComboBox.SelectedItem).CodeStation);

               if(bn.FirstStation== bn.LastStation)
                {
                    MessageBox.Show("ERROR", "Verification", MessageBoxButton.OK);
                }
                //bn.stationsList
            }
            else
            {

            }
            
            MessageBoxResult res = MessageBox.Show("Add line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                    bl.AddBusLine(bn);
                    bl.GetBusLine(bn.ID).DeepCopyTo(pl);
                    BS.ts.Add(pl);
                    bl.AddFollowingStation(addedLine.FirstStation.ToString(), addedLine.LastStation.ToString());
                  /* BO.FollowingStations chosen = bl.GetFollowingStation(addedLine.FirstStation.ToString(), addedLine.LastStation.ToString());
                    if(chosen.Distance==0)
                    {
                        FollowingStationsDistace f = new FollowingStationsDistace(chosen, bl);
                        f.Show();
                    }
                    else { }*/
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }           
        }

        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void busNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*  private bool CheckEqual()
          {
              if(lastStationComboBox.)
              {

              }
          }*/
    }
}
