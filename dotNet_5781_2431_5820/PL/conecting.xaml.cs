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
     public partial class conecting : Window
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
                var MyUser = bl.GetAllUsers().Where(me => me.UserName == manager_Name.Text).Cast<PO.User>().ToList();
                if(MyUser!=null)
                {
                    if(MyUser[0].Password==manager_password.Text)
                    {
                        Window1 win = new Window1(MyUser);
                        win.ShowDialog();
                    }
                    else
                    {                  
                        MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (user_Name.Text.Length != 0 && user_password.Text.Length != 0)
            {//open a different window from the manager's
                var MyUser = bl.GetAllUsers().Where(me => me.UserName == user_Name.Text).Cast<PO.User>().ToList();
                if (MyUser != null)
                {
                    if (MyUser[0].Password == user_password.Text)
                    {
                        Window1 win = new Window1();....
                        win.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (Newuser_name.Text.Length != 0 && NewUser_password.Text.Length != 0)
            {
                var MyUser = bl.GetAllUsers().Where(me => me.UserName == Newuser_name.Text).Cast<PO.User>().ToList();
                if (MyUser == null)
                {//open a differrent window than the manager's
                    try
                    {
                        BO.User user = MyUser.Cast<BO.User >().First();
                        bl.AddUser(user);
                        this.Close();
                    }
                    catch (BO.BadUserName_PasswordException ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Console.WriteLine("Your sign in succeeded");
                    Window1 win = new Window1(); //open a differrent window than the manager's

                    win.ShowDialog();
                    else
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
            }    
        }

        private void manager_passowrd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void manager_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
