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
    /// Interaction logic for AddOutGoingLine.xaml
    /// </summary>
    public partial class AddOutGoingLine : Window
    {
        public BO.OutGoingLine trip;
        public bool isTimeLegal = false;
        public IBL bl;
        public PO.BusLine MybusLine;
        public AddOutGoingLine(IBL _bl,PO.BusLine busLine)
        {
            InitializeComponent();
            bl = _bl;
            MybusLine = busLine;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(hours.Text) > 23 || int.Parse(minutes.Text) > 59 || int.Parse(seconds.Text) > 59)
                    throw new BO.BadBusLineIdException("cannot add the trip, illegal time format");

                TimeSpan ts = new TimeSpan(int.Parse(hours.Text), int.Parse(minutes.Text), int.Parse(seconds.Text));
                TimeSpan ts1 = new TimeSpan(int.Parse(hoursEnd.Text), int.Parse(minutesEnd.Text), int.Parse(secondsEnd.Text));
                TimeSpan ts2 = new TimeSpan(int.Parse(hoursF.Text), int.Parse(minutesF.Text), int.Parse(secondsF.Text));

                trip = new BO.OutGoingLine();
                trip.LineStartTime = ts;
                trip.LineFinishTime = ts1;
                trip.LineFrequencyTime = ts2;
                trip.Id = MybusLine.ID;
                bl.AddLineExit(trip);
                this.Close();
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void hours_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            hours.MaxLength = 2;
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

        private void minutes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            minutes.MaxLength = 2;
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

        private void seconds_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            seconds.MaxLength = 2;
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
    }
}
