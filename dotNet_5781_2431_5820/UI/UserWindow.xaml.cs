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
using PL;
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public UserWindow(PO.User user)
        {
            InitializeComponent();
            username.DataContext = user.UserName;
            SlidePanel.Opacity = 0.0;
        }

        private void buslines_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Opacity = 1;
            SlidePanel.Height = buslines.Height;
            PL.BusLineWindow buslineswindow = new PL.BusLineWindow(bl);
            buslineswindow.ShowDialog();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Opacity = 1;
            SlidePanel.Height = Stations.Height;
            PL.StationsWindow1 stationwindow = new StationsWindow1(bl);
            stationwindow.ShowDialog();
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            conecting win1 = new conecting(bl);
            win1.ShowDialog();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void simulation_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Opacity = 1;
            SlidePanel.Height = Stations.Height;
            //PL.SimulateOneStationWindow simulation = new PL.SimulateOneStationWindow(bl);
            //simulation.ShowDialog();
        }
    }
}
