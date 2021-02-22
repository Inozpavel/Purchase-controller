using System.Collections.Generic;
using Purchases.Entities;

namespace Purchases.Models
{
    public interface IUserService
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest request);

        AuthenticateResponse? Register(User user);

        User? GetById(int id);

        IEnumerable<User> GetAll();
    }
}