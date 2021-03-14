using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Entities;

namespace Stores.Data
{
    public interface IStoreCategoryRepository
    {
        Task<IEnumerable<StoreCategory>> Find(int storeId);

        Task<StoreCategory> Add(StoreCategory category);

        Task<StoreCategory?> Find(int storeId, string categoryName);

        Task<StoreCategory?> Find(int storeId, int categoryId);

        Task SaveChangesAsync();

        void Delete(StoreCategory category);

        StoreCategory Update(StoreCategory category);

        Task<Store?> FindStore(int storeId);
    }
}