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
using PL;
using BO;
namespace UI
{
    /// <summary>
    /// Interaction logic for BusLineSchedual.xaml
    /// </summary>
    public partial class BusLineSchedual : Window
    {
        public BusLineSchedual()
        {
            InitializeComponent();
        }

        private void cbLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }
    }
}
