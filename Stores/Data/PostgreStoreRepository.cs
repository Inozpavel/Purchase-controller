using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stores.Entities;

namespace Stores.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationContext _context;

        public StoreRepository(ApplicationContext context) => _context = context;

        public async Task<IEnumerable<Store>> All() => await _context.Stores.ToListAsync();

        public async Task<Store> Add(Store store)
        {
            var addedStore = await _context.Stores.AddAsync(new Store
            {
                Address = store.Address,
                Phone = store.Phone,
                Products = store.Products,
                StoreName = store.StoreName
            });
            return addedStore.Entity;
        }

        public async Task<Store> Find(int id) => await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == id);

        public async Task<Store?> Find(string storeName) =>
            await _context.Stores.FirstOrDefaultAsync(x => x.StoreName == storeName);

        public Store Update(Store store)
        {
            var updatedStore = _context.Stores.Update(store);
            return updatedStore.Entity;
        }

        public void Delete(Store store) => _context.Stores.Remove(store);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}