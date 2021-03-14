using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Services
{
    public interface IStoreCategoryService
    {
        Task<StoreCategory> AddAsync(int storeId, CategoryRequest request);

        Task<IEnumerable<StoreCategory>> FindAllAsync(int storeId);

        Task<StoreCategory?> FindInStoreByIdAsync(int storeId, int categoryId);

        Task<StoreCategory> UpdateAsync(StoreCategory category, CategoryRequest request);

        Task DeleteAsync(StoreCategory category);

        Task<bool> CheckStoreWithIdIsExistingAsync(int storeId);
    }
}