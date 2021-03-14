using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchases.Api.DTOs;
using Purchases.Api.Entities;
using Purchases.Api.Exceptions;
using Purchases.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Purchases.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Operations about users")]
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
        [SwaggerResponse(StatusCodes.Status202Accepted, Type = typeof(AuthenticateResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized,
            "When email is already registered or password is too short", typeof(ProblemDetails))]
        public async Task<ActionResult<AuthenticateResponse>> Register([Required] RegisterRequest request)
        {
            try
            {
                var response = await _userService.RegisterAsync(request);

                return Accepted(response);
            }
            catch (ApiException e)
            {
                return Unauthorized(new ProblemDetails {Detail = e.Message});
            }
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
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([Required] AuthenticateRequest request)
        {
            try
            {
                var response = await _userService.AuthenticateAsync(request);

                return Accepted(response);
            }
            catch (ApiException e)
            {
                return Unauthorized(new ProblemDetails {Detail = e.Message});
            }
        }

        /// <summary>
        ///     Finds all existing users
        /// </summary>
        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "If there are no users")]
        // Todo: Remove when project will be ready
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            if (!users.Any())
                return NoContent();
            return Ok(users);
        }
    }
}