using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Purchases.Entities;

namespace Purchases.Data
{
    public class PostgreUsersRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public PostgreUsersRepository(ApplicationContext context) => _context = context;

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> FindUserAsync(int id) => await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

        public async Task<User?> FindUserAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> FindUserAsync(string email, string password) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

        public async Task<User> AddUserAsync(User user)
        {
            var addedUser = await _context.Users.AddAsync(new User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic
            });
            return addedUser.Entity;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}