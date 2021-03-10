using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Data;
using Stores.DTOs;
using Stores.Entities;

namespace Stores.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMapper _mapper;

        private readonly IStoreRepository _repository;

        public StoreService(IStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Store> AddAsync(StoreRequest request)
        {
            var store = _mapper.Map<Store>(request);
            //Todo: check name if existing
            
            var addedStore = await _repository.Add(store);
            await _repository.SaveChangesAsync();
            return addedStore;
        }

        public async Task<IEnumerable<Store>> AllAsync() => await _repository.All();

        public Task<Store?> FindById(int id) => _repository.Find(id);

        public async Task<Store> Update(Store store)
        {
            var updatedStore = _repository.Update(store);
            await _repository.SaveChangesAsync();
            return updatedStore;
        }

        public async Task DeleteAsync(Store store)
        {
            _repository.Delete(store);
            await _repository.SaveChangesAsync();
        }
    }
}