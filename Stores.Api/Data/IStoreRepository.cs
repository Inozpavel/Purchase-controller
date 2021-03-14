using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public interface IStoreRepository
    {
        Task<Store> Add(Store store);

        Task<IEnumerable<Store>> All();

        Task<Store?> Find(int storeId);

        Task<Store?> Find(string? storeName, string address);

        Store Update(Store store);

        void Delete(Store store);

        Task SaveChangesAsync();
    }
}