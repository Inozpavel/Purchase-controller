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
            }.ForEach(x => AddUser(x));
        }

        public IEnumerable<User> Users => _users.Values;

        public User? FindUser(int id) => Users.FirstOrDefault(x => x.Id == id);

        public User AddUser(User user)
        {
            int key = _users.Count;
            while (_users.Keys.Contains(key))
                key++;
            user.Id = key;
            _users[user.Id] = user;

            return user;
        }
    }
}