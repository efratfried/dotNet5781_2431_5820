using dotNet_02_5781_2431_5820;
using dotNet_02_5781_2431_5820.git;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace dotNet_5781_3a_2431_5820
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BusPresentation : Window
    {
        private AllLines busLineCollection;
        public BusPresentation()//ctor
        {
            InitializeComponent();
            AddRandomlyBus();//func initialization that randomally adds 10 buses

        }
        private static Random r = new Random(DateTime.Now.Millisecond);
        private void AddRandomlyBus()
        {
            busLineCollection = new AllLines();

            for (int i = 0; i < 10; i++)//add 10 buses
            {
                int busLineNum = r.Next(1, 999);//randomal bus line number

                List<BusStop> stat = new List<BusStop>();
                int numOfStations = r.Next(2, 30);

                int firstStationNum = r.Next(5000, 999999);//init 1st station
                BusStopLine first = new BusStopLine(firstStationNum, true);
                stat.Add(first);

                for (int j = 0; j < numOfStations - 1; j++)//the other stations
                {
                    int stationNum = r.Next(5000, 999999);
                    BusStopLine st = new BusStopLine(stationNum, false);
                    stat.Add(st);
                }

                Location ar = (Location)r.Next(0, 5);//enum of areas

                BusLine bus = new BusLine() { Stations = stat, busLine = busLineNum, FirstStation = first, LastStation = stat[stat.Count - 1], Area = ar };

                busLineCollection.buses.Add(bus);//add the bus to collection
            }

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


