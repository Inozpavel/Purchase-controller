using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Data;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Services
{
    public class ProductService : IProductService
    {
        private readonly IStoreCategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IStoreCategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Product?> Add(int storeId, ProductRequest request)
        {
            if (await _productRepository.Find(storeId, request.ProductName) != null)
                return null;

            var product = _mapper.Map<Product>(request);
            product.StoreId = storeId;

            List<StoreCategory> categories = new();

            foreach (int categoryId in request.CategoriesIds)
            {
                var category = await _categoryRepository.Find(storeId, categoryId);
                if (category == null)
                    return null;

                categories.Add(category);
            }

            product.Categories = categories;

            var addedProduct = await _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return addedProduct;
        }

        public async Task<Product?> Find(int id) => await _productRepository.Find(id);

        public void Delete(Product product) => _productRepository.Delete(product);

        public async Task<IEnumerable<Product>> FindByStore(int storeId) =>
            await _productRepository.FindByStore(storeId);

        public async Task<Product> Update(Product product, ProductRequest request)
        {
            product.Categories.Clear();
            var categories = new List<StoreCategory>();

            foreach (int categoryId in request.CategoriesIds)
            {
                var category = await _categoryRepository.Find(product.StoreId, categoryId);
                if (category == null)
                    return null;
                categories.Add(category);
            }

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.ProductName = request.ProductName;
            categories.ForEach(product.Categories.Add);

            var updatedProduct = _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
            return updatedProduct;
        }
    }
}