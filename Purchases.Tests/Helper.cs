using System;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Purchases.Api.Entities;

namespace Purchases.Tests
{
    public static class Helper
    {
        public static readonly IConfigurationRoot Configuration;

        static Helper()
        {
            string configPath = Assembly.Load("Purchases.Api").Location
                .Replace("Purchases.Api.dll", "appsettings.json");

            Configuration = new ConfigurationBuilder().AddJsonFile(configPath).Build();
        }

        public static Mapper CreateMapperWithProfiles<T>(params T[] profiles) where T : Profile
        {
            return new(new MapperConfiguration(options =>
            {
                foreach (var profile in profiles)
                    options.AddProfile(profile);
            }));
        }

        public static User GenerateRandomUser()
        {
            var random = new Random();
            byte[] passwordBytes =
                Enumerable.Range(0, random.Next(8, 15)).Select(_ => (byte) random.Next(33, 126)).ToArray();

            byte[] emailBytes = Enumerable.Range(0, random.Next(5, 10))
                .Select(_ => (byte) random.Next(97, 122)).ToArray();

            var user = new User
            {
                Password = Encoding.UTF8.GetString(passwordBytes),
                Email = Encoding.UTF8.GetString(emailBytes) + "@mail.ru"
            };

            return user;
        }
    }
}