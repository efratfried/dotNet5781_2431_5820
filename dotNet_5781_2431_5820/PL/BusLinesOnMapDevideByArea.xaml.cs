using System;
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
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for BusLinesOnMapDevideByArea.xaml
    /// </summary>
    public partial class BusLinesOnMapDevideByArea : Window
    {
        IBL bl;
        public BusLinesOnMapDevideByArea(IBL _bl)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = _bl;

            cbNorth.DisplayMemberPath = "BusNumber";//show only specific Property of object
            cbNorth.SelectedValuePath = "LineId";//selection return only specific Property of object

            cbSouth.DisplayMemberPath = "BusNumber";//show only specific Property of object
            cbSouth.SelectedValuePath = "LineId";//selection return only specific Property of object

            cbJerusalem.DisplayMemberPath = "BusNumber";//show only specific Property of object
            cbJerusalem.SelectedValuePath = "LineId";//selection return only specific Property of object

            cbCenter.DisplayMemberPath = "BusNumber";//show only specific Property of object
            cbCenter.SelectedValuePath = "LineId";//selection return only specific Property of object

            cbGeneral.DisplayMemberPath = "BusNumber";//show only specific Property of object
            cbGeneral.SelectedValuePath = "LineId";//selection return only specific Property of object

            RefreshAllLinesComboBox();
        }

        void RefreshAllLinesComboBox()//refresh the combobox each time the user changes the selection 
        {
            cbNorth.DataContext = bl.GetAllLinesByArea(BO.Area.North);//ObserListOfLines;
            cbSouth.DataContext = bl.GetAllLinesByArea(BO.Area.South);//ObserListOfLines;
            cbJerusalem.DataContext = bl.GetAllLinesByArea(BO.Area.Jerusalem);//ObserListOfLines;
            cbCenter.DataContext = bl.GetAllLinesByArea(BO.Area.Center);//ObserListOfLines;
            cbGeneral.DataContext = bl.GetAllLinesByArea(BO.Area.General);//ObserListOfLines;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddLine win = new AddLine((sender as ComboBox).SelectedItem as BO.BusLine);
            win.Show();
        }
    }
}
