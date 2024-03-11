using CliningWpf.View.Pages;
using System.Linq;
using System.Windows;

namespace CliningWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrm.Navigate(new AuthorizationPage());
            PhoneTb.Text = "888-888-888";
            CityCmb.DisplayMemberPath = "CityName";
            CityCmb.SelectedValuePath = "CityID";
            CityCmb.ItemsSource = App.context.City.ToList();
        }
    }
}
