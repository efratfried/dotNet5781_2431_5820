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
using ViewModel;
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
                PO.User MyUser = new PO.User();
                BO.User B= bl.GetUser(manager_Name.Text, manager_password.Text);//.Where(me => me.UserName == manager_Name.Text).Cast<PO.User>().ToList().First();
                B.DeepCopyTo(MyUser);

                if(MyUser!=null)
                {
                    if(MyUser.Password==manager_password.Text && MyUser.Me == BO.Access.Manager || MyUser.Password == manager_password.Text && MyUser.Me == BO.Access.Passnger)
                    {
                        ManagerWindow win = new ManagerWindow(MyUser);
                        win.ShowDialog();
                    }
                    //else if()
                    //else
                    //{                  
                    //    //MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}

                }
                else
                {
                    //MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                //MessageBox.Show(ex.Message + ex.InnerException, "wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
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
