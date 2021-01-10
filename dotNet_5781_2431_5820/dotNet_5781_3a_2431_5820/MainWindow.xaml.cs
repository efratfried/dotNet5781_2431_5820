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
            int StopsNum = m.Next(50, 100);
            for (int i = 0; i < StopsNum; i++)
            {//before adding a line we need to have a stations at least start & begin stations.
                busLineCollection.AddStopToList();//check this func
            }
            for (int j = 0; j < 10; j++)
            {//10 randoms lines.
                int NumofTheLine = m.Next(1, 999);
                busLineCollection.AddLine(NumofTheLine);//check this func
            }
            cbBusLines.ItemsSource = busLineCollection.Lines;
            cbBusLines.DisplayMemberPath =  "LineNum";
            cbBusLines.SelectedIndex = 0;
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLineCollection[index];
            UpGrid.DataContext = currentDisplayBusLine.LineNum;
            lbBusLineStations.DataContext = currentDisplayBusLine.LineStops;
        }
        private BusLine currentDisplayBusLine;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ShowBusLine(((int)(cbBusLines.SelectedItem as BusLine).MyArea));
        }
        public BusLine this[int busLineNum]//indexer
        {
            get
            {
                return busLineCollection.Lines.Find(item => item.LineNum == busLineNum);
            }
            set
            { }
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LineNum);
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LineNum);
        }
    }
}


