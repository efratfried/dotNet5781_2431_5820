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
        IBL bl;
        PO.Station station;
        public ObservableCollection<string> Disable_Access;
        public UpdateStation(IBL _bl,PO.Station s)
        {
            InitializeComponent();
            Updatecode.Text = s.CodeStation;
            UpdateAdress.Text = s.Address;
            Updatename.Text = s.StationName;
            Updatelatitude.Text = s.Latitude.ToString();
            Updatelingtitude.Text = s.longitude.ToString();
            UpDisableAccess.Text = s.DisableAccess.ToString();
            bl = _bl;
            station = s;
        }
        void RefreshDisableAccessComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<string> DisableAccess = new List<string>() { "yes", "no" };
            //List<PO.Station> sta1 = new List<PO.Station>();
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

    }
}
