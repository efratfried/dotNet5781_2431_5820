using BLAPI;
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
using PO;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for busseswindow.xaml
    /// </summary>
    public partial class busseswindow : Window
    {
        IBL bl;
        PO.Bus currentbus;
        public busseswindow(IBL _bl)
        {
            InitializeComponent();
            Refreshbusses_listComboBox();
          
             

            /*void RefreshAllLineStationsOfLineGrid()
            {
                IEnumerable<PO.BusLine> busLines;
                lineStationDataGrid.DataContext = bl.GetAllBuss();
            }*/
            //   RefreshAllBussesComboBox();


            // cbStudentID.DisplayMemberPath = "Name";//show only specific Property of object
            //  cbStudentID.SelectedValuePath = "ID";//selection return only specific Property of object

            // studentCourseDataGrid.IsReadOnly = true;
            // courseDataGrid.IsReadOnly = true;

        }

        /*
         void RefreshAllRegisteredCoursesGrid()
        {
            studentCourseDataGrid.DataContext = bl.GetAllCoursesPerStudent(curStu.ID);
        }

        void RefreshAllNotRegisteredCoursesGrid()
        {
            List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => bl.GetAllCoursesPerStudent(curStu.ID).All(c2 => c2.ID != c1.ID)).ToList();
            courseDataGrid.DataContext = listOfUnRegisteredCourses;
        }

        private void cbStudentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curStu = (cbStudentID.SelectedItem as BO.Student);
            gridOneStudent.DataContext = curStu;

            if (curStu != null)
            {
                //list of courses of selected student
                RefreshAllRegisteredCoursesGrid();
                //list of all courses (that selected student is not registered to it)
                RefreshAllNotRegisteredCoursesGrid();                
            }
        }
    */
        private void Refreshbusses_listComboBox()//refresh the combobox each time the user changes the selection 
        {
            IEnumerable<PO.Bus> bus = bl.GetAllBuss().Cast<PO.Bus>();
            currentbus = bus.FirstOrDefault();
            busses_list.ItemsSource = bus;
            Licensenumbus.DataContext = currentbus.LicenseNum;
            foul_status.DataContext = currentbus.foul;
            km_.DataContext = currentbus.KM;
            aviability_status.DataContext = currentbus.Status;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (busses_list.SelectedIndex < 0)
                return;
            try
            {
                IEnumerable<BO.Bus> templist = bl.GetAllBuss().Cast<BO.Bus>();

                currentbus = busses_list.SelectedItem as PO.Bus;
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void inner_info_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (inner_info.SelectedItem == accident)
                {
                    BO.Bus bus = bl.GetBus(currentbus.LicenseNum.ToString());
                    inner_info.DataContext = bus.AccidentsDuco;
                }

                if (inner_info.SelectedItem == treats)
                {
                    BO.Bus bus = bl.GetBus(currentbus.LicenseNum.ToString());
                    inner_info.DataContext = bus.TreatsDuco;
                }

                if (inner_info.SelectedItem == last_drives)
                {
                    BO.Bus bus = bl.GetBus(currentbus.LicenseNum.ToString());
                    inner_info.DataContext = bus.drivingBusesDuco;
                }
                else
                {
                    throw new BO.BadBusLineIdException("you didnt choose a duocment");
                }
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //currentbusID.DataContext = bl.GetAllBuss();
            //currentbusID.SelectedIndex = 0; //index of the object to be selected
        }

        private void start_driving_Click(object sender, RoutedEventArgs e)
        {
                delete_bus.IsEnabled = false;
                update_bus.IsEnabled = false;
                start_driving.IsEnabled = false;
                start_filling_foul.IsEnabled = false;
                start_treatment.IsEnabled = false;           
        }

        private void start_filling_foul_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_treatment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_bus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_bus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteBus(currentbus.LicenseNum.ToString());
                Refreshbusses_listComboBox();
            }
            catch (BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Could'nt delete the bus", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_bus_Click(object sender, RoutedEventArgs e)
        {
            AddBus ab = new AddBus();
            ab.ShowDialog();

        }

        private void aviability_status_TextBlock(object sender, DependencyPropertyChangedEventArgs e)
        {
       //     if(currentbus.sts)
        }

        private void foul_status_TextBlock(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        /* private void refresh(object sender, DependencyPropertyChangedEventArgs e)
         {
             var iweil = bl.GetAllBuss().Select(item => new InvoiceWithEntryInfo().ToList();
         }*/




        //  currentbus = DataContext as Bus;
    }
}


