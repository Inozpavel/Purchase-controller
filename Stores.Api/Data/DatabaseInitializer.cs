using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stores.Api.Entities;
using Stores.Api.Enums;

namespace Stores.Api.Data
{
    public class DatabaseInitializer
    {
        private readonly ApplicationContext _context;

        public DatabaseInitializer(ApplicationContext context) => _context = context;

        public void Initialize()
        {
            _context.Database.Migrate();

            EnsurePaymentMethodsAdded();
            EnsureStoresAdded();
        }

        private void EnsurePaymentMethodsAdded()
        {
            string[] methods = Enum.GetNames(typeof(PaymentMethods));
            if (_context.PaymentMethods.Any())
                return;
            _context.PaymentMethods.AddRange(methods.Select(name => new PaymentMethod(name)));
            _context.SaveChanges();
        }

        private void EnsureStoresAdded()
        {
            if (_context.Stores.Any())
                return;

            // Everything is random generated

            var stores = new List<Store>
            {
                new()
                {
                    StoreName = "Пятерочка",
                    Address = "Кемеровская область, город Балашиха, наб. Гоголя, 32",
                    Phone = "7(499)693-27-44",
                    Categories = new List<StoreCategory>
                    {
                        new("Мясо"),
                        new("Овощи"),
                        new("Хлебобулочные изделия"),
                        new("Кондитерские изделия"),
                    },
                    Products = new List<Product>
                    {
                        new("Вода питьевая", 30, 100),
                        new("Масло сливочное", 70, 50),
                        new("Молоко", 100, 20),
                    }
                },
                new()
                {
                    StoreName = "IKEA",
                    Address = "Ленинградская область, город Воскресенск, проезд Сталина, 23",
                    Phone = "7(499)295-89-78",
                    Categories = new List<StoreCategory>
                    {
                        new("Кухни"),
                        new("Кровати"),
                        new("Столы"),
                        new("Бытовая техника"),
                    }
                },
                new()
                {
                    StoreName = "Детский мир",
                    Address = "Брянская область, город Орехово-Зуево, наб. Гагарина, 20",
                    Phone = "7(499)506-70-59",
                    Categories = new List<StoreCategory>
                    {
                        new("Одежда"),
                        new("Игрушки"),
                        new("Детское питание"),
                        new("Обувь"),
                    }
                },
                new()
                {
                    StoreName = "Перекресток",
                    Address = "Томская область, город Павловский Посад, пл. Славы, 13",
                    Phone = "7(499)455-16-11",
                    Categories = new List<StoreCategory>
                    {
                        new("Мясные изделия"),
                        new("Фрукты и овощи"),
                        new("Хлеб"),
                        new("Напитки"),
                    }
                },
                new()
                {
                    StoreName = "Дикси",
                    Address = "Челябинская область, город Раменское, спуск Косиора, 42",
                    Phone = "7(499)539-27-36",
                }
            };

            stores.ForEach(x => _context.Stores.Add(x));
            _context.SaveChanges();
        }
    }
}