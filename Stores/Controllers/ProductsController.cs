using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stores.DTOs;
using Stores.Entities;
using Stores.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Controllers
{
    [ApiController]
    [Route("api/Stores")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IStoreService _storeService;

        public ProductsController(IProductService productProductService, IStoreService storeService, IMapper mapper)
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
            if (await _storeService.FindById(storeId) == null)
                return NotFound();

            var addedProduct = await _productService.Add(storeId, request);

            if (addedProduct == null)
                return BadRequest(new ProblemDetails
                    {Detail = "Product with given name is already existing in this store!"});

            return CreatedAtAction(nameof(FindById), new {productId = addedProduct.ProductId}, addedProduct);
        }

        /// <summary>
        ///     Finds all existing products in store
        /// </summary>
        [HttpGet("{storeId}/products")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> AllProducts(int storeId)
        {
            if (await _storeService.FindById(storeId) == null)
                return NotFound();

            var products = await _productService.FindByStore(storeId);
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        /// <summary>
        ///     Finds product by id
        /// </summary>
        [HttpGet("products/{productId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> FindById(int productId)
        {
            var product = await _productService.Find(productId);

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
            var product = await _productService.Find(productId);

            if (product == null)
                return NotFound();

            var updatedProduct = await _productService.Update(product, request);

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
            var product = await _productService.Find(productId);

            if (product == null)
                return NotFound();

            _productService.Delete(product);

            return Ok(product);
        }
    }
}