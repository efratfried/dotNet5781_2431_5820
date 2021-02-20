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
    /// Interaction logic for conecting.xaml
    /// </summary>
     partial class conecting : Window
    {
        IBL bl;
        BO.User _user;
        public conecting()
        {
            InitializeComponent();
        }

        private void entering(object sender, RoutedEventArgs e)
        {
            if (manager_Name.Text.Length!=0 && manager_password.Text.Length!=0)
            {
                /*
                 *List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => bl.GetAllCoursesPerStudent(curStu.ID).All(c2 => c2.ID != c1.ID)).ToList();
            courseDataGrid.DataContext = listOfUnRegisteredCourses; */
                List<BO.User> users=bl.GetAllUser().where()
            }

            /* if (manager_Name.Text.lengh != 0)
             {
                 ManagerWindow win = new ManagerWindow(bl);
                 win.Show();
             }

             if (manager_Name.Text.lengh != 0)
             {
                 ManagerWindow win = new ManagerWindow(bl);
                 win.Show();
             }*/
            else
            {
                throw new Exception("The details are'nt correct")...;
            }
        }

        private void manager_passowrd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(manager_password.Text.Length!=0)
            {

            }
        }

        private void manager_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
