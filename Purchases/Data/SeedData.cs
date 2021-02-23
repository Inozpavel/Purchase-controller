using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Purchases.Entities;
using Purchases.Models;
using Purchases.Services;

namespace Purchases.Data
{
    public static class SeedData
    {
        public static void EnsureUsersAdded(IApplicationBuilder builder)
        {
            var context = builder.ApplicationServices.GetRequiredService<ApplicationContext>();
            var service = builder.ApplicationServices.GetRequiredService<IUserService>();
            if (context.Users.Any())
                return;

            new List<RegisterRequest>
            {
                new()
                {
                    Email = "ivanov@mail.ru",
                    Password = "12345i"
                },
                new()
                {
                    Email = "petrov@yandex.ru",
                    Password = "12345p"
                },
                new()
                {
                    Email = "sidorov@gmail.com",
                    Password = "12345s"
                }
            }.ForEach(x => service.RegisterAsync(x));
        }
    }
}