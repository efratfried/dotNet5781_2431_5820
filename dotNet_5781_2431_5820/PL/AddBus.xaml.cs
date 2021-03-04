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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        IBL bL;
        public BO.Bus addbus;
        public bool AllFieldsWereFilled = false;
        public bool thereIsATrip = false;
        public AddBus()
        {
            InitializeComponent();
        }

        private void AddLineDetails_Click(object sender, RoutedEventArgs e)
        {
            AddLine al = new AddLine(bL);
            al.ShowDialog();
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LicenseNum.Text != "" && LicenseDate.SelectedDate != null && busKM.Text != "" && busfoul.Text != "" && firm.SelectedItem != null)
                {
                    AllFieldsWereFilled = true;
                    addbus.LicenseNum = LicenseNum.Text;
                    addbus.LicenseDate = DateTime.Parse(LicenseDate.SelectedDate.ToString());
                    addbus.KM = double.Parse(busKM.Text);
                    addbus.foul = double.Parse(busfoul.Text);
                    addbus.Firm = (BO.Firm)(firm.SelectedIndex);
                }
            }
            catch(BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBoxResult res = MessageBox.Show("Add line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            this.Close();
        }

        private void firm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addbus.Firm = (BO.Firm)(firm.SelectedIndex);
        }
    }
}
