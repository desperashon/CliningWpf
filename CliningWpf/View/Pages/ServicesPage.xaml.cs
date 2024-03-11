using CliningWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CliningWpf.View.Pages
{
    public partial class ServicesPage : Page
    {
        private readonly IvanovEntities context; // Контекст базы данных

        public ServicesPage()
        {
            InitializeComponent();

            // Создаем новый контекст базы данных
            context = new IvanovEntities();

            // Привязываем список сотрудников к контексту базы данных

            EmployeesListBox.ItemsSource = context.Employees.ToList();


            // Привязываем событие выбора элемента в списке сотрудников
            EmployeesListBox.SelectionChanged += EmployeesListBox_SelectionChanged;
        }

        // Обработчик события выбора элемента в списке сотрудников
        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранного сотрудника
            Employees selectedEmployee = EmployeesListBox.SelectedItem as Employees;

            if (selectedEmployee != null)
            {
                try
                {
                    // Выполняем LINQ-запрос для выбора всех EquipmentID, связанных с выбранным сотрудником
                    var equipmentIDs = context.Service_Request
                        .Where(sr => sr.EmployeeID == selectedEmployee.EmployeeID)
                        .Select(sr => sr.EmployeeID)
                        .ToList();

                    // Выполняем запрос к базе данных для загрузки информации о выбранном оборудовании
                    var selectedEmployeeEquipment = context.Requests
                        .Where(equipment => equipmentIDs.Contains(equipment.EquipmentID))
                        .ToList();

                    // Привязываем результаты к SelectedEmployeeServicesListBox
                    SelectedEmployeeServicesListBox.ItemsSource = selectedEmployeeEquipment;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении информации из базы данных: " + ex.Message);
                }
            }
        }





        // При закрытии страницы освобождаем ресурсы
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose(); // Освобождаем контекст базы данных
        }
    }
}
