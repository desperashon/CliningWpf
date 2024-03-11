using CliningWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string mes = "";


            if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                mes += "Введите имя\n";
            }
            else if (!Regex.IsMatch(NameTb.Text, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                mes += "Имя должно содержать только буквы русского алфавита\n";
            }


            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                mes += "Введите почту \n";
            }
            else if (!IsValidEmail(LoginTb.Text))
            {
                mes += "Введите корректный email\n";
            }


            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                mes += "Введите пароль\n";
            }
            else if (PasswordPb.Password.Length < 6)
            {
                mes += "Пароль должен содержать минимум 6 символов\n";
            }

            if (mes != "")
            {
                MessageBox.Show(mes);
                return;
            }


            Employees user = new Employees()
            {
               Email = LoginTb.Text,
                FullName = NameTb.Text,
                Password = PasswordPb.Password,
               
            };


            App.context.Employees.Add(user);
           
            App.context.SaveChanges();

            MessageBox.Show("Успешная регистрация!");
            NavigationService.Navigate(new MainPage());
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
            AuthorizationPage loginPage = new AuthorizationPage();

          
            NavigationService.Navigate(loginPage);
        }
    }
}
