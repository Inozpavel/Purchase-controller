using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stores.DTOs;
using Stores.Entities;
using Stores.Services;

namespace Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService) => _storeService = storeService;

        [HttpPost("add")]
        public async Task<ActionResult<Store>> Add([Required] StoreRequest request)
        {
            var addedStore = await _storeService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new {id = addedStore.StoreId}, addedStore);
        }

        [HttpGet("all")]
        public async Task<ActionResult<Store>> All()
        {
            var stores = await _storeService.AllAsync();

            if (!stores.Any())
                return NoContent();

            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetById(int id)
        {
            var store = await _storeService.FindById(id);

            if (store == null)
                return NotFound();

            return Ok(store);
        }

        [HttpDelete("{id}")]
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