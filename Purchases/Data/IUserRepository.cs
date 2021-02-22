﻿using System.Collections.Generic;
using Purchases.Entities;

namespace Purchases.Data
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        User? FindUser(int id);

        User AddUser(User user);
    }
}