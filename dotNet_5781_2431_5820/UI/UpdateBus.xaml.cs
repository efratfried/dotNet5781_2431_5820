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
        PO.Bus updatebus=new PO.Bus();
        IBL bL;
        public bool AllFieldsWereFilled = false;
        public UpdateBus(PO.Bus bus,IBL _bl)
        {
            InitializeComponent();
            bL = _bl;
            updatebus = bus;
           
            km_.Text = updatebus.KM.ToString();
            foul_status.Text = updatebus.foul.ToString();
            firm.SelectedItem = updatebus.Firm;
            Licensenum.Text = bus.LicenseNum.ToString();
            firm.ItemsSource = Enum.GetValues(typeof(BO.Firm));
            firm.SelectedIndex = 0;
            Licensenum.IsReadOnly = true;
            aviability_status.IsReadOnly = true;
        }
       
        private void button_update_click(object sender, RoutedEventArgs e)
        {
            if (firm.SelectedItem!= null && km_.Text!="" && foul_status.Text!="")
            {
                AllFieldsWereFilled = true;
                updatebus.Firm = (BO.Firm)firm.SelectedItem;
                updatebus.KM = double.Parse(km_.Text);
                updatebus.foul = double.Parse(foul_status.Text);
                BO.Bus b=new BO.Bus();
                updatebus.DeepCopyTo(b);

                switch (updatebus.foul )
                {
                    case 0:

                    default:
                        break;
                }
                bL.UpdateBusPersonalDetails(b);
            }

            MessageBoxResult res = MessageBox.Show("update bus details?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            this.Close();
        }

        private void firm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Licensenum_TextChanged(object sender, TextChangedEventArgs e)
        {
            Licensenum.Text = updatebus.LicenseNum;
        }
    }
}
