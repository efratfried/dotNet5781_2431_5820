using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulateOneStationWindow.xaml
    /// </summary>
    public partial class SimulateOneStationWindow : Window
    {
        IBL bl;
        PO.Station station;
        public ObservableCollection<PO.Station> stationlist;
        private ObservableCollection<BO.DigitalPanel> outGoingLineList;
        Stopwatch stopwatch;
        BackgroundWorker workerPanl;
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;
        PO.Station sta2;
        bool isTimerRun;
        string timmerText;
        public SimulateOneStationWindow(IBL _bl, PO.Station _stat)
        {
            InitializeComponent();
            Closing += Window_Closing;
            bl = _bl;
            station = _stat;
            // gridOneStation.DataContext =station;
            stopwatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            tsStartTime = DateTime.Now.TimeOfDay;
            stopwatch.Restart();
            isTimerRun = true;
            stationlist = new ObservableCollection<PO.Station>();
            RefreshAllStationsComboBox();
            timerworker.RunWorkerAsync();          
        }

        void RefreshAllStationsComboBox()//refresh the combobox each time the user changes the selection 
        {
            List<BO.Station> sta = bl.GetAllStations().ToList();

            for (int i = 0; i < sta.Count; i++)
            {
                PO.Station sta2 = new PO.Station();
                sta[i].DeepCopyTo(sta2);

                stationlist.Add(sta2);
            }
            PO.Station st= stationlist.ToList().Find(i => i.CodeStation == "10847");
            stationlist[stationlist.ToList().FindIndex(i => i.CodeStation == "10847")] = stationlist[0];
            stationlist[0] = st;
            StationComboBox.ItemsSource = stationlist;

            //StationComboBox.DisplayMemberPath = "CodeStation";
            StationComboBox.DisplayMemberPath = "StationName";
            //StationComboBox.SelectedIndex = -1;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            stopwatch.Stop();
            isTimerRun = false;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan CurrentTime = tsStartTime + stopwatch.Elapsed;
            timmerText = CurrentTime.ToString().Substring(0, 8);
            this.timerTextBlock.Text = timmerText;         
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                 timerworker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                workerPanl.ReportProgress(1);
                Thread.Sleep(20000);
                //outGoingLineList.Clear();
            }
        }
        private void Worker_ProgressChanged1(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                outGoingLineList = new ObservableCollection<BO.DigitalPanel>();
                foreach (BO.DigitalPanel item in bl.DigitalPaneles(int.Parse(sta2.CodeStation), TimeSpan.Parse(timmerText)))
                {
                    outGoingLineList.Add(item);
                }
                nisayon.ItemsSource = outGoingLineList;
              
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void statName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nisayon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {       
            sta2 = (PO.Station)StationComboBox.SelectedItem;
            
            workerPanl = new BackgroundWorker();
            workerPanl.DoWork += Worker_DoWork1;
            workerPanl.ProgressChanged += Worker_ProgressChanged1;
            workerPanl.WorkerReportsProgress = true;
            workerPanl.RunWorkerAsync();
        }
    }
}
