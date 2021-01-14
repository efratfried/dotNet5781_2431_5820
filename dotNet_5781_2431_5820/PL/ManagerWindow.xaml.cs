using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        /*
         *   protected string UserName { set; get; }
        protected string Password { set; get; }
        public Access Me { protected set; get; }
         * */

        IBL bl;
        BO.User curManger;
        public ManagerWindow(IBL _bl)
        {
            InitializeComponent();

            bl = _bl;

            graduationComboBox.ItemsSource = Enum.GetValues(typeof(BO.UserGraduate));
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.UserStatus));
            personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.PersonalStatus));

            cbUserUserName.DisplayMemberPath = "Name";//show only specific Property of object
            cbUserPassword.SelectedValuePath = "Password";//selection return only specific Property of object
            RefreshAllUserComboBox();

            UserCourseDataGrid.IsReadOnly = true;
            courseDataGrid.IsReadOnly = true;

        }

        void RefreshAllUserComboBox()
        {
            cbUserID.DataContext = bl.GetAllUsers();
        }

        void RefreshAllRegisteredCoursesGrid()
        {
            UserCourseDataGrid.DataContext = bl.GetAllCoursesPerUser(curManager.ID);
        }

        void RefreshAllNotRegisteredCoursesGrid()
        {
            List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => bl.GetAllCoursesPerUser(curManager.ID).All(c2 => c2.ID != c1.ID)).ToList();
            courseDataGrid.DataContext = listOfUnRegisteredCourses;
        }

        private void cbUserID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curManager = (cbUserID.SelectedItem as BO.User);
            gridOneUser.DataContext = curManager;

            if (curManager != null)
            {
                //list of courses of selected User
                RefreshAllRegisteredCoursesGrid();
                //list of all courses (that selected User is not registered to it)
                RefreshAllNotRegisteredCoursesGrid();
            }
        }

        private void btUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curManager != null)
                    bl.UpdateUserPersonalDetails(curManager);
            }
            catch (BO.BadUserIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected User?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (curManager != null)
                {
                    bl.DeleteUser(curManager.ID);

                    RefreshAllRegisteredCoursesGrid();
                    RefreshAllNotRegisteredCoursesGrid();
                    RefreshAllUserComboBox();
                }
            }
            catch (BO.BadUserIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadUserIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btUpdateGradeInCourse_Click(object sender, RoutedEventArgs e)
        {
            BO.UserCourse scBO = ((sender as Button).DataContext as BO.UserCourse);
            GradeWindow win = new GradeWindow(scBO);
            win.Closing += WinUpdateGrade_Closing;
            win.ShowDialog();
        }

        private void WinUpdateGrade_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BO.UserCourse scBO = (sender as GradeWindow).curScBO;

            MessageBoxResult res = MessageBox.Show("Update grade for selected User?", "Verification", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                (sender as GradeWindow).cbGrade.Text = (sender as GradeWindow).gradeBeforeUpdate.ToString();
            }
            else if (res == MessageBoxResult.Cancel)
            {
                (sender as GradeWindow).cbGrade.Text = (sender as GradeWindow).gradeBeforeUpdate.ToString();
                e.Cancel = true; //window stayed open. cancel closing event.
            }
            else
            {
                try
                {
                    bl.UpdateUserGradeInCourse(curManager.ID, scBO.ID, (float)scBO.Grade);
                    RefreshAllRegisteredCoursesGrid();

                }
                catch (BO.BadUserIdCourseIDException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btUnRegisterCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.UserCourse scBO = ((sender as Button).DataContext as BO.UserCourse);
                bl.DeleteUserInCourse(curManager.ID, scBO.ID);
                RefreshAllRegisteredCoursesGrid();
                RefreshAllNotRegisteredCoursesGrid();
            }
            catch (BO.BadUserIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btRegisterCourse_Click(object sender, RoutedEventArgs e)
        {
            if (curManager == null)
            {
                MessageBox.Show("You must select a User first", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                BO.Course cBO = ((sender as Button).DataContext as BO.Course);
                bl.AddUserInCourse(curManager.ID, cBO.ID);

                RefreshAllRegisteredCoursesGrid();
                RefreshAllNotRegisteredCoursesGrid();
            }
            catch (BO.BadUserIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btAddUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This method is under construction!", "TBD", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void graduationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btUpdateStudent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}