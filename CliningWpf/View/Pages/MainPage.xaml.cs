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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MainFrm.Navigate(new GlavPage());
        }
        private void MainPageLb_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.NavigationService.Navigate(new GlavPage());
        }

        private void EqipmentPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.NavigationService.Navigate(new EqipmentPage());
        }

        private void EmplouePage_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.NavigationService.Navigate(new EmplouePage());
        }

        private void Services_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.NavigationService.Navigate(new ServicesPage());
        }

        private void OrderPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.NavigationService.Navigate(new OrderPage());
        }
    }
}
