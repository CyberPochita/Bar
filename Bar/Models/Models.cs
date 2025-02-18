using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bar.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> clients { get; set; } = null!;
        public DbSet<Employee> employees { get; set; } = null!;
        public DbSet<Drink> drinks { get; set; } = null!;
        public DbSet<Table> tables { get; set; } = null!;
        public DbSet<Order> orders { get; set; } = null!;
        public DbSet<OrderItem> orderItems { get; set; } = null!;
        public DbSet<Event> events { get; set; } = null!;
        public DbSet<Reviews> reviews { get; set; } = null!;
    }

    public class Client
    {
        public int Id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime visitDate { get; set; }
        public string? preferences { get; set; }
        public int visitCount { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? membership { get; set; }
        public string? post { get; set; }
        public DateTime dateOfEmployment { get; set; }
        public double pay { get; set; }
    }

    public class Drink
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public bool isAlcohol { get; set; }
        public double price { get; set; }
        public double volume { get; set; }
        public double alcoholDegree { get; set;}
    }

    public class Table
    {
        public int Id { get; set; }
        public int tableNumber { get; set; }
        public int capacity { get; set; }
        public string? status { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }

        public int idTable { get; set; }
        public Table? table { get; set; }

        public double totalPrice { get; set; }
        public DateTime datetime { get; set; }
        public string? status { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }

        public int idOrder { get; set; }
        public Order? order { get; set; }
        public int idDrink { get; set; }
        public Drink? drink { get; set; }

        public int quantity { get; set; }
        public double priceUnit { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string? eventName { get; set; }
        public DateTime datetime { get; set; }
        public string? description { get; set; }
        public int maxNumberParticipants { get; set; }
    }

    public class Reviews
    {
        public int Id { get; set; }
        
        public int idClient { get; set; }
        public Client? client { get; set; }

        public int score { get; set; }
        public string? textReview { get; set; }
        public DateTime dateReview { get; set; }
    }
}
