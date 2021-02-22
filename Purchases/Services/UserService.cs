using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public AuthenticateResponse? Authenticate(AuthenticateRequest request)
        {
            var user = _repository.Users.FirstOrDefault(x =>
                x.Email == request.Email && x.Password == request.Password);

            if (user == null)
                return null;

            string token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public AuthenticateResponse? Register(User user)
        {
            if (_repository.Users.Any(
                x => string.Equals(x.Email, user.Email, StringComparison.CurrentCultureIgnoreCase)))
                return null;

            var addedUser = _repository.AddUser(user);
            var response = Authenticate(new AuthenticateRequest
            {
                Email = addedUser.Email,
                Password = addedUser.Password
            });
            return response;
        }

        public User? GetById(int id) => _repository.FindUser(id);

        public IEnumerable<User> GetAll() => _repository.Users;

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
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