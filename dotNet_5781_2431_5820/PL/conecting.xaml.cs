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

namespace PL
{
    /// <summary>
    /// Interaction logic for conecting.xaml
    /// </summary>
     partial class conecting : Window
    {
        public conecting()
        {
            InitializeComponent();
        }

        private void entering(object sender, RoutedEventArgs e)
        {
            if (manager_Name.Text.Length!=0 && manager_Password.Text.Length!=0)
            {
                win.Show();
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

        private void manager_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void user_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Newuser_Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
