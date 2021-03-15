using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Services
{
    public interface IPurchaseService
    {
        Task<Purchase> AddAsync(PurchaseRequest request);

        Task<IEnumerable<Purchase>> FindAllPurchasesAsync(int userId);
    }
}