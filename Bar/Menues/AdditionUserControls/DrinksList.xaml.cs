using Bar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Bar.Menues.AdditionUserControls
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

    public partial class DrinksList : UserControl
    {
        public ObservableCollection<CardDrink> cards { get; set; }
        private List<CardDrink> allDrinks;
        private int clickCount = 0;
        private string originalColor = "#242424";
        private string selectedColor = "#2A3527";
        private List<string>? selectedDrinks;
        private DatabaseModule db;

        public DrinksList(DatabaseModule db)
        {
            InitializeComponent();
            cards = new ObservableCollection<CardDrink>();
            this.db = db;
            allDrinks = new List<CardDrink>();
            selectedDrinks = new List<string>();
            DataContext = this;

            LoadDrinks();
        }

        private void LoadDrinks()
        {
            List<Drink> drinks = db.GetDrinks();
            cards.Clear();
            allDrinks.Clear();

            foreach (var drink in drinks)
            {
                var card = new CardDrink
                {
                    Id = drink.Id,
                    title = drink.title,
                    isAlcohol = drink.isAlcohol,
                    price = drink.price,
                    volume = drink.volume,
                    degree = drink.alcoholDegree
                };

                cards.Add(card);
                allDrinks.Add(card);
            }
        }

        public void SubstringSearch(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                
                cards.Clear();
                foreach (var drink in allDrinks)
                {
                    cards.Add(drink);
                }
                return;
            }

            word = word.ToLower();
            
            var resultDrinks = allDrinks
                .Where(drink => drink.title != null && drink.title.ToLower().Contains(word))
                .OrderBy(drink => drink.title)
                .ToList();

            cards.Clear();
            foreach (var drink in resultDrinks)
            {
                cards.Add(drink);
            }
        }

        public void ApplyFilter(string filter)
        {
            // Создаем новую коллекцию для результатов
            var filteredCollection = new ObservableCollection<CardDrink>();

            // Применяем фильтр
            switch (filter)
            {
                case "Is alcohol":
                    foreach (var drink in allDrinks.Where(d => d.isAlcohol))
                        filteredCollection.Add(drink);
                    break;

                case "By price":
                    foreach (var drink in allDrinks.OrderBy(d => d.price))
                        filteredCollection.Add(drink);
                    break;

                case "By volume":
                    foreach (var drink in allDrinks.OrderBy(d => d.volume))
                        filteredCollection.Add(drink);
                    break;

                case "By alcohol degree":
                    foreach (var drink in allDrinks.Where(d => d.isAlcohol).OrderBy(d => d.degree))
                        filteredCollection.Add(drink);
                    break;

                default: // "All drinks"
                    foreach (var drink in allDrinks)
                        filteredCollection.Add(drink);
                    break;
            }

            // Полностью заменяем коллекцию
            cards.Clear();
            foreach (var item in filteredCollection)
            {
                cards.Add(item);
            }
        }

        public List<Drink> GetSelectedDrinks()
        {
            return new List<Drink>(db.GetDrinksByTitles(selectedDrinks));
        }

        private void ContainerBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
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

                if (clickCount == 2)
                {
                    var border = sender as Border;

                    if (border.Background.ToString() == new SolidColorBrush((Color)ColorConverter.ConvertFromString(originalColor)).ToString())
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedColor));
                        var cardDrink = border.DataContext as CardDrink;

                        if (cardDrink != null)
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
