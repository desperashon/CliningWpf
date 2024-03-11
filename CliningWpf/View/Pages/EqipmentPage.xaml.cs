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
        private readonly IvanovEntities _context; 
        public ObservableCollection<Equipment> PurchasedEquipment { get; set; }
        public ObservableCollection<Equipment> AvailableEquipment { get; set; }
        public ObservableCollection<BitmapImage> EquipmentImages { get; set; } 

        public EqipmentPage()
        {
            InitializeComponent();
            _context = new IvanovEntities(); 
            LoadData(); 
            LoadImages(); 
            DataContext = this;
        }

        private void LoadImages()
        {
            EquipmentImages = new ObservableCollection<BitmapImage>(); 

            foreach (var item in PurchasedEquipment)
            {
                try
                {
                  
                    BitmapImage bitmap = new BitmapImage(new Uri(item.Image));

                 
                    EquipmentImages.Add(bitmap);
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine("Ошибка загрузки изображения: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
 
            AvailableEquipment = new ObservableCollection<Equipment>(_context.Equipment.ToList());

          
            PurchasedEquipment = new ObservableCollection<Equipment>(_context.Equipment.Where(e => e.Quantity != 0).ToList());
        }






        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
           
            Equipment selectedEquipment = ((Button)sender).DataContext as Equipment;

            if (selectedEquipment != null)
            {
                try
                {
           
                    selectedEquipment.Quantity--;

            
                    if (selectedEquipment.Quantity < 0)
                    {
                        MessageBox.Show("Недостаточно оборудования на складе.");
                        selectedEquipment.Quantity++; 
                        return;
                    }

                
                    _context.Entry(selectedEquipment).State = EntityState.Modified;
                    _context.SaveChanges();

             

                   
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
