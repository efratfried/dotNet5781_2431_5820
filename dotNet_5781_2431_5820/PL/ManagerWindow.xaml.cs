﻿using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void busses_Click(object sender, RoutedEventArgs e)
        {
            PL.PO.busseswindow busseswindow= new PO.busseswindow();
            busseswindow.Show();
        }

        private void buslines_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buslinesStations_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
