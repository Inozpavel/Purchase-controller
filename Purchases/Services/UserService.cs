using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Purchases.Data;
using Purchases.DTOs;
using Purchases.Entities;

namespace Purchases.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        private readonly IUserRepository _repository;

        public UserService(IConfiguration configuration, IMapper mapper, IUserRepository repository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request)
        {
            var user = await _repository.FindUserAsync(request.Email, HashPassword(request.Password));

            if (user == null)
                return null;

            string token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _repository.FindUserAsync(request.Email) != null)
                return null;

            string password = request.Password;
            var user = _mapper.Map<RegisterRequest, User>(request);
            user.Password = HashPassword(password);

            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            var response = await AuthenticateAsync(new AuthenticateRequest
            {
                Email = user.Email,
                Password = password
            });
            return response;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _repository.GetAllUsersAsync();

        private string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] secret = Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("TOKEN_SECONDS_LIFETIME")),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString())
                }),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256)
            });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            string hashedPassword = BitConverter.ToString(SHA256.HashData(bytes)).Replace("-", "");
            return hashedPassword;
        }
    }
}