using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Route("api/Stores")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IStoreService _storeService;

        public ProductsController(IProductService productProductService, IStoreService storeService)
        {
            _productService = productProductService;
            _storeService = storeService;
        }

        /// <summary>
        ///     Creates a new product for store
        /// </summary>
        [HttpPost("{storeId}/products/add")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Add(int storeId, [Required] ProductRequest request)
        {
            try
            {
                if (await _storeService.FindByIdAsync(storeId) == null)
                    return NotFound();

                var addedProduct = await _productService.AddAsync(storeId, request);

                return CreatedAtAction(nameof(FindById), new {productId = addedProduct.ProductId}, addedProduct);
            }
            catch (ApiException e)
            {
                return BadRequest(new ProblemDetails {Detail = e.Message});
            }
        }

        /// <summary>
        ///     Finds all existing products in store
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{storeId}/products")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> AllProducts(int storeId)
        {
            if (await _storeService.FindByIdAsync(storeId) == null)
                return NotFound();

            var products = await _productService.FindByStoreIdAsync(storeId);
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        /// <summary>
        ///     Finds product by id
        /// </summary>
        [AllowAnonymous]
        [HttpGet("products/{productId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> FindById(int productId)
        {
            var product = await _productService.FindByIdAsync(productId);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        ///     Updates product by id
        /// </summary>
        [HttpPut("products/{productId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> UpdateById(int productId, ProductRequest request)
        {
            var product = await _productService.FindByIdAsync(productId);

            if (product == null)
                return NotFound();

            var updatedProduct = await _productService.UpdateAsync(product, request);

            return Ok(updatedProduct);
        }

        /// <summary>
        ///     Deletes product by id
        /// </summary>
        [HttpDelete("products/{productId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> DeleteById(int productId)
        {
            var product = await _productService.FindByIdAsync(productId);

            if (product == null)
                return NotFound();

            await _productService.DeleteAsync(product);

            return Ok(product);
        }
    }
}