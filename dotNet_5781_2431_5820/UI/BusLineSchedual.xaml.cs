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
using PL;
namespace UI
{
    /// <summary>
    /// Interaction logic for BusLineSchedual.xaml
    /// </summary>
    public partial class BusLineSchedual : Window
    {
        IBL bl;
        //PO.BusLine MyBusLine;
        //BO.BusStationLine MybusStation;
        public ObservableCollection<PO.BusLine> ts;
        public ObservableCollection<BO.BusStationLine> bs;
        public BusLineSchedual(IBL _bl)
        {
            InitializeComponent();
            ts = new ObservableCollection<PO.BusLine>();
            bl = _bl;
            RefreshAllLinesComboBox();
        }

        void RefreshAllLinesComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.BusLine> busLes = bl.GetBusLines().ToList();
            for (int i = 0; i < busLes.Count; i++)
            {
                PO.BusLine busLes2 = new PO.BusLine();
                busLes[i].DeepCopyTo(busLes2);

                ts.Add(busLes2);
            }
            BusLineList.ItemsSource = ts;
            BusLineList.DisplayMemberPath = "BusNum";
            BusLineList.SelectedIndex = 0;
        }

        void RefreshFrequenciesGrid()//refresh the combobox each time the user changes the selection 
        {
           /* List<BO.OutGoingLine> outG= bl.GetAllfrequencies(BusLineList.SelectedIndex).ToList();
            for (int i = 0; i < outG.Count; i++)
            {
                PO. busLes2 = new PO.BusLine();
                outG[i].DeepCopyTo(busLes2);

                ts.Add(busLes2);
            }
            BusLineList.ItemsSource = ts;
            BusLineList.DisplayMemberPath = "BusNum";
            BusLineList.SelectedIndex = 0;*/
        }
        private void BusLineSchedual1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BusLineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void frequencyTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
