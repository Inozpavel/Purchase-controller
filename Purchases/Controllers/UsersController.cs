using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchases.Entities;
using Purchases.Models;
using Purchases.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Purchases.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        /// <summary>
        ///     Creates a user
        /// </summary>
        /// <param name="request">Information about new user</param>
        /// <returns>Created user with token for authentication</returns>
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AuthenticateResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized,
            "When email is already registered or password is too short", typeof(ProblemDetails))]
        public async Task<ActionResult<AuthenticateResponse>> RegisterAsync(RegisterRequest request)
        {
            var response = await _userService.RegisterAsync(request);

            if (response == null)
                return Unauthorized(new ProblemDetails {Detail = "Email is already registered!"});

            return CreatedAtAction("GetById", new {id = response.Id}, response);
        }

        /// <summary>
        ///     Authorizes existing user
        /// </summary>
        /// <param name="request">User login data</param>
        /// <returns>User information with token for authentication</returns>
        [HttpPost("authenticate")]
        [SwaggerResponse(StatusCodes.Status202Accepted)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "When email or password is incorrect",
            typeof(ProblemDetails))]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
        {
            var response = await _userService.AuthenticateAsync(request);

            if (response == null)
                return Unauthorized(new ProblemDetails {Detail = "Email or password is incorrect"});

            return Accepted(response);
        }

        /// <summary>
        ///     Returns all existing users
        /// </summary>
        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If there are no users")]
        // Todo: Remove when project will be ready
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            if (!users.Any())
                return NoContent();
            return Ok(users);
        }

        /// <summary>
        ///     Returns user with given id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "If user is not found")]
        // Todo: Remove when project will be ready
        public async Task<ActionResult<User>> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}