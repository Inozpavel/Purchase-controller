using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Purchases.Data;
using Purchases.Entities;
using Purchases.Models;

namespace Purchases.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IUserRepository _repository;

        public UserService(IConfiguration configuration, IUserRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request)
        {
            var user = await _repository.FindUserAsync(request.Email, request.Password);

            if (user == null)
                return null;

            string token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse?> RegisterAsync(User user)
        {
            if (await _repository.FindUserAsync(user.Email) != null)
                return null;

            var addedUser = await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            var response = await AuthenticateAsync(new AuthenticateRequest
            {
                Email = addedUser.Email,
                Password = addedUser.Password
            });
            return response;
        }

        public async Task<User?> GetByIdAsync(int id) => await _repository.FindUserAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync() => await _repository.GetAllUsersAsync();

        private string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] secret = Encoding.UTF8.GetBytes(_configuration["SecretJWTKey"]);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("TokenSecondsLifetime")),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString())
                }),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256)
            });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}