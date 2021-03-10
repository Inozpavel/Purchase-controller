using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stores.Entities;

namespace Stores.Data
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

        public Product Update(Product product)
        {
            var updatedProduct = _context.Products.Update(product);
            return updatedProduct.Entity;
        }

        public async Task<Product?> Find(int id) => await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

        public void Delete(Product product) => _context.Products.Remove(product);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}