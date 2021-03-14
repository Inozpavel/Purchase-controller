using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Api.Data;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;

namespace Stores.Api.Services
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

        public async Task<Product> AddAsync(int storeId, ProductRequest request)
        {
            if (await _productRepository.Find(storeId, request.ProductName) != null)
                throw new ApiException("Category with given name is already existing in this store!");

            var product = _mapper.Map<Product>(request);
            product.StoreId = storeId;
            product.Categories = await FindCategoriesInStoreAsync(storeId, request.CategoriesIds);

            var addedProduct = await _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return addedProduct;
        }

        public async Task<Product?> FindByIdAsync(int id) => await _productRepository.Find(id);

        public async Task<IEnumerable<Product>> FindByStoreIdAsync(int storeId) =>
            await _productRepository.FindByStore(storeId);

        public async Task<Product> UpdateAsync(Product product, ProductRequest request)
        {
            product.Categories.Clear();

            var categories = await FindCategoriesInStoreAsync(product.StoreId, request.CategoriesIds);
            categories.ForEach(product.Categories.Add);

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.ProductName = request.ProductName;

            var updatedProduct = _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
            return updatedProduct;
        }

        public async Task DeleteAsync(Product product)
        {
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        private async Task<List<StoreCategory>> FindCategoriesInStoreAsync(int storeId, IEnumerable<int> categoriesIds)
        {
            List<StoreCategory> categories = new();

            foreach (int categoryId in categoriesIds)
            {
                var category = await _categoryRepository.Find(storeId, categoryId);
                if (category == null)
                    throw new ApiException("This store does not contains category with given id: " + categoryId);

                categories.Add(category);
            }

            return categories;
        }
    }
}