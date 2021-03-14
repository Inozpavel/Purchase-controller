using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stores.Api.Controllers;
using Stores.Api.Data;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;
using Stores.Api.Mapper;
using Stores.Api.Services;
using Xunit;

namespace Stores.Tests
{
    public class StoreControllerTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        public async void UserCanViewAllStore(int storesCount)
        {
            var stores = CreateRandomStoresList(storesCount);
            var repository = new Mock<IStoreRepository>();
            repository.Setup(x => x.All()).ReturnsAsync(stores);

            var service = new StoreService(repository.Object, Helper.CreateMapperWithProfile<StoreProfile>());
            var controller = new StoresController(service);

            var response = await controller.All();

            if (storesCount > 0)
            {
                Assert.IsType<OkObjectResult>(response.Result);
                Assert.Equal(stores, ((OkObjectResult) response.Result).Value);
            }
            else
            {
                Assert.IsType<NoContentResult>(response.Result);
            }
        }

        [Fact]
        public async void UserCanAddStore()
        {
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var request = new Fixture().Create<StoreRequest>();
            var mock = new Mock<IStoreService>();
            mock.Setup(x => x.AddAsync(request)).ReturnsAsync(mapper.Map<Store>(request));

            var controller = new StoresController(mock.Object);

            var response = await controller.Add(request);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async void AddingExistingStoreWillReturnBadRequest()
        {
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var request = new Fixture().Create<StoreRequest>();
            var mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Find(request.StoreName, request.Address)).ReturnsAsync(mapper.Map<Store>(request));

            var service = new StoreService(mock.Object, mapper);
            var controller = new StoresController(service);

            var response = await controller.Add(request);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async void UserCanGetStoreById(int storesCount)
        {
            var stores = CreateRandomStoresList(storesCount);
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var store = stores.FirstOrDefault();
            var mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Find(store.StoreId))
                .ReturnsAsync(stores.FirstOrDefault(x => x.StoreId == store?.StoreId));

            var service = new StoreService(mock.Object, mapper);
            var controller = new StoresController(service);

            var response = await controller.GetById(store?.StoreId ?? -1);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async void GetStoreWithIncorrectIdWillReturnNotFound()
        {
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var store = CreateRandomStore();
            var mock = new Mock<IStoreRepository>();

            var service = new StoreService(mock.Object, mapper);
            var controller = new StoresController(service);

            var response = await controller.GetById(store.StoreId);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void UserCanUpdateStoreById()
        {
            var store = CreateRandomStore();
            var request = new Fixture().Create<StoreRequest>();
            var mock = new Mock<IStoreService>();
            mock.Setup(x => x.UpdateAsync(store, request)).ReturnsAsync(store);
            mock.Setup(x => x.FindByIdAsync(store.StoreId)).ReturnsAsync(store);

            var controller = new StoresController(mock.Object);

            var response = await controller.UpdateById(store.StoreId, request);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async void UpdateStoreWithIncorrectIdWillReturnNotFound()
        {
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var store = CreateRandomStore();
            var mock = new Mock<IStoreRepository>();

            var service = new StoreService(mock.Object, mapper);
            var controller = new StoresController(service);

            var request = new Fixture().Create<StoreRequest>();
            var response = await controller.UpdateById(store.StoreId, request);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void UpdateStoreWithExistingNameWillReturnBadRequest()
        {
            var store = CreateRandomStore();
            var request = new Fixture().Create<StoreRequest>();
            var mock = new Mock<IStoreService>();
            mock.Setup(x => x.UpdateAsync(store, request)).Throws<ApiException>();

            var controller = new StoresController(mock.Object);
            var response = await controller.UpdateById(store.StoreId, request);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void UserCanDeleteStoreById()
        {
            var store = CreateRandomStore();
            var mock = new Mock<IStoreService>();
            mock.Setup(x => x.FindByIdAsync(store.StoreId)).ReturnsAsync(store);

            var controller = new StoresController(mock.Object);

            var response = await controller.DeleteById(store.StoreId);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async void DeleteStoreWithIncorrectIdWillReturnNotFound()
        {
            var mapper = Helper.CreateMapperWithProfile<StoreProfile>();
            var store = CreateRandomStore();
            var mock = new Mock<IStoreRepository>();

            var service = new StoreService(mock.Object, mapper);
            var controller = new StoresController(service);

            var response = await controller.DeleteById(store.StoreId);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        private static Store CreateRandomStore() =>
            new Fixture().Build<Store>().Without(x => x.Products).Without(x => x.Categories).Create();

        private static List<Store> CreateRandomStoresList(int storesCount)
        {
            var list = Enumerable
                .Range(0, storesCount)
                .Select(_ => CreateRandomStore())
                .ToList();

            return list;
        }
    }
}