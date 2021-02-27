using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Purchases.Entities;

namespace Purchases.Data
{
    public class PostgrePurchasesRepository : IPurchasesRepository
    {
        private readonly ApplicationContext _context;

        public PostgrePurchasesRepository(ApplicationContext context) => _context = context;

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            var addedPurchase = await _context.Purchases.AddAsync(new Purchase()
            {
                Name = purchase.Name,
                UserId = purchase.UserId,
                Cost = purchase.Cost,
                Date = purchase.Date
            });
            return addedPurchase.Entity;
        }

        public void Update(Purchase purchase) => _context.Purchases.Update(purchase);

        public async Task<IEnumerable<Purchase>> AllForUserAsync(int userId) =>
            await _context.Purchases.Where(x => x.UserId == userId).ToListAsync();

        public async Task<IEnumerable<Purchase>> AllForUserOnDataAsync(int userId, DateTime date) =>
            await _context.Purchases.Where(x => x.UserId == userId && x.Date == date).ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}