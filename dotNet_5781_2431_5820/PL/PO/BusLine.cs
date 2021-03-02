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
        static readonly DependencyProperty AreaProperty = DependencyProperty.Register("Area", typeof(BO.Area), typeof(BusLine));

        public int BusNum { get => (int)GetValue(BusNumProperty); set => SetValue(BusNumProperty, value); }
        public string FirstStation { get => (string)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }
        public string LastStation { get => (string)GetValue(LastStationProperty); set => SetValue(LastStationProperty, value); }
        public bool IsDeleted { get => (bool)GetValue(IsDeletedProperty); set => SetValue(IsDeletedProperty, value); }
        public BO.BusLine MyBusLine { get => (BO.BusLine)GetValue(MyBusLineProperty); set => SetValue(MyBusLineProperty, value); }
        public BO.Area Area { get => (BO.Area)GetValue(AreaProperty); set => SetValue(AreaProperty, value); }
    }
}

