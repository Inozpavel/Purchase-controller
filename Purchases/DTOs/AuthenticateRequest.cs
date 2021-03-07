using System.ComponentModel.DataAnnotations;

namespace Purchases.DTOs
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}