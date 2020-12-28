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
        public AllLines busLineCollection;
        static Random m = new Random();
        public BusPresentation()//ctor
        {
            InitializeComponent();
            busLineCollection = new AllLines();     
            
            int StopsNum = m.Next(50,100);

            for (int i = 0; i < 5; i++)
            {//before adding a line we need to have a stations at least start & begin stations.
                busLineCollection.AddStopToList();//check this func
            }
            //till here everthing is all right!!!!!!!!!
            for (int j = 0; j < 10; j++)
            {//10 randoms lines.
                int NumofTheLine = m.Next(1,999);
                busLineCollection.AddLine(NumofTheLine);//check this func
            }

            cbBusLines.ItemsSource = busLineCollection.Lines;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
        }
        private BusLine currentDisplayBusLine;
       private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LineNum);
        }
        public BusLine this[int index]
        {
            get
            {
                foreach(var item in busLineCollection.Lines)
                {
                    if (item.LineNum == index)
                    {
                        return item;
                    }
                }
                return null;
            }

            private set
            {
                this[index] = value;
            }
        }
        private void ShowBusLine(int index)
        {          
            currentDisplayBusLine = busLineCollection.Lines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.LineStops;
        }

    }
}


