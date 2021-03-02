using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public abstract class BusLine : DependencyObject
    {
        static readonly DependencyProperty BusNumProperty = DependencyProperty.Register("BusNum", typeof(int), typeof(BusLine));
        static readonly DependencyProperty FirstStationProperty = DependencyProperty.Register("FirstStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty LastStationProperty = DependencyProperty.Register("LastStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty IsDeletedProperty = DependencyProperty.Register("IsDeleted", typeof(bool), typeof(BusLine));
        static readonly DependencyProperty MyBusLineProperty = DependencyProperty.Register("MyBusLine", typeof(BusLine), typeof(BusLine));

        public int LicenseNum { get => (int)GetValue(BusNumProperty); set => SetValue(BusNumProperty, value); }
        public DateTime LicenseDate { get => (DateTime)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }
        public BO.Status Status { get => (BO.Status)GetValue(LastStationProperty); set => SetValue(LastStationProperty, value); }
        public bool IsDeleted { get => (bool)GetValue(IsDeletedProperty); set => SetValue(IsDeletedProperty, value); }
        public BO.BusLine MyBusLine { get => (BO.BusLine)GetValue(MyBusLineProperty); set => SetValue(MyBusLineProperty, value); }
    }
}

