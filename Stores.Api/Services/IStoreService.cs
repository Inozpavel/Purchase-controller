using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Services
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