using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context) => _context = context;

        public async Task<Product> Add(Product product)
        {
            var addedProduct = await _context.Products.AddAsync(product);
            return addedProduct.Entity;
        }

        public async Task<Product?> Find(int id) => await _context.Products.Include(x => x.Categories)
            .FirstOrDefaultAsync(x => x.ProductId == id);

        public async Task<Product?> Find(int storeId, string productName)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.StoreId == storeId && x.ProductName == productName);
        }

        public async Task<IEnumerable<Product>> FindByStore(int storeId) =>
            await _context.Products.Where(x => x.StoreId == storeId).ToListAsync();

        public Product Update(Product product)
        {
            var updatedProduct = _context.Products.Update(product);
            return updatedProduct.Entity;
        }

        public void Delete(Product product) => _context.Products.Remove(product);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}