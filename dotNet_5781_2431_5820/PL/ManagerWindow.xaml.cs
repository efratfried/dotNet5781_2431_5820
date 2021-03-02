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
    public partial class Window1 : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public Window1()
        {
            InitializeComponent();
        }

        private void busses_Click(object sender, RoutedEventArgs e)
        {
            PL.busseswindow busseswindow= new PL.busseswindow(bl);
            busseswindow.Show();//CANT OPEN OTHER WHEN FIRST NOT CLOSE
        }

        private void buslines_Click(object sender, RoutedEventArgs e)
        {
            PL.BusLineWindow buslineswindow = new PL.BusLineWindow(bl);
            buslineswindow.Show();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            PL.BusStation stationwindow = new PL.BusStation(bl);
            stationwindow.Show();
        }

    }
}
