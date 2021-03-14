using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Services
{
    public interface IStoreService
    {
        Task<Store> AddAsync(StoreRequest store);

        Task<IEnumerable<Store>> AllAsync();

        Task<Store?> FindByIdAsync(int id);

        Task<Store> UpdateAsync(Store store, StoreRequest request);

        Task DeleteAsync(Store store);
    }
}