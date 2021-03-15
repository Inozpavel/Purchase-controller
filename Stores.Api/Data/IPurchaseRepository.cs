using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public interface IPurchaseRepository
    {
        Task<Purchase> AddAsync(Purchase purchase);

        Task<PaymentMethod?> FindPaymentMethodByIdAsync(int id);

        Task<CustomCategory?> FindCustomCategoryASync(int userId, string name);

        Task SaveChangesAsync();

        Task<IEnumerable<Purchase>> FindAllAsync(int userId);

        Task<IEnumerable<PaymentMethod>> FindAllPaymentMethods();
    }
}