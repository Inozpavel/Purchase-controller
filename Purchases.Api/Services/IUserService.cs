using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Api.DTOs;
using Purchases.Api.Entities;

namespace Purchases.Api.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);

        Task<AuthenticateResponse> RegisterAsync(RegisterRequest request);

        Task<IEnumerable<User>> GetAllAsync();
    }
}