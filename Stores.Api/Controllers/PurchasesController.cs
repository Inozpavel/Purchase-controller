using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;
using Stores.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Api.Controllers
{
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
                var purchase = await _service.AddAsync(request);
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
        public async Task<ActionResult<Purchase>> Add()
        {
            int userId = 0;
            var purchases = await _service.FindAllPurchasesAsync(userId);
            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }
    }
}