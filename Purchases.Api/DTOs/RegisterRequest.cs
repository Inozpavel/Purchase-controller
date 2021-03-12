using System.ComponentModel.DataAnnotations;

namespace Purchases.Api.DTOs
{
    public class RegisterRequest
    {
        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Patronymic { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [MinLength(8)]
        public string Password { get; init; }
    }
}