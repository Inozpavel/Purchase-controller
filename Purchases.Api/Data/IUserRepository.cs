﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Api.Entities;

namespace Purchases.Api.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> FindUserAsync(int id);

        Task<User?> FindUserAsync(string email);

        Task<User?> FindUserAsync(string email, string password);

        Task<User> AddUserAsync(User user);

        Task SaveChangesAsync();
    }
}