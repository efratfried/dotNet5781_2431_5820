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
using PL;
namespace UI
{
    /// <summary>
    /// Interaction logic for AddAccident.xaml
    /// </summary>
    public partial class AddAccident : Window
    {
        IBL bl;
        public PO.Bus B;

        public IEnumerable<BO.Accident> accidents;
        public AddAccident(PO.Bus bus,IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            licensenum.Text = bus.LicenseNum;
            accidentDate.Text = "";

            B = bus;
        }

        private void Addaccident_Click(object sender, RoutedEventArgs e)
        {
            if(accidentDate.SelectedDate!=null)
            {
               BO.Accident a = bl.GetAllAccident().Where(i => i.AccidentDate == accidentDate.SelectedDate.Value).First();

                if (a!=null)
                {                   
                    MessageBoxResult m = MessageBox.Show("Couldnt add the accident because there was already an accident of this vehicle in the same date" , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BO.Accident ac = new BO.Accident()
                    {
                        AccidentDate =  DateTime.Parse(accidentDate.SelectedDate.ToString()),
                        LicenseNum = B.LicenseNum,
                        AccidentNum = a.AccidentNum + 1

                    };

                    bl.AddAccident(ac);
                    MessageBox.Show("Succeed", "Verification", MessageBoxButton.OK);
                    this.Close();
                }
            }
           
        }
    }
}
