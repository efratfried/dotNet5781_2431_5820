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

namespace PL.PO
{
    /// <summary>
    /// Interaction logic for busseswindow.xaml
    /// </summary>
    public partial class busseswindow : Window
    {
        public busseswindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataContext = Binding.IndexerName;
        }

        private void inner_info_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void start_driving_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_filling_foul_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_treatment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_bus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_bus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_bus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
