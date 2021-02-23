using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Purchases.Entities;
using Purchases.Models;
using Purchases.Services;

namespace Purchases.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateResponse>> RegisterAsync(RegisterRequest request)
        {
            var response = await _userService.RegisterAsync(request);

            if (response != null)
                return CreatedAtAction("GetById", new {id = response.Id}, response);

            ModelState.AddModelError(nameof(request.Email), "Email is already registered!");
            return BadRequest(ModelState);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
        {
            var response = await _userService.AuthenticateAsync(request);

            if (response == null)
                return BadRequest(new {message = "Email or password is incorrect"});
            return Ok(response);
        }

        [HttpGet("all")]
        // Todo: Remove when permanent database will be added
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            if (!users.Any())
                return NoContent();
            return Ok(users);
        }

        [HttpGet("{id}")]
        // Todo: Remove when permanent database will be added
        public async Task<ActionResult<User>> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}