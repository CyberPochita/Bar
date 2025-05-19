using Bar.Menues.AdditionUserControls;
using Bar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Bar.Menues
{
    public partial class Drinks : UserControl
    {
        private DatabaseModule db;
        private DrinksList drinksList;
        public Drinks(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            drinksList = new(db);
            GridDrinks.Children.Add(drinksList);
        }

        private void NameDrink_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholderNameDrink.Visibility = string.IsNullOrWhiteSpace(NameDrink.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string word = NameDrink.Text.Trim();

            drinksList.SubstringSearch(word);
        }

        private void ComboFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboFiltr.SelectedItem is ComboBoxItem selectedItem)
            {
                string filter = selectedItem.Content.ToString();
                drinksList.ApplyFilter(filter);
            }
        }
    }
}
