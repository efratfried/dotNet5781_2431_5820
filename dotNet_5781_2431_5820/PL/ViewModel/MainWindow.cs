/*using System;
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

        static readonly DependencyProperty BusProperty = DependencyProperty.Register("Bus", typeof(PO.Bus), typeof(MainWindow));
        public PO.Bus bus { get => (PO.Bus)GetValue(BusProperty); set => SetValue(BusProperty, value); }

        static readonly DependencyProperty BusLicenseNumsProperty = DependencyProperty.Register("BusLicenseNums", typeof(ObservableCollection<PO.Bus>), typeof(MainWindow));
        public ObservableCollection<PO.Bus> BusLicenseNums { get => (ObservableCollection<PO.Bus>)GetValue(BusLicenseNumsProperty); set => SetValue(BusLicenseNumsProperty, value); }

        public BO.Bus BusBO
        {
            set
            {
                if (value == null)
                    bus = new PO.Bus();
                else
                {
                    value.DeepCopyTo(bus);
                    //Student.ID = value.ID;
                    ////...
                    //Student.ListOfCourses.Clear();
                    //foreach (var fromCourse in value.ListOfCourses)
                    //{
                    //    PO.StudentCourse toCourse = new PO.StudentCourse();
                    //    toCourse.Grade = fromCourse.Grade;
                    //    toCourse. Number = fromCourse.Number;
                    //    // ...
                    //    Student.ListOfCourses.Add(toCourse);
                    //}
                }
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }

        public MainWindow() => Reset();

        BackgroundWorker getStudentWorker;
        internal void blGetStudent(int id)
        {
            if (getStudentWorker != null)
                getStudentWorker.CancelAsync();
            getStudentWorker = new BackgroundWorker();
            getStudentWorker.WorkerSupportsCancellation = true;
            getStudentWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    BusBO = (BO.Bus)args.Result;
            };
            getStudentWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                object student = bl.GetBus((int)args.Argument);
                args.Result = worker.CancellationPending ? null : student;
            };
            getStudentWorker.RunWorkerAsync(id);
        }

        internal void Reset()
        {
            if (getStudentWorker != null)
            {
                getStudentWorker.CancelAsync();
                getStudentWorker = null;
            }
            if (getStudentIDsWorker != null)
            {
                getStudentIDsWorker.CancelAsync();
                getStudentIDsWorker = null;
            }
            bus = new PO.Bus();
            blGetStudentIDs();
        }

        BackgroundWorker getStudentIDsWorker;
        public void blGetStudentIDs()
        {
            getStudentIDsWorker = new BackgroundWorker();
            getStudentIDsWorker.WorkerSupportsCancellation = true;
            getStudentIDsWorker.WorkerReportsProgress = true;
            getStudentIDsWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => getStudentIDsWorker = null;
            getStudentIDsWorker.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    StudentIDs.Add(new PO.Bus() { Person = (BO.Bus)args.UserState });
            };
            getStudentIDsWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                foreach (var item in bl.GetStudentIDNameList())
                {
                    if (worker.CancellationPending) break;
                    worker.ReportProgress(0, item);
                }
            };
            StudentIDs = new ObservableCollection<PO.Bus>();
            getStudentIDsWorker.RunWorkerAsync();
        }
    }
}*/