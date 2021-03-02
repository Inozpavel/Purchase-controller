using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Entities;

namespace Purchases.Data
{
    public interface IPurchasesRepository
    {
        Task<Purchase> AddAsync(Purchase purchase);

        void Update(Purchase purchase);

        Task<IEnumerable<Purchase>> AllForUserAsync(int userId);

        Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date);

        Task SaveChangesAsync();
    }
}