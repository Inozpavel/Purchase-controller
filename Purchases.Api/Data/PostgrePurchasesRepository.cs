﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Purchases.Api.Entities;

namespace Purchases.Api.Data
{
    public class PostgrePurchasesRepository : IPurchasesRepository
    {
        private readonly ApplicationContext _context;

        public PostgrePurchasesRepository(ApplicationContext context) => _context = context;

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            var addedPurchase = await _context.Purchases.AddAsync(new Purchase
            {
                Name = purchase.Name,
                UserId = purchase.UserId,
                Price = purchase.Price,
                Date = purchase.Date
            });
            return addedPurchase.Entity;
        }

        public void Update(Purchase purchase) => _context.Purchases.Update(purchase);

        public async Task<IEnumerable<Purchase>> AllForUserAsync(int userId) =>
            await _context.Purchases.Where(x => x.UserId == userId).ToListAsync();

        public async Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date) =>
            await _context.Purchases.Where(x => x.UserId == userId && x.Date == date).ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}