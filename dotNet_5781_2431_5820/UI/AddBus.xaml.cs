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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        IBL bL;
        public BO.Bus addbus = new BO.Bus();
        public bool AllFieldsWereFilled = false;
        public bool thereIsATrip = false;
        public PO.Bus Mybus=new PO.Bus();
        public AddBus(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            LicenseNum.Text = "";
            LicenseDate.Text = "";
            //firm.SelectedItem = (BO.Firm)addbus.Firm;
            busKM.Text = "";
            firm.ItemsSource = Enum.GetValues(typeof(BO.Firm));
            firm.SelectedIndex = 0; //index of the object to be selected
        }
        
        private void AddLineDetails_Click(object sender, RoutedEventArgs e)
        {
           // AddLine al = new AddLine(bL,);
           // al.ShowDialog();
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
                if (LicenseNum.Text != "" && LicenseDate.SelectedDate != null && busKM.Text != "" && busfoul.Text != "" && firm.SelectedItem != null)
                {
                    AllFieldsWereFilled = true;
                    Mybus.LicenseNum = LicenseNum.Text;
                    Mybus.LicenseDate = DateTime.Parse(LicenseDate.SelectedDate.ToString());
                    Mybus.KM = double.Parse(busKM.Text);
                    Mybus.foul = double.Parse(busfoul.Text);
                    Mybus.firm = (BO.Firm)(firm.SelectedIndex);
                    Mybus.DeepCopyTo(addbus);
                    bL.AddBus(addbus);
                    this.Close();
                }
                 else
                 {
                MessageBoxResult m = MessageBox.Show("You didnt fill all the details", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }

            MessageBoxResult res = MessageBox.Show("Add line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            this.Close();
        }

        private void firm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void LicenseNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void busfoul_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void busKM_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LicenseNumTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void foulTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void KMTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
    }
}
