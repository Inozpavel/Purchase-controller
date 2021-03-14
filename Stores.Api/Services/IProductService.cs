using System.Collections.Generic;
using System.Threading.Tasks;
using Stores.Api.DTOs;
using Stores.Api.Entities;

namespace Stores.Api.Services
{
    public interface IProductService
    {
        Task<Product> AddAsync(int storeId, ProductRequest request);

        Task<Product?> FindByIdAsync(int id);

        Task<IEnumerable<Product>> FindByStoreIdAsync(int storeId);

        Task<Product> UpdateAsync(Product product, ProductRequest request);

        Task DeleteAsync(Product product);
    }
}