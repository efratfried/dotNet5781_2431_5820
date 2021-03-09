using BLAPI;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PL;
namespace UI
{
    /// <summary>
    /// Interaction logic for EnteringWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");//we create an "object" of IBL interface in order to use BL functions and classes

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conecting win = new conecting(bl);
            win.ShowDialog();
        }
        public void iocn()
        {//C:\Users\user1\source\repos\efratfried\dotNet5781_2431_5820\dotNet_5781_2431_5820\PL
            Uri iconUri = new Uri("C:/Users/user1/source/repos/efratfried/dotNet5781_2431_5820/dotNet_5781_2431_5820/PL/logo1.ico", UriKind.RelativeOrAbsolute);

            this.Icon = BitmapFrame.Create(iconUri);
        }

    }
}
