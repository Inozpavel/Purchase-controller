using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchases.Entities;
using Purchases.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Purchases.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Everything about purchases")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "If user if unauthorized")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService _purchasesService;

        public PurchasesController(IPurchasesService purchasesService) => _purchasesService = purchasesService;

        /// <summary>
        ///     Get all purchases for user
        /// </summary>
        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Purchase>>> AllPurchasesAsync()
        {
            var purchases = await _purchasesService.AllForUserAsync(GetUserId());

            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

        /// <summary>
        ///     Get all purchases on date for user
        /// </summary>
        [HttpGet("all/{date}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Purchase>>> AllPurchasesOnDateAsync([Required] DateTime date)
        {
            var purchases = await _purchasesService.AllForUserOnDateAsync(GetUserId(), date);

            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

        /// <summary>
        ///     Add purchase for user
        /// </summary>
        /// <param name="purchase"></param>
        [HttpPost("add")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult> AddAsync([Required] Purchase purchase)
        {
            purchase.UserId = GetUserId();

            var addedPurchase = await _purchasesService.AddAsync(purchase);
            return Ok(addedPurchase);
        }

        private int GetUserId()
        {
            string id = ((ClaimsIdentity) User.Identity)?.FindFirst(x => x.Type == "id")?.Value ??
                        throw new Exception("Cant get user id from identity");

            return int.Parse(id);
        }
    }
}