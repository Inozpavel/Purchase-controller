using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Entities;

namespace Stores.Data
{
    public interface IStoreRepository
    {
        Task<Store> Add(Store store);

        Task<IEnumerable<Store>> All();

        Task<Store> Find(int id);

        Task<Store> Find(string name);

        Store Update(Store store);

        void Delete(Store store);

        Task SaveChangesAsync();
    }
}