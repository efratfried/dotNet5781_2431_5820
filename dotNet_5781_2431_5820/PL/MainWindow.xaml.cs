using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel.MainWindow viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel.MainWindow();
            //viewModel.Reset();
            DataContext = viewModel;
        }

        private void cbBusLineID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id;

            if (cbBusLineID.SelectedIndex < 0)
                return;

            try
            {
                id = (int)cbBusLineID.SelectedValue;
                viewModel.blGetBusLine(id);
                //gridBusLine.DataContext = BusLineBO;
                //courses = new ObservableCollection<BO.BusLineCourse>(BusLineBO.listOfCourses);
                //BusLineCourseDataGrid.DataContext = courses;
                //BusLineCourseDataGrid.DataContext = BusLineBO.listOfCourses;

                //cbBusLineID.Text = ((PO.ListedBus)cbBusLineID.SelectedItem).Show;
                //cbBusLineID.IsDropDownOpen = false;
                //gridBusLine.Visibility = Visibility.Visible;
                //btReset.IsEnabled = true;
                //MessageBox.Show(BusLineBO.ToString(), "BusLine");
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbBusLineID_DropDownOpened(object sender, EventArgs e) => cbBusLineID_Handle();
        private void cbBusLineID_KeyUp(object sender, KeyEventArgs e) => cbBusLineID_Handle();
        private void cbBusLineID_Handle()
        {
            if (!cbBusLineID.IsDropDownOpen) return;
            var ctb = cbBusLineID.Template.FindName("PART_EditableTextBox", cbBusLineID) as TextBox;
            if (ctb == null) return;
            var caretPos = ctb.CaretIndex;

            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cbBusLineID.Items);
            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(cbBusLineID.Text))
                {
                    return true;
                }
                else
                {
                    if (((PO.ListedBus)o).Show.Contains(cbBusLineID.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            });

            itemsViewOriginal.Refresh();
            ctb.CaretIndex = caretPos;
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            //cbBusLineID.Text = "";
            cbBusLineID.SelectedIndex = -1;
            //btReset.IsEnabled = false;
            //gridBusLine.Visibility = Visibility.Hidden;
            viewModel.Reset();
        }

        private void btnListed_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            StackPanel parent = (StackPanel)btn.Parent;
            Label lbl = (Label)parent.FindName("lbListed");
            MessageBox.Show(lbl.Tag.ToString());
            MessageBox.Show(((PO.ListedBus)btn.DataContext).Bus.Name);
        }
    }
}
