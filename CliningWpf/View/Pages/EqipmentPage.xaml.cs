using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CliningWpf.Models;

namespace CliningWpf.View.Pages
{
    public partial class EqipmentPage : Page
    {
        private readonly IvanovEntities _context; // Замените YourDbContext на ваш контекст базы данных

        public ObservableCollection<Equipment> PurchasedEquipment { get; set; }
        public ObservableCollection<Equipment> AvailableEquipment { get; set; }
        public ObservableCollection<BitmapImage> EquipmentImages { get; set; } // Коллекция для хранения изображений

        public EqipmentPage()
        {
            InitializeComponent();
            _context = new IvanovEntities(); // Инициализируйте ваш контекст базы данных
            LoadData(); // Загрузка данных из базы данных
            LoadImages(); // Загрузка изображений
            DataContext = this;
        }

        private void LoadImages()
        {
            EquipmentImages = new ObservableCollection<BitmapImage>(); // Создаем новую коллекцию для изображений

            foreach (var item in PurchasedEquipment)
            {
                try
                {
                    // Создаем новый BitmapImage из URL-адреса
                    BitmapImage bitmap = new BitmapImage(new Uri(item.Image));

                    // Добавляем BitmapImage в коллекцию
                    EquipmentImages.Add(bitmap);
                }
                catch (Exception ex)
                {
                    // Обработка ошибок, если не удается загрузить изображение
                    Console.WriteLine("Ошибка загрузки изображения: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            // Получите доступное для заказа оборудование из базы данных, количество которого равно 0
            AvailableEquipment = new ObservableCollection<Equipment>(_context.Equipment.ToList());

            // Получите закупленное оборудование из базы данных
            PurchasedEquipment = new ObservableCollection<Equipment>(_context.Equipment.Where(e => e.Quantity != 0).ToList());
        }






        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получите выбранный элемент из списка доступного для заказа оборудования
            Equipment selectedEquipment = ((Button)sender).DataContext as Equipment;

            if (selectedEquipment != null)
            {
                try
                {
                    // Уменьшите количество оборудования в списке доступного для заказа на 1
                    selectedEquipment.Quantity--;

                    // Проверяем, что количество оборудования не стало отрицательным
                    if (selectedEquipment.Quantity < 0)
                    {
                        MessageBox.Show("Недостаточно оборудования на складе.");
                        selectedEquipment.Quantity++; // Возвращаем обратно уменьшенное количество
                        return;
                    }

                    // Сохраняем изменения в базе данных
                    _context.Entry(selectedEquipment).State = EntityState.Modified;
                    _context.SaveChanges();

                    // Обновляем список закупленного оборудования

                    // Обновляем данные контекста
                    DataContext = this;
                    PurchasedEquipment = new ObservableCollection<Equipment>(_context.Equipment.Where(d => d.Quantity != 0).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении количества оборудования: " + ex.Message);
                }
            }
        }


    }
}
