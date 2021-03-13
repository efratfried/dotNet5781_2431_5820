using BLAPI;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PL;
using System.Windows.Threading;
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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (mePlayer.Source != null)
            {
                if (mePlayer.NaturalDuration.HasTimeSpan)
                    lblStatus.Content = String.Format("{0} / {1}", mePlayer.Position.ToString(@"mm\:ss"), mePlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lblStatus.Content = "No file selected...";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conecting win = new conecting(bl);
            win.ShowDialog();
        }
        public void iocn()
        {//C:\Users\user1\source\repos\efratfried\dotNet5781_2431_5820\dotNet_5781_2431_5820\PL
            Uri iconUri = new Uri("logo1.ico", UriKind.RelativeOrAbsolute);

            this.Icon = BitmapFrame.Create(iconUri);
        }

    }
}
