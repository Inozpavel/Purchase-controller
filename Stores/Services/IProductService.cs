using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Services
{
    public interface IProductService
    {
        Task<Product?> Add(int storeId, ProductRequest request);

        Task<Product?> Find(int id);

        void Delete(Product product);

        Task<IEnumerable<Product>> FindByStore(int storeId);

        Task<Product> Update(Product product, ProductRequest request);
    }
}