using System.ComponentModel.DataAnnotations;

namespace Purchases.Models
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}