using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchases.Contracts;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;
using Stores.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Api.Controllers
{
    /// <summary>
    ///     Adds receipt information for user
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    [SwaggerTag("Operations about receipts and payment methods")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint;

        private readonly IPurchaseService _service;

        public PurchasesController(IPublishEndpoint endpoint, IPurchaseService service)
        {
            _endpoint = endpoint;
            _service = service;
        }

        /// <summary>
        ///     Creates new receipt for user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "If user if unauthorized")]
        public async Task<ActionResult<Purchase>> Add(PurchaseRequest request)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "-1");
                var purchase = await _service.AddAsync(userId, request);

                var storePurchase = new StorePurchase
                {
                    Date = purchase.TimeOfPurchase,
                    UserId = userId,
                    PurchaseProducts = purchase.ReceiptPositions.Select(x => new PurchaseProduct
                    {
                        Count = x.Count,
                        Name = x.Product.ProductName,
                        Price = x.Product.Price
                    }).ToList()
                };
                await _endpoint.Publish(storePurchase);
                return Ok(purchase);
            }
            catch (ApiException e)
            {
                return BadRequest(new ProblemDetails {Detail = e.Message});
            }
        }

        /// <summary>
        ///     Finds all receipts for user
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "If user if unauthorized")]
        public async Task<ActionResult<Purchase>> All()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "-1");
            var purchases = await _service.FindAllPurchasesAsync(userId);
            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

        /// <summary>
        ///     Finds all payment methods
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("paymentMethods")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAllPaymentMethods()
        {
            var paymentMethods = await _service.FindAllPaymentMethods();
            if (!paymentMethods.Any())
                return NoContent();

            return Ok(paymentMethods);
        }

        /// <summary>
        ///     Finds all purchases for store
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{storeId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAllPurchasesForStore(int storeId)
        {
            var paymentMethods = await _service.FindAllPurchasesForStoreAsync(storeId);
            if (!paymentMethods.Any())
                return NoContent();

            return Ok(paymentMethods);
        }
    }
}