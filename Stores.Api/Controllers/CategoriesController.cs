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
    [Route("api/Stores.Api")]
    public class CategoriesController : ControllerBase
    {
        private readonly IStoreCategoryService _service;

        public CategoriesController(IStoreCategoryService service) => _service = service;

        /// <summary>
        ///     Creates a new category in store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{storeId}/[controller]/add")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If category with given name is alreay added in store",
            typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> Add(int storeId, CategoryRequest request)
        {
            try
            {
                if (!await _service.CheckStoreWithIdIsExistingAsync(storeId))
                    return NotFound();
                var addedCategory = await _service.AddAsync(storeId, request);
                return CreatedAtAction(nameof(FindById), new {storeId, categoryId = addedCategory.StoreCategoryId},
                    addedCategory);
            }
            catch (ApiException e)
            {
                return BadRequest(new ProblemDetails {Detail = e.Message});
            }
        }

        /// <summary>
        ///     Finds all existing categories in store
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet("{storeId}/[controller]")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If there are no categories in store")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> All(int storeId)
        {
            if (!await _service.CheckStoreWithIdIsExistingAsync(storeId))
                return NotFound();
            var categories = await _service.FindAllAsync(storeId);
            if (!categories.Any())
                return NoContent();

            return Ok(categories);
        }

        /// <summary>
        ///     Finds category in store by id
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> FindById(int storeId, int categoryId)
        {
            if (!await _service.CheckStoreWithIdIsExistingAsync(storeId))
                return NotFound();
            var category = await _service.FindInStoreByIdAsync(storeId, categoryId);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        ///     Updates category in store by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If category id is not found", typeof(ProblemDetails))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StoreCategory>> UpdateById(int storeId, int categoryId, CategoryRequest request)
        {
            try
            {
                if (!await _service.CheckStoreWithIdIsExistingAsync(storeId))
                    return NotFound();
                var category = await _service.FindInStoreByIdAsync(storeId, categoryId);

                if (category == null)
                    return NotFound();

                return Ok(await _service.UpdateAsync(category, request));
            }
            catch (ApiException e)
            {
                return BadRequest(new ProblemDetails {Detail = e.Message});
            }
        }

        /// <summary>
        ///     Deletes category in store by id
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> DeleteById(int storeId, int categoryId)
        {
            if (!await _service.CheckStoreWithIdIsExistingAsync(storeId))
                return NotFound();
            var category = await _service.FindInStoreByIdAsync(storeId, categoryId);

            if (category == null)
                return NotFound();

            await _service.DeleteAsync(category);
            return Ok(category);
        }
    }
}