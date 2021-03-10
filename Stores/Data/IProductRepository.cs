using System.Threading.Tasks;
using Stores.Entities;

namespace Stores.Data
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);


        Task<Product?> Find(int id);

        Product Update(Product product);

        void Delete(Product product);

        Task SaveChangesAsync();
    }
}