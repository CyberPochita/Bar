﻿using System;
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
using System.Windows.Shapes;
using Bar.Models;
using Microsoft.IdentityModel.Tokens;
using Bar.Menues.AdditionUserControls;

namespace Bar.Menues.AdditionDialog
{
    /// <summary>
    /// Логика взаимодействия для CompactDrinks.xaml
    /// </summary>
    public partial class CompactDrinks : Window
    {
        private DatabaseModule db;
        private DrinksList drinksList;
        public List<Drink>? listDrinks { get; private set; }

        public CompactDrinks(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            drinksList = new DrinksList(db);
            ContainerDrinks.Children.Add(drinksList);
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            listDrinks = drinksList.GetSelectedDrinks();
            if (listDrinks.IsNullOrEmpty())
            {
                MessageBox.Show("Нужно выбрать хотя бы один напиток", "Ошибка при выборе напитка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Выход из окна");
            DialogResult = false;
            Close();
        }
    }
}
