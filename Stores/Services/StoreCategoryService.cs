using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Data;
using Stores.DTOs;
using Stores.Entities;
using Stores.Exceptions;

namespace Stores.Services
{
    public class StoreCategoryService : IStoreCategoryService
    {
        private readonly IMapper _mapper;

        private readonly IStoreCategoryRepository _repository;

        public StoreCategoryService(IStoreCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StoreCategory> AddAsync(int storeId, CategoryRequest request)
        {
            if (await _repository.Find(storeId, request.CategoryName) != null)
                throw new ApiException("Category with given name is already added in this store");

            var category = _mapper.Map<StoreCategory>(request);
            category.StoreId = storeId;
            var addedCategory = await _repository.Add(category);

            await _repository.SaveChangesAsync();
            return addedCategory;
        }

        public async Task<IEnumerable<StoreCategory>> FindAllAsync(int storeId) => await _repository.Find(storeId);

        public async Task<StoreCategory?> FindInStoreByIdAsync(int storeId, int categoryId) =>
            await _repository.Find(storeId, categoryId);

        public async Task<StoreCategory> UpdateAsync(StoreCategory category, CategoryRequest request)
        {
            category.StoreCategoryName = request.CategoryName;
            var updatedCategory = _repository.Update(category);
            await _repository.SaveChangesAsync();

            return updatedCategory;
        }

        public async Task DeleteAsync(StoreCategory category)
        {
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> CheckStoreWithIdIsExistingAsync(int storeId) =>
            await _repository.FindStore(storeId) != null;
    }
}