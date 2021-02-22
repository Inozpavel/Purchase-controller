using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<AuthenticateResponse> Register(User user)
        {
            var response = _userService.Register(user);
            user.Id = response.Id;
            return CreatedAtAction(nameof(Register), new {id = response.Id}, user);
        }

        [HttpPost("authenticate")]
        public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            if (response == null)
                return BadRequest(new {message = "Email or password is incorrect"});
            return Ok(response);
        }

        [HttpGet("all")]
        // Todo: Remove when permanent database will be added
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();
            if (!users.Any())
                return NoContent();
            return Ok(users);
        }
    }
}