using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Purchases.Api.Controllers;
using Purchases.Api.Data;
using Purchases.Api.Entities;
using Purchases.Api.Services;
using Xunit;

namespace Purchases.Tests
{
    public class PurchasesControllerTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(1)]
        [InlineData(0)]
        public async Task UserCanViewAllPurchases(int purchasesToAdd)
        {
            var user = Helper.GenerateRandomUser();


            var purchases = new List<Purchase>();
            for (int i = 0; i < purchasesToAdd; i++)
                purchases.Add(new Purchase {UserId = user.UserId});

            Mock<IPurchasesRepository> mock = new();
            PurchasesService service = new(mock.Object);
            mock.Setup(x => x.AllForUserAsync(user.UserId)).ReturnsAsync(purchases);

            var controller = new PurchasesController(service)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim("id", user.UserId.ToString())
                        }))
                    }
                }
            };

            var response = await controller.GetAll();
            if (purchasesToAdd > 0)
            {
                Assert.IsType<OkObjectResult>(response.Result);
                var actual = (response.Result as OkObjectResult)?.Value as List<Purchase>;
                Assert.Equal(purchases.Count, actual?.Count);
            }
            else
                Assert.IsType<NoContentResult>(response.Result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(1)]
        [InlineData(0)]
        public async Task UserCanViewAllPurchasesOnDate(int purchasesToAdd)
        {
            var user = Helper.GenerateRandomUser();
            var date = DateTime.Now;


            var purchases = new List<Purchase>();
            for (int i = 0; i < purchasesToAdd; i++)
                purchases.Add(new Purchase {UserId = user.UserId, Date = date});

            Mock<IPurchasesRepository> mock = new();
            PurchasesService service = new(mock.Object);
            mock.Setup(x => x.AllForUserOnDateAsync(user.UserId, date))
                .ReturnsAsync(purchases.Where(x => x.Date == date).ToList());

            var controller = new PurchasesController(service)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim("id", user.UserId.ToString())
                        }))
                    }
                }
            };

            var response = await controller.GetAllOnDate(date);
            if (purchasesToAdd > 0)
            {
                Assert.IsType<OkObjectResult>(response.Result);
                var actual = (response.Result as OkObjectResult)?.Value as List<Purchase>;
                Assert.Equal(purchases.Count, actual?.Count);
            }
            else
                Assert.IsType<NoContentResult>(response.Result);
        }


        [Fact]
        public async Task CanAddPurchase()
        {
            var user = Helper.GenerateRandomUser();

            var purchase = new Purchase
            {
                Cost = 1200,
                Date = DateTime.Now,
                Name = "purchase",
                UserId = user.UserId
            };

            Mock<IPurchasesRepository> mock = new();
            PurchasesService service = new(mock.Object);
            mock.Setup(x => x.AddAsync(purchase)).ReturnsAsync(purchase);

            var controller = new PurchasesController(service)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim("id", user.UserId.ToString())
                        }))
                    }
                }
            };

            var response = await controller.Add(purchase);
            Assert.IsType<OkObjectResult>(response);
        }
    }
}