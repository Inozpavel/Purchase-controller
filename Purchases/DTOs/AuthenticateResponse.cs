using System.ComponentModel.DataAnnotations;
using Purchases.Entities;

namespace Purchases.DTOs
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName ?? "";
            LastName = user.LastName ?? "";
            Patronymic = user.Patronymic ?? "";
            Email = user.Email;
            Token = token;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}