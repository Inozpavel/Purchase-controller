using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Entities;

namespace Stores.Data
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);


        Task<Product?> Find(int id);

        Task<Product?> Find(int storeId, string productName);

        Task<IEnumerable<Product>> FindByStore(int storeId);

        Product Update(Product product);

        void Delete(Product product);

        Task SaveChangesAsync();
    }
}