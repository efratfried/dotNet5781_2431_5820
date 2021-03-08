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
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        public AddStationToLine()
        {
            InitializeComponent();
        }

        private void addstation_Click(object sender, RoutedEventArgs e)
        {
            if(addstation.DataContext!=null)
            {
                FollowingStationsDistace win = new FollowingStationsDistace();
                win.Show();
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Please choose on a station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
        }
    }
}
