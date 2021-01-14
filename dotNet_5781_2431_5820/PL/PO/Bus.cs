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
    public abstract class Bus : DependencyObject
    {
        static readonly DependencyProperty LicenseNumProperty = DependencyProperty.Register("LicenseNum", typeof(int), typeof(Bus));
        static readonly DependencyProperty LicenseDateProperty = DependencyProperty.Register("LicenseDate", typeof(string), typeof(Bus));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("KM", typeof(BO.Status), typeof(Bus));
        static readonly DependencyProperty KMProperty = DependencyProperty.Register("foul", typeof(string), typeof(Bus));
        static readonly DependencyProperty foulProperty = DependencyProperty.Register("Status", typeof(int), typeof(Bus));
        static readonly DependencyProperty FirmProperty = DependencyProperty.Register("Firm", typeof(string), typeof(Bus));
        public int LicenseNum { get => (int)GetValue(LicenseNumProperty); set => SetValue(LicenseNumProperty, value); }
        public DateTime LicenseDate { get => (DateTime)GetValue(LicenseDateProperty); set => SetValue(LicenseDateProperty, value); }
        public BO.Status Status { get => (BO.Status)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        public double KM { get => (double)GetValue(KMProperty); set => SetValue(KMProperty, value); }
        public double foul { get => (int)GetValue(foulProperty); set => SetValue(foulProperty, value); }
        public BO.Firm firm { get => (BO.Firm)GetValue(FirmProperty); set => SetValue(FirmProperty, value); }
    }
}
