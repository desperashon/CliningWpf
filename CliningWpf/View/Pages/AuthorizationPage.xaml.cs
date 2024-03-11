using CliningWpf.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CliningWpf.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
           
            Employees user = App.context.Employees
                  .FirstOrDefault(w => w.Password == PasswordPb.Password && w.Email == LoginTb.Text);

            if (user != null)
            {

                
                NavigationService.Navigate(new MainPage());

            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
                PasswordPb.Clear();
                LoginTb.Clear();
            }
        }

       

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
    
            RegistrationPage registrationPage = new RegistrationPage();

            
            NavigationService.Navigate(registrationPage);
        }
    }
}
