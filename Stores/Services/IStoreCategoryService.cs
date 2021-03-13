using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Services
{
    public interface IStoreCategoryService
    {
        Task<bool> CheckStoreWithIdIsExisting(int storeId);

        Task<IEnumerable<StoreCategory>> FindAll(int storeId);

        Task<StoreCategory?> FindInStoreById(int storeId, int categoryId);

        Task<StoreCategory?> Add(int storeId, CategoryRequest request);

        void Delete(StoreCategory category);

        Task<StoreCategory> Update(StoreCategory category, CategoryRequest request);
    }
}