using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using BLAPI;

namespace ViewModel
{
    public class MainWindow : DependencyObject
    {
        IBL bl = BLFactory.GetBL("1");

        static readonly DependencyProperty BusLineProperty = DependencyProperty.Register("BusLine", typeof(PO.BusLine), typeof(MainWindow));
        public PO.BusLine BusLine { get => (PO.BusLine)GetValue(BusLineProperty); set => SetValue(BusLineProperty, value); }

        static readonly DependencyProperty BusLineIDsProperty = DependencyProperty.Register("BusLineIDs", typeof(ObservableCollection<PO.ListedBus>), typeof(MainWindow));
        public ObservableCollection<PO.ListedBus> BusLineIDs { get => (ObservableCollection<PO.ListedBus>)GetValue(BusLineIDsProperty); set => SetValue(BusLineIDsProperty, value); }

        public BO.BusLine BusLineBO
        {
            set
            {
                if (value == null)
                    BusLine = new PO.BusLine();
                else
                {
                    value.DeepCopyTo(BusLine);
                    //BusLine.ID = value.ID;
                    ////...
                    //BusLine.ListOfCourses.Clear();
                    //foreach (var fromCourse in value.ListOfCourses)
                    //{
                    //    PO.BusLineCourse toCourse = new PO.BusLineCourse();
                    //    toCourse.Grade = fromCourse.Grade;
                    //    toCourse. Number = fromCourse.Number;
                    //    // ...
                    //    BusLine.ListOfCourses.Add(toCourse);
                    //}
                }
                // update more properties in BusLine if needed... That is, properties that don't appear as is in BusLineBO...
            }
        }

        public MainWindow() => Reset();

        BackgroundWorker getBusLineWorker;
        internal void blGetBusLine(int id)
        {
            if (getBusLineWorker != null)
                getBusLineWorker.CancelAsync();
            getBusLineWorker = new BackgroundWorker();
            getBusLineWorker.WorkerSupportsCancellation = true;
            getBusLineWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    BusLineBO = (BO.BusLine)args.Result;
            };
            getBusLineWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                object BusLine = bl.GetBusLine((int)args.Argument);
                args.Result = worker.CancellationPending ? null : BusLine;
            };
            getBusLineWorker.RunWorkerAsync(id);
        }

        internal void Reset()
        {
            if (getBusLineWorker != null)
            {
                getBusLineWorker.CancelAsync();
                getBusLineWorker = null;
            }
            if (getBusLineIDsWorker != null)
            {
                getBusLineIDsWorker.CancelAsync();
                getBusLineIDsWorker = null;
            }
            BusLine = new PO.BusLine();
            blGetBusLineIDs();
        }

        BackgroundWorker getBusLineIDsWorker;
        public void blGetBusLineIDs()
        {
            getBusLineIDsWorker = new BackgroundWorker();
            getBusLineIDsWorker.WorkerSupportsCancellation = true;
            getBusLineIDsWorker.WorkerReportsProgress = true;
            getBusLineIDsWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => getBusLineIDsWorker = null;
            getBusLineIDsWorker.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    BusLineIDs.Add(new PO.ListedBus() { Bus = (BO.ListedBus)args.UserState });
            };
            getBusLineIDsWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                foreach (var item in bl.GetBusLineIDNameList())
                {
                    if (worker.CancellationPending) break;
                    worker.ReportProgress(0, item);
                }
            };
            BusLineIDs = new ObservableCollection<PO.ListedBus>();
            getBusLineIDsWorker.RunWorkerAsync();
        }
    }
}
