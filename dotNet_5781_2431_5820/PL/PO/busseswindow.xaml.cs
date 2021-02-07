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

namespace PL.PO
{
    /// <summary>
    /// Interaction logic for busseswindow.xaml
    /// </summary>
    public partial class busseswindow : Window
    {
        public busseswindow()
        {
            InitializeComponent();
        }

        /*
         זה נראה לי מתאים דבר ראשון להפוך לקומבו בוקס ולא לליסט בוקס 
        בנוסף זו פונקציה שלדעתי מתאימה אבל צריך לשרמט אותה.
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
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = Binding.IndexerName;

        }

        private void inner_info_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void start_driving_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_filling_foul_Click(object sender, RoutedEventArgs e)
        {

        }

        private void start_treatment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_bus_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Height = update_bus.Height;

        }

        private void delete_bus_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Height = delete_bus.Height;
        }

        private void add_bus_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel.Height = add_bus.Height;

        }
    }
}
