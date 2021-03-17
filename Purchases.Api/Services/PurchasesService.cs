using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Api.Data;
using Purchases.Api.Entities;

namespace Purchases.Api.Services
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesRepository _repository;

        public PurchasesService(IPurchasesRepository repository) => _repository = repository;

        public async Task<IEnumerable<Purchase>> AllForUserAsync(int userId) =>
            await _repository.AllForUserAsync(userId);

        public async Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date) =>
            await _repository.AllForUserOnDateAsync(userId, date);
    }
}