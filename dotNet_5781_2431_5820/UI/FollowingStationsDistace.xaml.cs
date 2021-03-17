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
    /// Interaction logic for FollowingStationsDistace.xaml
    /// </summary>
    public partial class FollowingStationsDistace : Window
    {
        IBL bl;
        public ObservableCollection<BO.FollowingStations> fs;
        PL.BusLineWindow bw;
        BO.FollowingStations station;
        public FollowingStationsDistace(BO.FollowingStations first,IBL _bl, PL.BusLineWindow bbw)
        {
            InitializeComponent();
            bl = _bl;
            bw = bbw;
            fs = new ObservableCollection<BO.FollowingStations>();
            dis1.Text = first.Distance.ToString();
            time.Text = first.AverageDrivingTime.ToString();
            first1.Text = first.FirstStationCode.ToString();
            second1.Text = first.SecondStationCode.ToString();
            station = first;
        }

        private void dis1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (dis1.IsFocused == false)
            //{
            //    double distance = bl.DistancefromPriviouStation(first1.Text, second1.Text);
            //    if(double.Parse(dis1.Text)<distance)
            //    {
            //        MessageBoxResult res = MessageBox.Show("The distance cant be under the air distance", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //    }
            //    else { }
            //}
        }
        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (dis1.Text != "") 
            {
                //double distance = bl.DistancefromPriviouStation(first1.Text, second1.Text);//te minimal distance- air distance
                //if (double.Parse(dis1.Text) < distance)
                //{
                //    throw new Exception("The distance can't be under the air distance ");
                //    MessageBoxResult res = MessageBox.Show("The distance can't be under the air distance", "ERROR", MessageBoxButton.YesNo, MessageBoxImage.Error);
                //}
                //else
                //{
                //sfs.Add(station);
                
                station.AverageDrivingTime = TimeSpan.Parse(time.Text.ToString());
                station.Distance = double.Parse(dis1.Text);

                bl.UpdateFollowingStationPersonalDetails(station);
                    int index = bw.bs.ToList().FindIndex(i => i.BusStationNum == station.FirstStationCode);
                BO.BusStationLine busStationLine = bw.bs[index];

                busStationLine.AverageDrivingTime =  station.AverageDrivingTime;
                busStationLine.Distance = station.Distance;
                bw.bs[index] = busStationLine;
                    this.Close();
               // }
            }
            else 
            {
                throw new Exception("Please fill all the fields");
            }
        }


    }
}
