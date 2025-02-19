using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;

namespace Bar.Models
{
    public class DatabaseModule
    {
        private readonly ApplicationContext _context;

        public DatabaseModule(ApplicationContext context)
        {
            this._context = context;
        }

        public void AddOrder(int tableId, Drink drink) // Сделать заказ
        {
            double cost = drink.price;

            Order newOrder = new Order
            {
                idTable = tableId,
                totalPrice = cost,
                datetime = DateTime.Now,
                status = "В обработке"
            };

            OrderItem newOrderItem = new OrderItem
            {
                idOrder = newOrder.Id,
                idDrink = drink.Id,
                priceUnit = drink.price
            };

            _context.orders.Add(newOrder);
            _context.orderItems.Add(newOrderItem);

            _context.SaveChanges();
            Console.WriteLine("Заказ сделан");
        }

        public void AddOrder(int tableId, List<Drink> drinks) // Сделать заказ на несколько напитков
        {
            double cost = 0;
            foreach (Drink drink in drinks)
            {
                cost += drink.price;
            }

            Order newOrder = new Order
            {
                idTable = tableId,
                totalPrice = cost,
                datetime = DateTime.Now,
                status = "В обработке"
            };

            _context.orders.Add(newOrder);

            foreach (Drink drink in drinks)
            {
                OrderItem newOrderItem = new OrderItem
                {
                    idOrder = newOrder.Id,
                    idDrink = drink.Id,
                    priceUnit = drink.price
                };

                _context.orderItems.Add(newOrderItem);
            }

            _context.SaveChanges();
            Console.WriteLine("Заказ сделан");
        }

        public void DeleteOrder(int id)
        {
            foreach (OrderItem order in _context.orderItems)
            {
                if (order.idOrder == id)
                {
                    _context.orderItems.Remove(order);
                }
            }

            foreach (var order in _context.orders)
            {
                if (order.Id == id)
                {
                    _context.orders.Remove(order);
                }
            }

            _context.SaveChanges();
            Console.WriteLine("Заказ удален");
        }

        public void EditOrder(Order order, int tableId, string status)
        {
            order.idTable = tableId;
            order.status = status;

            _context.SaveChanges();
            Console.WriteLine("Заказ изменен");
        }

        public Order GetOrder(int id) // Получить один заказ
        {
            Order? order = _context.orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                throw new Exception("Объект содержит null");
            }

            return order;
        }

        public List<Order> GetOrders() => _context.orders.ToList(); // Получить все заказы

        public List<Order> GetOrdersByStatus(string status) // Получить заказы по статусу
        {
            List<Order> orders = new List<Order>();

            foreach (Order order in _context.orders)
            {
                if (status == order.status)
                {
                    orders.Add(order);
                }
            }

            return orders;
        }

        public List<Order> GetOrdersByDatetime(DateTime dateTime) // Получить заказы по актуальности
        {
            List<Order> orders = new List<Order>();

            foreach (Order order in _context.orders)
            {
                if (dateTime == order.datetime)
                {
                    orders.Add(order);
                }
            }

            return orders;
        }

        public List<Drink> GetDrinks() => _context.drinks.ToList(); // Получить все напитки

        public List<Drink> GetDrinksByTitle(string title) // Получить все напитки по названию
        {
            List<Drink> drinks = new List<Drink>();

            foreach (var drink in _context.drinks)
            {
                if (drink.title.Contains(title))
                {
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetDrinksByIsAlcohol(bool isAlcohol) // Получить все алкоголические/не алкоголические напитки
        {
            List<Drink> drinks = new List<Drink>();

            foreach (var drink in _context.drinks)
            {
                if (drink.isAlcohol == isAlcohol)
                {
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetDrinksByPrice(double price) // Получить все напитки по цене
        {
            List<Drink> drinks = new List<Drink>();

            foreach (var drink in _context.drinks)
            {
                if (drink.price == price)
                {
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetDrinksByVolume(double volume) // Получить напитки по объему
        {
            List<Drink> drinks = new List<Drink>();

            foreach (var drink in _context.drinks)
            {
                if (drink.volume == volume)
                {
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetDrinksByAlcoholDegree(double degree) // Получить все напитки по градусу
        {
            List<Drink> drinks = new List<Drink>();

            foreach (var drink in _context.drinks)
            {
                if (drink.alcoholDegree == degree)
                {
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public void AddClient(string firstName, string lastName, string phoneNumber, DateTime visitDate, string preference, int visitCount)
        {
            Client client = new Client
            {
                firstName = firstName,
                lastName = lastName,
                phoneNumber = phoneNumber,
                visitDate = visitDate,
                preferences = preference,
                visitCount = visitCount,
            };

            _context.clients.Add(client);
            _context.SaveChanges();
            Console.WriteLine("Добавлен клиент");
        }

        public void EditClient(Client client, string firstName, string lastName, string phoneNumber, DateTime visitDate, string preference, int visitCount)
        {
            client.firstName = firstName;
            client.lastName = lastName;
            client.phoneNumber = phoneNumber;
            client.visitDate = visitDate;
            client.preferences = preference;
            client.visitCount = visitCount;

            _context.SaveChanges();
            Console.WriteLine("Добавлен клиент");
        }

        public void DeleteClient(int id)
        {
            foreach (var client in _context.clients)
            {
                if (client.Id == id)
                {
                    _context.Remove(client);
                }
            }
        }

        public List<Client> GetClients()
        {
            List<Client> sortedClients = _context.clients.ToList();
            sortedClients.Sort((cl1, cl2) => cl1.lastName.CompareTo(cl2.lastName));
            return sortedClients;
        }

        public void AddEvent(string eventName, DateTime datetime, string description, int maxPeople)
        {
            Event newEvent = new Event
            {
                eventName = eventName,
                datetime = datetime,
                description = description,
                maxNumberParticipants = maxPeople
            };

            _context.events.Add(newEvent);
            _context.SaveChanges();
            Console.WriteLine("Добавлено новое событие");
        }

        public void EditEvent(Event eevent, string eventName, DateTime datetime, string description, int maxPeople)
        {
            eevent.eventName = eventName;
            eevent.datetime = datetime;
            eevent.description = description;
            eevent.maxNumberParticipants = maxPeople;

            _context.SaveChanges();
            Console.WriteLine("Изменено событие");
        }

        public void DeleteEvent(int id)
        {
            foreach(var events in _context.events)
            {
                if(events.Id == id)
                {
                    _context.Remove(events);
                }
            }
        }

        public List<Event> GetEvents() => _context.events.ToList();

        public List<Table> GetTables() => _context.tables.ToList();

        public void AddReview(int idClient, int score, string text, DateTime dateTime)
        {
            Reviews review = new Reviews
            {
                idClient = idClient,
                score = score,
                textReview = text,
                dateReview = dateTime,
            };

            _context.reviews.Add(review);
            _context.SaveChanges();
            Console.WriteLine("Добавлено новый отзыв");
        }

        public void EditReview(Reviews review, int idClient, int score, string text, DateTime dateTime)
        {
            review.idClient = idClient;
            review.score = score;
            review.textReview = text;
            review.dateReview = dateTime;

            _context.SaveChanges();
            Console.WriteLine("Изменен отзыв");
        }

        public void DeleteReview(int id)
        {
            foreach(var review in _context.reviews)
            {
                if(review.Id == id)
                {
                    _context.Remove(review);
                }
            }

            _context.SaveChanges();
            Console.WriteLine("Удален отзыв");
        }

        public List<Reviews> GetReviews() => _context.reviews.ToList();
    }
}
