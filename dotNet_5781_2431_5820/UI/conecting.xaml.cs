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
            bl = bl1;
        }
        private void entering(object sender, RoutedEventArgs e)
        {
            ManagerWindow win = new ManagerWindow();
            win.ShowDialog();
            /*if (manager_Name.Text.Length!=0 && manager_password.Text.Length!=0)
            {
                PO.User MyUser = new PO.User();
                //BO.User B= bl.GetUser(manager_Name.Text, manager_password.Text);//.Where(me => me.UserName == manager_Name.Text).Cast<PO.User>().ToList().First();
                //B.DeepCopyTo(MyUser);

                if (MyUser != null)
                {
                    try
                    {
                        if (MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Manager || MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Passnger)
                        {
                            ManagerWindow win = new ManagerWindow(MyUser);
                            win.ShowDialog();
                        }
                        else
                        {
                            MessageBoxResult res = MessageBox.Show("The Station doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        }
                    }
                    catch (BO.BadUserName_PasswordException ex)
                    {
                        MessageBox.Show(ex.Message + ex.InnerException, "wrong details", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("The Station doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("The Station doesn't exist", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
            } */
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
