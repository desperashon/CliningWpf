using CliningWpf.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CliningWpf.View.Pages
{
    public partial class EmplouePage : Page
    {
        public EmplouePage()
        {
            InitializeComponent();

           

         
            EmployeesListBox.ItemsSource = App.context.Employees.ToList();
            EmployeesListBox.SelectionChanged += EmployeesListBox_SelectionChanged;

           
            ContactButton.Click += ContactButton_Click;

          
      

         
            FillFloorComboBox();
        }

      
        private void FillFloorComboBox()
        {
            // Очистка комбобокса
            Floorcmb.Items.Clear();

           
            for (int i = 1; i <= 10; i++)
            {
                Floorcmb.Items.Add($"Этаж {i}");
            }
        }

      
        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var selectedEmployee = EmployeesListBox.SelectedItem as Employees;

         
            if (selectedEmployee != null)
            {
                
                EmployeePhoto.Source = new BitmapImage(new Uri(selectedEmployee.Photo));
                EmployeeId.Text = $"ID: {selectedEmployee.EmployeeID}";
                EmployeeName.Text = $"Имя: {selectedEmployee.FullName}";
                EmployeePhone.Text = $"Телефон: {selectedEmployee.Phone}";
                EmployeeEmail.Text = $"Email: {selectedEmployee.Email}";
            }
            else
            {
                
                EmployeePhoto.Source = null;
                EmployeeId.Text = string.Empty;
                EmployeeName.Text = string.Empty;
                EmployeePhone.Text = string.Empty;
                EmployeeEmail.Text = string.Empty;
            }
        }

     
        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
           
            MessageBox.Show("Контактная информация сотрудника");
        }

   
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
      
            if (EmployeesListBox.SelectedItem != null && Floorcmb.SelectedItem != null)
            {
               
                Employees selectedEmployee = EmployeesListBox.SelectedItem as Employees;

               
                Schedules newSchedule = new Schedules
                {
                    CleaningLocation = Floor.Text,
                    EmployeeID = selectedEmployee.EmployeeID, 
                    Floor = (string)Floorcmb.SelectedItem, 
                    StartDate = (DateTime)CleaningStartDate.SelectedDate, 
                    EndDate = (DateTime)CleaningEndDate.SelectedDate 
                };

               
                App.context.Schedules.Add(newSchedule);
                App.context.SaveChanges();

            
                Floor.Text = "";
                EmployeesListBox.SelectedItem = null;
                Floorcmb.SelectedItem = null;
                CleaningStartDate.SelectedDate = null;
                CleaningEndDate.SelectedDate = null;

          
                MessageBox.Show("Запись успешно добавлена в таблицу Schedules.");
            }
            else
            {
         
                MessageBox.Show("Выберите сотрудника из списка и укажите этаж.");
            }
        }

    }
}
