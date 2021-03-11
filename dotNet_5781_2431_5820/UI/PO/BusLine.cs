using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace PO
{
    public class BusLine : DependencyObject
    {
        static readonly DependencyProperty BusNumProperty = DependencyProperty.Register("BusNum", typeof(int), typeof(BusLine));
        static readonly DependencyProperty FirstStationProperty = DependencyProperty.Register("FirstStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty LastStationProperty = DependencyProperty.Register("LastStation", typeof(int), typeof(BusLine));
        static readonly DependencyProperty IsDeletedProperty = DependencyProperty.Register("IsDeleted", typeof(bool), typeof(BusLine));
        static readonly DependencyProperty MyBusLineProperty = DependencyProperty.Register("MyBusLine", typeof(BusLine), typeof(BusLine));
        static readonly DependencyProperty AreaProperty = DependencyProperty.Register("Area", typeof(BO.Area), typeof(BusLine));
        static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(BusLine));
        //static readonly ObservableCollection(PO.BusLine) StationList=ObservableCollection(PO.BusLine)

        public int BusNum { get => (int)GetValue(BusNumProperty); set => SetValue(BusNumProperty, value); }
        public int FirstStation { get => (int)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }
        public int LastStation { get => (int)GetValue(LastStationProperty); set => SetValue(LastStationProperty, value); }
        public bool IsDeleted { get => (bool)GetValue(IsDeletedProperty); set => SetValue(IsDeletedProperty, value); }
        public BO.BusLine MyBusLine { get => (BO.BusLine)GetValue(MyBusLineProperty); set => SetValue(MyBusLineProperty, value); }
        public BO.Area Area { get => (BO.Area)GetValue(AreaProperty); set => SetValue(AreaProperty, value); }
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }
    }
}

