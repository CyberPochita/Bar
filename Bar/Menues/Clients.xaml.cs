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
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    /// 
    public class Card
    {
        public int id { get; set; }
        public int idTable { get; set; }
        public DateTime dateOrder { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }

    public partial class Clients : UserControl
    {
        public ObservableCollection<Card> cards { get; set; }

        public Clients()
        {
            InitializeComponent();
            cards = new ObservableCollection<Card>();
            DataContext = this;

            for (int i = 0; i < 100; i++)
            {
                cards.Add(new Card { id = i + 1, idTable = i + 2, dateOrder = DateTime.Now, price = 123 + i * 2, status = "в обработке"});
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
