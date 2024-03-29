﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Purchases.Api.DTOs;
using Purchases.Api.Services;

namespace Purchases.Api.Data
{
    public class DatabaseInitializer
    {
        private readonly ApplicationContext _context;

        private readonly IUserService _userService;

        public DatabaseInitializer(ApplicationContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public void Initialize()
        {
            _context.Database.Migrate();
            EnsureUsersAdded();
        }

        private void EnsureUsersAdded()
        {
            if (_context.Users.Any())
                return;

            List<RegisterRequest> list = new()
            {
                new RegisterRequest
                {
                    Email = "ivanov@mail.ru",
                    Password = "12345678i"
                },
                new RegisterRequest
                {
                    Email = "petrov@yandex.ru",
                    Password = "12345678p"
                },
                new RegisterRequest
                {
                    Email = "sidorov@gmail.com",
                    Password = "12345678s"
                }
            };

            foreach (var registerRequest in list)
                _userService.RegisterAsync(registerRequest).Wait();
        }
    }
}