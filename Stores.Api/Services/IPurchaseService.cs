using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Services
{
    public interface IPurchaseService
    {
        Task<Purchase> AddAsync(int userId, PurchaseRequest request);

        Task<IEnumerable<Purchase>> FindAllPurchasesAsync(int userId);

        Task<IEnumerable<PaymentMethod>> FindAllPaymentMethods();

        Task<IEnumerable<Purchase>> FindAllPurchasesForStoreAsync(int storeId);
    }
}