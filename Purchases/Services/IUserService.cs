using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.DTOs;
using Purchases.Entities;

namespace Purchases.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request);

        Task<AuthenticateResponse?> RegisterAsync(RegisterRequest request);

        Task<IEnumerable<User>> GetAllAsync();
    }
}