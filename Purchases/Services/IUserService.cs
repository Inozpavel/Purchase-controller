using System.Collections.Generic;
using System.Threading.Tasks;
using Purchases.Entities;
using Purchases.Models;

namespace Purchases.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateRequest request);

        Task<AuthenticateResponse?> RegisterAsync(RegisterRequest request);

        Task<User?> GetByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();
    }
}