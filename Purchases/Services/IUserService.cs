using System.Collections.Generic;
using Purchases.Entities;
using Purchases.Models;

namespace Purchases.Services
{
    public interface IUserService
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest request);

        AuthenticateResponse? Register(User user);

        User? GetById(int id);

        IEnumerable<User> GetAll();
    }
}