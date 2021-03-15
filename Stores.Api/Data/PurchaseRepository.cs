using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationContext _context;

        public PurchaseRepository(ApplicationContext context) => _context = context;

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            var addedPurchase = await _context.Purchases.AddAsync(purchase);
            return addedPurchase.Entity;
        }

        public async Task<PaymentMethod?> FindPaymentMethodByIdAsync(int id) =>
            await _context.PaymentMethods.FirstOrDefaultAsync(x => x.PaymentMethodId == id);

        public async Task<CustomCategory?> FindCustomCategoryASync(int userId, string name)
        {
            return await _context.CustomCategories.FirstOrDefaultAsync(x =>
                x.UserId == userId && x.CustomCategoryName == name);
        }

        public async Task<IEnumerable<Purchase>> FindAllAsync(int userId)
        {
            return await _context.Purchases.Where(x => x.UserId == userId)
                .Include("ReceiptPositions.CustomCategories.CustomCategory")
                .Include("ReceiptPositions.Product.Categories")
                .ToListAsync();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}