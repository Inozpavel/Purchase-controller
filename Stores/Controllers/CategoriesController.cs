using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stores.DTOs;
using Stores.Entities;
using Microsoft.AspNetCore.Http;
using Stores.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Controllers
{
    [ApiController]
    [Route("api/Stores")]
    public class CategoriesController : ControllerBase
    {
        private readonly IStoreCategoryService _service;

        public CategoriesController(IStoreCategoryService service) => _service = service;

        /// <summary>
        /// Finds all existing categories in store
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet("{storeId}/[controller]")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If there are no categories in store")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> All(int storeId)
        {
            if (!await _service.CheckStoreWithIdIsExisting(storeId))
                return NotFound();
            var categories = await _service.FindAll(storeId);
            if (!categories.Any())
                return NoContent();

            return Ok(categories);
        }

        /// <summary>
        /// Creates a new category in store
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
            if (!await _service.CheckStoreWithIdIsExisting(storeId))
                return NotFound();
            var addedCategory = await _service.Add(storeId, request);

            if (addedCategory == null)
                return BadRequest(new ProblemDetails
                {
                    Detail = "Category with given name is already existing in this store!"
                });

            return CreatedAtAction(nameof(Find), new {storeId, categoryId = addedCategory.StoreCategoryId},
                addedCategory);
        }

        /// <summary>
        /// Finds category in store by id
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> Find(int storeId, int categoryId)
        {
            if (!await _service.CheckStoreWithIdIsExisting(storeId))
                return NotFound();
            var category = await _service.FindInStoreById(storeId, categoryId);
            
            if (category == null)
                return NotFound();
            
            return Ok(category);
        }

        /// <summary>
        /// Updates category in store by id
        /// </summary>
        /// <returns></returns>
        [HttpPut("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> Update(int storeId, int categoryId, CategoryRequest request)
        {
            if (!await _service.CheckStoreWithIdIsExisting(storeId))
                return NotFound();
            var category = await _service.FindInStoreById(storeId, categoryId);

            if (category == null)
                return NotFound();

            await _service.Update(category, request);
            return Ok(category);
        }

        /// <summary>
        /// Deletes category in store by id
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{storeId}/[controller]/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is incorrect")]
        public async Task<ActionResult<StoreCategory>> Delete(int storeId, int categoryId)
        {
            if (!await _service.CheckStoreWithIdIsExisting(storeId))
                return NotFound();
            var category = await _service.FindInStoreById(storeId, categoryId);

            if (category == null)
                return NotFound();

            _service.Delete(category);
            return Ok(category);
        }
    }
}