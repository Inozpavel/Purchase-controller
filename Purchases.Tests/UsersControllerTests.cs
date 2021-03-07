using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Purchases.Controllers;
using Purchases.Data;
using Purchases.DTOs;
using Purchases.Entities;
using Purchases.Mapper;
using Purchases.Services;
using Xunit;

namespace Purchases.Tests
{
    public class UsersControllerTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public async void UserCanViewAllUsers(int preAddedUsersCount)
        {
            var users = new List<User>();
            for (int i = 0; i < preAddedUsersCount; i++)
                users.Add(Helper.GenerateRandomUser());

            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(users);

            var service = new UserService(Helper.Configuration, Helper.CreateMapperWithProfiles(new UserProfile()),
                mock.Object);
            var controller = new UsersController(service);
            var response = await controller.GetAll();

            if (preAddedUsersCount == 0)
                Assert.IsType<NoContentResult>(response.Result);
            else
            {
                Assert.NotNull(response.Result);
                Assert.IsType<OkObjectResult>(response.Result);
                Assert.Equal(users, (response.Result as OkObjectResult)?.Value);
            }
        }

        [Fact]
        public async void UserCanRegister()
        {
            var user = Helper.GenerateRandomUser();
            var mapper = Helper.CreateMapperWithProfiles(new UserProfile());

            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.AddUserAsync(user)).ReturnsAsync(user);
            mock.Setup(x => x.FindUserAsync(user.Email, UserService.HashPassword(user.Password))).ReturnsAsync(user);

            var service = new UserService(Helper.Configuration, mapper, mock.Object);
            var controller = new UsersController(service);

            var response = await controller.Register(new RegisterRequest
            {
                Email = user.Email,
                Password = user.Password
            });

            Assert.IsType<AcceptedResult>(response.Result);
        }

        [Fact]
        public async Task UserCanAuthenticate()
        {
            var user = Helper.GenerateRandomUser();
            var mapper = Helper.CreateMapperWithProfiles(new UserProfile());

            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.FindUserAsync(user.Email, UserService.HashPassword(user.Password))).ReturnsAsync(user);

            var service = new UserService(Helper.Configuration, mapper, mock.Object);
            var controller = new UsersController(service);

            var response = await controller.Authenticate(new AuthenticateRequest
            {
                Email = user.Email,
                Password = user.Password
            });

            Assert.IsType<AcceptedResult>(response.Result);
            Assert.NotNull(((AcceptedResult) response.Result).Value);
        }

        [Fact]
        public async Task UserWithExistingEmailCanNotRegister()
        {
            var user = Helper.GenerateRandomUser();
            var mapper = Helper.CreateMapperWithProfiles(new UserProfile());

            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.AddUserAsync(user)).ReturnsAsync(new User
            {
                Email = user.Email,
                Password = UserService.HashPassword(user.Password)
            });
            mock.Setup(x => x.FindUserAsync(user.Email)).ReturnsAsync(user);

            var service = new UserService(Helper.Configuration, mapper, mock.Object);
            var controller = new UsersController(service);

            var response = await controller.Register(new RegisterRequest
            {
                Email = user.Email,
                Password = user.Password
            });

            Assert.IsType<UnauthorizedObjectResult>(response.Result);
            Assert.NotNull(((UnauthorizedObjectResult) response.Result).Value);
        }

        [Fact]
        public async Task UserWithIncorrectLoginDataCanNotAuthenticate()
        {
            var user = Helper.GenerateRandomUser();
            var mapper = Helper.CreateMapperWithProfiles(new UserProfile());

            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.FindUserAsync(user.Email, UserService.HashPassword(user.Password)))
                .ReturnsAsync((User) null);

            var service = new UserService(Helper.Configuration, mapper, mock.Object);
            var controller = new UsersController(service);

            var response = await controller.Authenticate(new AuthenticateRequest
            {
                Email = user.Email,
                Password = user.Password
            });

            Assert.IsType<UnauthorizedObjectResult>(response.Result);
            Assert.NotNull(((UnauthorizedObjectResult) response.Result).Value);
        }
    }
}