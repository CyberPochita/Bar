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
    public class CardDrink
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public bool isAlcohol { get; set; }
        public double price { get; set; }
        public double volume { get; set; }
        public double degree { get; set; }
    }

    public partial class Drinks : UserControl
    {
        public ObservableCollection<CardDrink> cards { get; set; }
        private int clickCount = 0;
        private string originalColor = "#242424";
        private string selectedColor = "#2A3527";
        private List<string>? selectedDrinks;
        private DatabaseModule db;

        public Drinks(DatabaseModule db)
        {
            InitializeComponent();
            cards = new ObservableCollection<CardDrink>();
            this.db = db;
            List<Drink> drinks = db.GetDrinks();
            selectedDrinks = new List<string>();
            DataContext = this;

            foreach(var drink in drinks)
            {
                cards.Add(new CardDrink { Id = drink.Id, title = drink.title, isAlcohol = drink.isAlcohol, price = drink.price, volume = drink.volume, degree = drink.alcoholDegree});
            }
        }

        public List<Drink> GetSelectedDrinks()
        {
            return new List<Drink>(db.GetDrinksByTitles(selectedDrinks));
        }

        private void ContainerBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                clickCount++;

                var timer = new System.Timers.Timer(300);
                timer.Elapsed += (s, args) =>
                {
                    clickCount = 0;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();

                if(clickCount == 2)
                {
                    var border = sender as Border;

                    if (border.Background.ToString() == new SolidColorBrush((Color)ColorConverter.ConvertFromString(originalColor)).ToString())
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedColor));
                        var cardDrink = border.DataContext as CardDrink;

                        if(cardDrink != null)
                        {
                            string title = cardDrink.title;
                            selectedDrinks.Add(title);
                        }
                    }
                    else
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(originalColor));

                        var cardDrink = border.DataContext as CardDrink;
                        if (cardDrink != null)
                        {
                            foreach (var item in selectedDrinks)
                            {
                                if (cardDrink.title == item)
                                {
                                    selectedDrinks.Remove(item);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
