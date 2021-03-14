using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public class PostgreStoreCategoryRepository : IStoreCategoryRepository
    {
        private readonly ApplicationContext _context;

        public PostgreStoreCategoryRepository(ApplicationContext context) => _context = context;

        public async Task<IEnumerable<StoreCategory>> Find(int storeId) =>
            await _context.StoreCategories.Where(x => x.StoreId == storeId).ToListAsync();

        public async Task<StoreCategory> Add(StoreCategory category)
        {
            var addedCategory = await _context.StoreCategories.AddAsync(category);
            return addedCategory.Entity;
        }

        public async Task<StoreCategory?> Find(int storeId, string categoryName)
        {
            return await _context.StoreCategories.FirstOrDefaultAsync(x =>
                x.StoreId == storeId && x.StoreCategoryName == categoryName);
        }

        public async Task<StoreCategory?> Find(int storeId, int categoryId)
        {
            return await _context.StoreCategories.FirstOrDefaultAsync(x =>
                x.StoreId == storeId && x.StoreCategoryId == categoryId);
        }

        public async Task<Store?> FindStore(int storeId) =>
            await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == storeId);

        public StoreCategory Update(StoreCategory category)
        {
            var updatedCategory = _context.StoreCategories.Update(category);
            return updatedCategory.Entity;
        }

        public void Delete(StoreCategory category) => _context.StoreCategories.Remove(category);

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}