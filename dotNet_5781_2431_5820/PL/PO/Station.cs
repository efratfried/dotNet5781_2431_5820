using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public abstract class Station : DependencyObject
    {
        static readonly DependencyProperty StationNameProperty = DependencyProperty.Register("StationName", typeof(string), typeof(Station));
        static readonly DependencyProperty AdressProperty = DependencyProperty.Register("Adress", typeof(BO.Adress), typeof(Station));
        static readonly DependencyProperty CodeStationProperty = DependencyProperty.Register("CodeStation", typeof(string), typeof(Station));
        static readonly DependencyProperty DisableAccessProperty = DependencyProperty.Register("DisableAccess", typeof(bool), typeof(Station));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(double), typeof(Station));
        static readonly DependencyProperty longitudeProperty = DependencyProperty.Register("longitude", typeof(double), typeof(Station));
        
        public string StationName { get => (string)GetValue(StationNameProperty); set => SetValue(StationNameProperty, value); }
        public BO.Adress Adress { get => (BO.Adress)GetValue(AdressProperty); set => SetValue(AdressProperty, value); }
        public string CodeStation { get => (string)GetValue(CodeStationProperty); set => SetValue(CodeStationProperty, value); }
        public bool DisableAccess { get => (bool)GetValue(DisableAccessProperty); set => SetValue(DisableAccessProperty, value); }
        public double Latitude { get => (double)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public double longitude { get => (double)GetValue(longitudeProperty); set => SetValue(longitudeProperty, value); }
    }
}


/*#region variables
 * 
 *      public string StationName { get; set; }
        //public Adress Adress { get; set; }
        public string CodeStation { get; set; }
        public bool DisableAccess { get; set; }
        public double Latitude { get; set; }
        public double longitude { get; set; }
#endregion*/
