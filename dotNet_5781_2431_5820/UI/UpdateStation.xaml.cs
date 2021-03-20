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
    /// Interaction logic for UpdateStation.xaml
    /// </summary>
    public partial class UpdateStation : Window
    {
        List<string> DisableAccess = new List<string>() { "yes", "no" };
        IBL bl;
        PO.Station station=new PO.Station();
        public ObservableCollection<string> Disable_Access;
        public UpdateStation(IBL _bl,PO.Station s)
        {
            InitializeComponent();
            bl = _bl;
            station = s;
            Updatecode.Text =station.CodeStation;
            UpdateAdress.Text = station.Address;
            Updatename.Text = station.StationName;
            Updatelatitude.Text = station.Latitude.ToString();
            Updatelingtitude.Text = station.longitude.ToString();
            UpDisableAccess.ItemsSource = DisableAccess;
            if(station.DisableAccess)
            {
                UpDisableAccess.SelectedItem = DisableAccess[0];
            }
            else
            {
                UpDisableAccess.SelectedItem = DisableAccess[1];
            }
        }
        void RefreshDisableAccessComboBox()//refresh the combobox each time the user changes the selection 
        {
            for (int i = 0; i < DisableAccess.Count; i++)
            {
                string DisableAccess1 = "";
                DisableAccess[i].DeepCopyTo(DisableAccess1);

                Disable_Access.Add(DisableAccess1);
            }
            UpDisableAccess.DataContext = Disable_Access;
            //StationComboBox.DisplayMemberPath = "CodeStation";
            UpDisableAccess.DisplayMemberPath = "DisableAccess";
            UpDisableAccess.SelectedIndex = 0;
        }

        private void Updatedetails_Click(object sender, RoutedEventArgs e)
        {
            PO.Station sta = new PO.Station();
            if (UpdateAdress.Text != "" && Updatename.Text != "" && Updatelatitude.Text != "" && Updatelingtitude.Text != "")
            {
                //BO.Station newStat = new BO.Station();//a local station, to save the changes that the user made in station's fields.
                sta.CodeStation = station.CodeStation;
                sta.Address = UpdateAdress.Text;
                sta.StationName = Updatename.Text;
                sta.longitude = double.Parse(Updatelatitude.Text);
                sta.Latitude = double.Parse(Updatelingtitude.Text);
                sta.DisableAccess = UpDisableAccess.ItemsSource.ToString() == "yes";

                    BO.Station temp = new BO.Station();
                    sta.DeepCopyTo(temp);
                    bl.UpdateStationPersonalDetails(temp);
                this.Close();
            }
            else
            {
                throw new BO.BadStationException("cannot update the station since not all fields were filled");
            }
        }

        private void Updatecode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UpDisableAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Updatename_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
