using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchases.Api.Entities;
using Purchases.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Purchases.Api.Controllers
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
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If the are no purchases")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAll()
        {
            var purchases = await _purchasesService.AllForUserAsync(GetUserId());

            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

        /// <summary>
        ///     Get all purchases on date for user
        /// </summary>
        /// <param name="date" example="2021-02-27T06:20:30">Time when the purchases was made</param>
        [HttpGet("{date}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If the are no purchases")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAllOnDate([Required] DateTime date)
        {
            var purchases = await _purchasesService.AllForUserOnDateAsync(GetUserId(), date);
            if (!purchases.Any())
                return NoContent();
            return Ok(purchases);
        }

        private int GetUserId()
        {
            string id = ((ClaimsIdentity) User.Identity)?.FindFirst(x => x.Type == "id")?.Value ??
                        throw new Exception("Cant get user id from identity");

            return int.Parse(id);
        }
    }
}