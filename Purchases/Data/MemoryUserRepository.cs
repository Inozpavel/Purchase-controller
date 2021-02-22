using System.Collections.Generic;
using System.Linq;
using Purchases.Entities;

namespace Purchases.Data
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<int, User> _users;

        public MemoryUserRepository()
        {
            _users = new Dictionary<int, User>();
            new List<User>
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
            }.ForEach(AddUser);
        }

        public IEnumerable<User> Users => _users.Values;

        public User? FindUser(int id) => _users[id];

        public void AddUser(User user)
        {
            int key = _users.Count;
            while (_users.Keys.Contains(key))
                key++;

            _users[key] = user;
        }
    }
}