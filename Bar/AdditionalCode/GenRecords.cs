using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bar.Models;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace Bar.AdditionalCode
{
    public class GenRecords
    {
        public static void SeedDatabase(ApplicationContext context)
        {
            var fakerClient = new Faker<Client>()
                .RuleFor(c => c.firstName, f => f.Name.FirstName())
                .RuleFor(c => c.lastName, f => f.Name.LastName())
                .RuleFor(c => c.phoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.visitDate, f => f.Date.Past(1))
                .RuleFor(c => c.preferences, f => f.Lorem.Sentence())
                .RuleFor(c => c.visitCount, f => f.Random.Int(1, 100));

            var fakerEmployee = new Faker<Employee>()
                .RuleFor(e => e.firstName, f => f.Name.FirstName())
                .RuleFor(e => e.lastName, f => f.Name.LastName())
                .RuleFor(e => e.membership, f => f.Lorem.Word())
                .RuleFor(e => e.post, f => f.Lorem.Word())
                .RuleFor(e => e.dateOfEmployment, f => f.Date.Past(5))
                .RuleFor(e => e.pay, f => f.Random.Double(30000, 100000));

            var fakerDrink = new Faker<Drink>()
                .RuleFor(d => d.title, f => f.Lorem.Word())
                .RuleFor(d => d.isAlcohol, f => f.Random.Bool())
                .RuleFor(d => d.price, f => f.Random.Double(1, 100))
                .RuleFor(d => d.volume, f => f.Random.Double(0.1, 2.0))
                .RuleFor(d => d.alcoholDegree, f => f.Random.Double(0, 100));

            var fakerTable = new Faker<Table>()
                .RuleFor(t => t.tableNumber, f => f.Random.Int(1, 50))
                .RuleFor(t => t.capacity, f => f.Random.Int(1, 10))
                .RuleFor(t => t.status, f => f.Random.ArrayElement(new[] { "Available", "Occupied", "Reserved" }));

            var fakerOrder = new Faker<Order>()
                .RuleFor(o => o.idTable, f => f.Random.Int(1, 2000))
                .RuleFor(o => o.totalPrice, f => f.Random.Double(10, 500))
                .RuleFor(o => o.datetime, f => f.Date.Recent())
                .RuleFor(o => o.status, f => f.Random.ArrayElement(new[] { "Pending", "Completed", "Cancelled" }));

            var fakerOrderItem = new Faker<OrderItem>()
                .RuleFor(oi => oi.idOrder, f => f.Random.Int(1, 2000))
                .RuleFor(oi => oi.idDrink, f => f.Random.Int(1, 2000))
                .RuleFor(oi => oi.priceUnit, f => f.Random.Double(1, 100));

            var fakerEvent = new Faker<Event>()
                .RuleFor(e => e.eventName, f => f.Lorem.Sentence())
                .RuleFor(e => e.datetime, f => f.Date.Future())
                .RuleFor(e => e.description, f => f.Lorem.Paragraph())
                .RuleFor(e => e.maxNumberParticipants, f => f.Random.Int(1, 100));

            var fakerReview = new Faker<Reviews>()
                .RuleFor(r => r.idClient, f => f.Random.Int(1, 2000))
                .RuleFor(r => r.score, f => f.Random.Int(1, 5))
                .RuleFor(r => r.textReview, f => f.Lorem.Paragraph())
                .RuleFor(r => r.dateReview, f => f.Date.Recent());

            // Генерация данных
            context.clients.AddRange(fakerClient.Generate(2000));
            context.employees.AddRange(fakerEmployee.Generate(2000));
            context.drinks.AddRange(fakerDrink.Generate(2000));
            context.tables.AddRange(fakerTable.Generate(2000));
            context.orders.AddRange(fakerOrder.Generate(2000));
            context.orderItems.AddRange(fakerOrderItem.Generate(2000));
            context.events.AddRange(fakerEvent.Generate(2000));
            context.reviews.AddRange(fakerReview.Generate(2000));

            context.SaveChanges();
        }
    }
}
