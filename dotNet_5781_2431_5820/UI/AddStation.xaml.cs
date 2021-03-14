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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL bl;
        public BO.Station addedStat=new BO.Station();
        public PO.Station adds=new PO.Station();
        public BO.BusStationLine bs=new BO.BusStationLine();
        public bool AllFieldsWereFilled = false;
        public ObservableCollection<bool> disable;

        public AddStation(IBL _bl)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            bl = _bl;
              //adds = Stat;            
        }
        
        void RefreshDisable_Access_ComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<string> disac = new List<string> { "yes", "no" };

            Disable_Access.Items.Add(disac[0]);
            Disable_Access.Items.Add(disac[1]);
            //bl.GetStationLicenseNumList();
            // Disable_Access.DisplayMemberPath = "DisableAccess";
            //Disable_Access.DataContext = disable;
            //Disable_Access.DisplayMemberPath = "DisableAccess";
            Disable_Access.SelectedIndex = 0;
        }
        private void Disable_Access_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addedStat.DisableAccess = (bool)(Disable_Access.SelectedItem);
        }

        private void addressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            if (addressTextBox.Text != "" && codeTextBox.Text != "0" && lattitudeTextBox.Text != "0" && longitudeTextBox.Text != "0" && nameTextBox.Text != "")
            {
                AllFieldsWereFilled = true;

                adds.StationName = nameTextBox.Text;
                adds.CodeStation = codeTextBox.Text;
                adds.Address = addressTextBox.Text;
                adds.longitude = double.Parse(longitudeTextBox.Text);
                adds.Latitude = double.Parse(lattitudeTextBox.Text);
                adds.DisableAccess = (string)Disable_Access.SelectedItem=="yes";
                adds.DeepCopyTo(addedStat);
                bl.AddStation(addedStat);
                adds.DeepCopyTo(bs);
                bl.AddBusStationLine(bs);
                this.Close();
            }
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void longitudeTextBox_TextChanged(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
            {
                return;
            }
            //allow entering one "." only. (since its double):
            if (e.Key == Key.OemQuestion && !Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift) && !longitudeTextBox.Text.Contains("."))
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

        private void lattitudeTextBox_TextChanged(object sender, KeyEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)//allow delete keys
            {
                return;
            }
            //allow entering one "." only. (since its double):
            if (e.Key == Key.OemQuestion && !Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift) && !lattitudeTextBox.Text.Contains("."))
                return;

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

        private void codeTextBox_TextChanged(object sender, KeyEventArgs e)
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

        private void codeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
        }

        /* List<BO.BusLine> busLes = bl.GetBusLines().ToList();
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
// BusLineComboBox.ItemsSource = buslines;*/
    
    
    }
}
