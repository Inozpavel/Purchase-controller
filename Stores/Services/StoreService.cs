using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Data;
using Stores.DTOs;
using Stores.Entities;
using Stores.Exceptions;

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
            if (await _repository.Find(request.StoreName, request.Address) != null)
                throw new ApiException("Store at given address with given name is already existing!");
            var store = _mapper.Map<Store>(request);

            var addedStore = await _repository.Add(store);
            await _repository.SaveChangesAsync();
            return addedStore;
        }

        public async Task<IEnumerable<Store>> AllAsync() => await _repository.All();

        public Task<Store?> FindByIdAsync(int id) => _repository.Find(id);

        public async Task<Store> UpdateAsync(Store store, StoreRequest request)
        {
            if (await _repository.Find(request.StoreName, request.Address) != null)
                throw new ApiException("Store at given address with given name is already existing!");

            store.StoreName = request.StoreName;
            store.Address = request.Address;
            store.Phone = request.Phone;

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