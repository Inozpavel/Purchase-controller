using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;
using Stores.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchasesController(IPurchaseService service) => _service = service;

        [HttpPost("add")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Purchase>> Add(PurchaseRequest request)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "-1");
                var purchase = await _service.AddAsync(userId, request);
                return Ok(purchase);
            }
            catch (ApiException e)
            {
                return BadRequest(new ProblemDetails {Detail = e.Message});
            }
        }

        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Purchase>> All()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "-1");
            var purchases = await _service.FindAllPurchasesAsync(userId);
            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

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
    }
}