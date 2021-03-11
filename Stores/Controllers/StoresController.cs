using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stores.DTOs;
using Stores.Entities;
using Stores.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService) => _storeService = storeService;

        /// <summary>
        ///     Finds all existing stores
        /// </summary>
        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<Store>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If there are no stores")]
        public async Task<ActionResult<IEnumerable<Store>>> All()
        {
            var stores = await _storeService.AllAsync();

            if (!stores.Any())
                return NoContent();

            return Ok(stores);
        }

        /// <summary>
        ///     Creates new store
        /// </summary>
        [HttpPost("add")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Store))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If store is already added")]
        public async Task<ActionResult<Store>> Add([Required] StoreRequest request)
        {
            var addedStore = await _storeService.AddAsync(request);
            if (addedStore == null)
                return BadRequest(new ProblemDetails {Detail = "Store with this name is already added!"});
            return CreatedAtAction(nameof(GetById), new {id = addedStore.StoreId}, addedStore);
        }

        /// <summary>
        ///     Finds store by id
        /// </summary>
        /// <param name="id">Id of the store</param>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Store))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is wrong")]
        public async Task<ActionResult<Store>> GetById(int id)
        {
            var store = await _storeService.FindById(id);

            if (store == null)
                return NotFound();

            return Ok(store);
        }

        /// <summary>
        ///     Updates store by id
        /// </summary>
        /// <param name="id">Id of the store</param>
        /// <param name="request">New store information</param>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Store))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is wrong")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "If store with given name is already added",
            typeof(ProblemDetails))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Store>> UpdateById(int id, StoreRequest request)
        {
            var store = await _storeService.FindById(id);
            if (store == null)
                return NotFound();

            if (await _storeService.FindByName(request.StoreName) != null)
                return BadRequest(new ProblemDetails {Detail = "Store with this name is already added!"});

            var updatedStore = await _storeService.Update(store, request);

            return Ok(updatedStore);
        }

        /// <summary>
        ///     Deletes store by id
        /// </summary>
        /// <param name="id">Id of the store</param>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Store))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If id is wrong")]
        public async Task<ActionResult<Store>> DeleteById(int id)
        {
            var store = await _storeService.FindById(id);

            if (store == null)
                return NotFound();

            await _storeService.DeleteAsync(store);
            return Ok(store);
        }
    }
}