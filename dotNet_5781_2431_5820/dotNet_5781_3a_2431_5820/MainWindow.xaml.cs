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
        ComboBox cbBusLines = new ComboBox();
        public AllLines busLineCollection;
        public BusPresentation()//ctor
        {
            busLineCollection = new AllLines();
            //InitializeComponent();
            Random m = new Random();
            int rand = m.Next(50,100);

            for (int i = 0; i < rand; i++)
            {//before adding a line we need to have a stations at least start & begin stations.
                busLineCollection.AddStopToList();
            }

            for (int j = 0; j < 10; j++)
            {//10 randoms lines.
                Random i = new Random();
                int rand1 = i.Next();
                busLineCollection.AddLine(rand1);
            }
            cbBusLines.ItemsSource = busLineCollection.Lines;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
            ComboBox = busLineCollection.Lines;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}


