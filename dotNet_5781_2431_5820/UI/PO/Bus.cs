using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    #region Variables
    /*
     * 
      public int LicenseNum { get; set; }
        public DateTime LicenseDate { get; set; }
        public double KM { get; set; }
        public double foul { get; set; }
        public Status Status { get; set; }
        public Firm Firm { get; set; }
        public IEnumerable <DateTime> AccidentsDuco { get; set; }
        public IEnumerable<Treat> TreatsDuco { get; set; }
        public IEnumerable<DrivingBus> drivingBusesDuco { get; set; }*/
    #endregion
    public class Bus : DependencyObject
    {
        static readonly DependencyProperty LicenseNumProperty = DependencyProperty.Register("LicenseNum", typeof(string), typeof(Bus));
        static readonly DependencyProperty LicenseDateProperty = DependencyProperty.Register("LicenseDate", typeof(DateTime), typeof(Bus));
        static readonly DependencyProperty KMProperty = DependencyProperty.Register("KM", typeof(double), typeof(Bus));
        static readonly DependencyProperty foulProperty = DependencyProperty.Register("foul", typeof(double), typeof(Bus));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(BO.Status), typeof(Bus));
        static readonly DependencyProperty FirmProperty = DependencyProperty.Register("Firm", typeof(BO.Firm), typeof(Bus));
        static readonly DependencyProperty AccidentProperty = DependencyProperty.Register("Accident", typeof(BO.Accident), typeof(Bus));
        public string LicenseNum { get => (string)GetValue(LicenseNumProperty); set => SetValue(LicenseNumProperty, value); }
        public DateTime LicenseDate { get => (DateTime)GetValue(LicenseDateProperty); set => SetValue(LicenseDateProperty, value); }
        public BO.Status Status { get => (BO.Status)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        public double KM { get => (double)GetValue(KMProperty); set => SetValue(KMProperty, value); }
        public double foul { get => (double)GetValue(foulProperty); set => SetValue(foulProperty, value); }
        public BO.Firm Firm { get => (BO.Firm)GetValue(FirmProperty); set => SetValue(FirmProperty, value); }
        public BO.Accident Accident { get => (BO.Accident)GetValue(AccidentProperty); set => SetValue(AccidentProperty, value); }
    }
}
