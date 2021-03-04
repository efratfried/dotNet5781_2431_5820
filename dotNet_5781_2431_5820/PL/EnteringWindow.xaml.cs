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
    /// Interaction logic for EnteringWindow.xaml
    /// </summary>
    public partial class EnteringWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");//we create an "object" of IBL interface in order to use BL functions and classes

        public EnteringWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conecting win = new conecting();
            win.ShowDialog();
        }
        public void iocn()
        {//C:\Users\user1\source\repos\efratfried\dotNet5781_2431_5820\dotNet_5781_2431_5820\PL
            Uri iconUri = new Uri("C:/Users/user1/source/repos/efratfried/dotNet5781_2431_5820/dotNet_5781_2431_5820/PL/logo1.ico", UriKind.RelativeOrAbsolute);

            this.Icon = BitmapFrame.Create(iconUri);
        }

    }
}
