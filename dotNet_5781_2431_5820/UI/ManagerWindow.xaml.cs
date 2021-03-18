using BLAPI;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public ManagerWindow(PO.User user)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            username.Text = user.UserName;
            slidepanel.Opacity = 0.0;
        }

        private void busses_Click(object sender, RoutedEventArgs e)
        {
            slidepanel.Opacity = 1.0;
            slidepanel.Height = busses.Height;
            PL.busseswindow busseswindow= new PL.busseswindow(bl);
            busseswindow.ShowDialog();//CANT OPEN OTHER WHEN FIRST NOT CLOSE
        }

        private void buslines_Click(object sender, RoutedEventArgs e)
        {
            slidepanel.Height = buslines.Height;
            slidepanel.Opacity =1.0; 
            PL.BusLineWindow buslineswindow = new PL.BusLineWindow(bl);
            buslineswindow.ShowDialog();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            slidepanel.Opacity = 1.0;
            slidepanel.Height = Stations.Height;
            PL.StationsWindow1 stationwindow = new StationsWindow1(bl);
            stationwindow.ShowDialog();
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            conecting win1 = new conecting(bl);
            win1.ShowDialog();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    
    }
}
