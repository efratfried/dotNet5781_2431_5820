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
using PO;
using UI;
namespace PL
{
    /// <summary>
    /// Interaction logic for conecting.xaml
    /// </summary>
     public partial class conecting : Window
     {
        IBL bl;
        BO.User user;
        public conecting(IBL bl1)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = bl1;
        }
        private void entering(object sender, RoutedEventArgs e)
        {
            if (manager_Name.Text.Length!=0 && manager_password.Text.Length!=0)
            {
               PO.User MyUser = new PO.User();
                user= bl.GetUser(manager_Name.Text, manager_password.Text);//.Where(me => me.UserName == manager_Name.Text).Cast<PO.User>().ToList().First();
                user.DeepCopyTo(MyUser);

                if (MyUser != null)
                {
                    try
                    {
                        if (MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Manager) //|| MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Passnger)
                        {
                            ManagerWindow win = new ManagerWindow(MyUser);
                            win.ShowDialog();
                        }

                        else
                        {
                            MessageBoxResult res = MessageBox.Show("The user doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        }
                    }
                    catch (BO.BadOpenWindow ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "couldn't open the window", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("The user doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
            }
            else if (user_Name.Text.Length != 0 && user_password.Text.Length != 0)
            {
                PO.User MyUser = new PO.User();
                user = bl.GetUser(user_Name.Text, user_password.Text);//.Where(me => me.UserName == manager_Name.Text).Cast<PO.User>().ToList().First();
                user.DeepCopyTo(MyUser);
                if (MyUser != null)
                {
                    try
                    {
                        if (MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Passnger) //|| MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Passnger)
                        {
                            UserWindow win = new UserWindow(MyUser);
                            win.ShowDialog();
                        }

                        else
                        {
                            MessageBoxResult res = MessageBox.Show("The user doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        }
                    }
                    catch (BO.BadOpenWindow ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "couldn't open the window", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("The user doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("The user doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
        }
        


        private void manager_passowrd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void manager_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NewUser_password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Newuser_namePassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void user_password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void user_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
