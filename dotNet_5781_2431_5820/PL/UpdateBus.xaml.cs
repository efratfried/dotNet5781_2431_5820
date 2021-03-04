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
    /// Interaction logic for UpdateBus.xaml
    /// </summary>
    public partial class UpdateBus : Window
    {       
        PO.Bus updatebus;
        IBL bL;
        public bool AllFieldsWereFilled = false;
        public UpdateBus(PO.Bus bus)
        {
            InitializeComponent();          
            updatebus = bus;
            aviability_status.Text = updatebus.Status.ToString();
            km_.Text = updatebus.KM.ToString();
            foul_status.Text = updatebus.foul.ToString();
            firm.SelectedItem = updatebus.firm;
            Licensenum.Text = bus.LicenseNum.ToString();

            Licensenum.IsEnabled = true;
            aviability_status.IsEnabled = true;
        }

        
        private void button_update_click(object sender, RoutedEventArgs e)
        {
            if (firm.SelectedItem!= null && km_.Text!="" && foul_status.Text!="")
            {
                AllFieldsWereFilled = true;
                updatebus.firm = (BO.Firm)firm.SelectedItem;
                updatebus.KM = double.Parse(km_.Text);
                updatebus.foul = double.Parse(foul_status.Text);
                BO.Bus b = updatebus as BO.Bus;

                bL.UpdateBusPersonalDetails(updatebus);
            }

            MessageBoxResult res = MessageBox.Show("update bus details?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            this.Close();
        }

        private void firm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatebus.firm = (BO.Firm)firm.SelectedItem;
        }
    }
}
