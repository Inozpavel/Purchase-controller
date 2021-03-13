using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Data;
using Stores.DTOs;
using Stores.Entities;

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

        public async Task<bool> CheckStoreWithIdIsExisting(int storeId) => await _repository.FindStore(storeId) != null;

        public async Task<IEnumerable<StoreCategory>> FindAll(int storeId) => await _repository.Find(storeId);

        public async Task<StoreCategory?> FindInStoreById(int storeId, int categoryId) =>
            await _repository.Find(storeId, categoryId);

        public async Task<StoreCategory?> Add(int storeId, CategoryRequest request)
        {
            if (await _repository.Find(storeId, request.CategoryName) != null)
                return null;

            var category = _mapper.Map<StoreCategory>(request);
            category.StoreId = storeId;
            var addedCategory = await _repository.Add(category);

            await _repository.SaveChanges();
            return addedCategory;
        }

        public void Delete(StoreCategory category)
        {
            _repository.Delete(category);
            _repository.SaveChanges();
        }

        public async Task<StoreCategory> Update(StoreCategory category, CategoryRequest request)
        {
            category.StoreCategoryName = request.CategoryName;
            var updatedCategory = _repository.Update(category);
            await _repository.SaveChanges();

            return updatedCategory;
        }
    }
}