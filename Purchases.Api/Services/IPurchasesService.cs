using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Api.Entities;

namespace Purchases.Api.Services
{
    public interface IPurchasesService
    {
        Task<IEnumerable<Purchase>> AllForUserAsync(int userId);

        Task<IEnumerable<Purchase>> AllForUserOnDateAsync(int userId, DateTime date);
    }
}