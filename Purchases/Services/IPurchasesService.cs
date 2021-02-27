using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Entities;

namespace Purchases.Services
{
    public interface IPurchasesService
    {
        Task<IEnumerable<Purchase>> AllForUserAsync(int userId);

        Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date);

        Task<Purchase> AddAsync(Purchase purchase);
    }
}