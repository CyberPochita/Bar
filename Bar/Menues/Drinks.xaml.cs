using Bar.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Drinks(DatabaseModule db)
        {
            InitializeComponent();
            cards = new ObservableCollection<CardDrink>();
            List<Drink> drinks = db.GetDrinks();
            DataContext = this;

            foreach(var drink in drinks)
            {
                cards.Add(new CardDrink { Id = drink.Id, title = drink.title, isAlcohol = drink.isAlcohol, price = drink.price, volume = drink.volume, degree = drink.alcoholDegree});
            }
        }
    }
}
