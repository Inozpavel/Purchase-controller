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

        Task<Store?> FindById(int id);

        Task<Store> Update(Store store);

        Task DeleteAsync(Store store);
    }
}