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
        public ObservableCollection<BO.OutGoingLine> outgoingline;
        public BusLineSchedual(IBL _bl)
        {
            InitializeComponent();
            ts = new ObservableCollection<PO.BusLine>();
            bl = _bl;
            RefreshAllLinesComboBox();
            bs = new ObservableCollection<BO.BusStationLine>();
            


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
            BusLineComboBox.ItemsSource = ts;
            BusLineComboBox.DisplayMemberPath = "BusNum";
            BusLineComboBox.SelectedIndex = 0;
        }

        void RefreshFrequenciesGrid()//refresh the combobox each time the user changes the selection 
        {
           
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            outgoingline = new ObservableCollection<BO.OutGoingLine>();
            foreach (var item in bl.GetAllfrequencies(((PO.BusLine)BusLineComboBox.SelectedItem).ID))
            {
                outgoingline.Add(item);
            }
            Frequency.ItemsSource = outgoingline;
            Area.Text= ((PO.BusLine)BusLineComboBox.SelectedItem).Area.ToString();
            firstStationLabel.Text = ((PO.BusLine)BusLineComboBox.SelectedItem).FirstStation.ToString();
            lastStationLabel.Text = ((PO.BusLine)BusLineComboBox.SelectedItem).LastStation.ToString();
        }

        private void frequencyTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Frequency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                BO.OutGoingLine OutG = (BO.OutGoingLine)Frequency.SelectedItem;
            if (OutG != default)
            {
                TCS.ItemsSource = OutG.DepartureTimes;
                PCS.ItemsSource = OutG.TimeFinishTrval;
            }
          

        }

        private void BusLineSchedual1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BusLineSchedual2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TCS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PCS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}