using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    /*#region variables
     * protected string UserName { set; get; }
        protected string Password { set; get; }
        public Access Me { protected set; get; }
    #endregion*/

    public abstract class User : DependencyObject
    {
        static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(User));
        static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(User));
        static readonly DependencyProperty MeProperty = DependencyProperty.Register("Me", typeof(BO.Access), typeof(User));

        public string UserName { get => (string)GetValue(UserNameProperty); set => SetValue(UserNameProperty, value); }
        public string Password { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
        public BO.Access Me { get => (BO.Access)GetValue(MeProperty); set => SetValue(MeProperty, value); }
    }
}
