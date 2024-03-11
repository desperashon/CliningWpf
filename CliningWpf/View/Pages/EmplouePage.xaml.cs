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

           

            // Привязка события выбора сотрудника из списка
            EmployeesListBox.ItemsSource = App.context.Employees.ToList();
            EmployeesListBox.SelectionChanged += EmployeesListBox_SelectionChanged;

            // Привязка события нажатия на кнопку "Связаться"
            ContactButton.Click += ContactButton_Click;

            // Привязка события нажатия на кнопку "Вызвать"
      

            // Заполнение комбобокса этажей
            FillFloorComboBox();
        }

        // Метод для очистки и заполнения комбобокса этажей
        private void FillFloorComboBox()
        {
            // Очистка комбобокса
            Floorcmb.Items.Clear();

            // Заполнение комбобокса этажами
            // Предположим, что у вас есть список этажей, который нужно добавить в комбобокс
            // Пример заполнения:
            for (int i = 1; i <= 10; i++)
            {
                Floorcmb.Items.Add($"Этаж {i}");
            }
        }

        // Обработчик события выбора сотрудника из списка
        private void EmployeesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранного сотрудника
            var selectedEmployee = EmployeesListBox.SelectedItem as Employees;

            // Проверяем, не является ли выбранный элемент null
            if (selectedEmployee != null)
            {
                // Обновляем информацию о сотруднике
                EmployeePhoto.Source = new BitmapImage(new Uri(selectedEmployee.Photo));
                EmployeeId.Text = $"ID: {selectedEmployee.EmployeeID}";
                EmployeeName.Text = $"Имя: {selectedEmployee.FullName}";
                EmployeePhone.Text = $"Телефон: {selectedEmployee.Phone}";
                EmployeeEmail.Text = $"Email: {selectedEmployee.Email}";
            }
            else
            {
                // Очищаем информацию о сотруднике, если ничего не выбрано
                EmployeePhoto.Source = null;
                EmployeeId.Text = string.Empty;
                EmployeeName.Text = string.Empty;
                EmployeePhone.Text = string.Empty;
                EmployeeEmail.Text = string.Empty;
            }
        }

        // Обработчик события нажатия на кнопку "Связаться"
        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            // Ваш код для обработки действия при нажатии на кнопку "Связаться"
            MessageBox.Show("Контактная информация сотрудника");
        }

        // Обработчик события нажатия на кнопку "Вызвать"
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли сотрудник и выбран ли этаж
            if (EmployeesListBox.SelectedItem != null && Floorcmb.SelectedItem != null)
            {
                // Получаем выбранного сотрудника из списка
                Employees selectedEmployee = EmployeesListBox.SelectedItem as Employees;

                // Создаем новую запись для расписания
                Schedules newSchedule = new Schedules
                {
                    CleaningLocation = Floor.Text,
                    EmployeeID = selectedEmployee.EmployeeID, // Предположим, что Id сотрудника соответствует EmployeeId в таблице Schedules
                    Floor = (string)Floorcmb.SelectedItem, // Получаем выбранный этаж из комбобокса
                    StartDate = (DateTime)CleaningStartDate.SelectedDate, // Получаем дату начала уборки из DatePicker
                    EndDate = (DateTime)CleaningEndDate.SelectedDate // Получаем дату окончания уборки из DatePicker
                };

                // Добавляем новую запись в базу данных
                App.context.Schedules.Add(newSchedule);
                App.context.SaveChanges();

                // Очищаем поля после сохранения записи
                Floor.Text = "";
                EmployeesListBox.SelectedItem = null;
                Floorcmb.SelectedItem = null;
                CleaningStartDate.SelectedDate = null;
                CleaningEndDate.SelectedDate = null;

                // Оповещаем пользователя об успешном создании записи
                MessageBox.Show("Запись успешно добавлена в таблицу Schedules.");
            }
            else
            {
                // Если сотрудник не выбран или этаж не выбран, выдаем сообщение об ошибке
                MessageBox.Show("Выберите сотрудника из списка и укажите этаж.");
            }
        }

    }
}
