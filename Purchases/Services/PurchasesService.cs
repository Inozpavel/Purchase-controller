using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Data;
using Purchases.Entities;

namespace Purchases.Services
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesRepository _repository;

        public PurchasesService(IPurchasesRepository repository) => _repository = repository;

        public async Task<IEnumerable<Purchase>> AllForUserAsync(int userId) =>
            await _repository.AllForUserAsync(userId);

        public async Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date) =>
            await _repository.AllForUserOnDateAsync(userId, date);

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            var addedPurchase = await _repository.AddAsync(purchase);
            await _repository.SaveChangesAsync();
            return addedPurchase;
        }
    }
}