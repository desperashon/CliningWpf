using CliningWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CliningWpf.View.Pages
{
    public partial class OrderPage : Page
    {
        private readonly IvanovEntities _context;

        public OrderPage()
        {
            InitializeComponent();

            _context = new IvanovEntities();
            InitializeComboBoxes();
          
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(description) && cmbEquipment.SelectedItem != null && cmbEmployees.SelectedItem != null)
            {
                try
                {
                    var selectedEmployee = cmbEmployees.SelectedItem as Employees;
                    var selectedEquipment = cmbEquipment.SelectedItem as Equipment;

                    // Создание нового заказа
                    var newRequest = new Requests
                    {
                        Description = description,
                        Cost = decimal.Parse(CostTextBox.Text),
                        DateRequested = DatePicker.SelectedDate ?? DateTime.Now 
                    };

                    _context.Requests.Add(newRequest);
                    _context.SaveChanges();

                    
                    var serviceRequest = new Service_Request
                    {
                        RequestID = newRequest.RequestID,
                        EmployeeID = selectedEmployee.EmployeeID,
                        
                    };

                    _context.Service_Request.Add(serviceRequest);
                    _context.SaveChanges();

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении заказа: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Описание заказа, стоимость или выбор сотрудника/оборудования не может быть пустым.");
            }
        }

        private void InitializeComboBoxes()
        {
            cmbEquipment.ItemsSource = _context.Equipment.ToList();
           

            cmbEmployees.ItemsSource = _context.Employees.ToList();
           
        }

        

        // Освобождение ресурсов при выгрузке страницы
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _context.Dispose();
        }
    }
}
