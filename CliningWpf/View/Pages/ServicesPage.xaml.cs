using CliningWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CliningWpf.View.Pages
{
    public partial class ServicesPage : Page
    {
        private readonly IvanovEntities context; 

        public ServicesPage()
        {
            InitializeComponent();

            
            context = new IvanovEntities();

            

            EmployeesListBox.ItemsSource = context.Employees.ToList();


           
            EmployeesListBox.SelectionChanged += EmployeesListBox_SelectionChanged;
        }

        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            Employees selectedEmployee = EmployeesListBox.SelectedItem as Employees;

            if (selectedEmployee != null)
            {
                try
                {
                   
                    var equipmentIDs = context.Service_Request
                        .Where(sr => sr.EmployeeID == selectedEmployee.EmployeeID)
                        .Select(sr => sr.EmployeeID)
                        .ToList();

                 
                    var selectedEmployeeEquipment = context.Requests
                        .Where(equipment => equipmentIDs.Contains(equipment.EquipmentID))
                        .ToList();

                    SelectedEmployeeServicesListBox.ItemsSource = selectedEmployeeEquipment;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении информации из базы данных: " + ex.Message);
                }
            }
        }





        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose();
        }
    }
}
