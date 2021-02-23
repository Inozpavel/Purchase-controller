﻿using System.ComponentModel.DataAnnotations;

namespace Purchases.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(64)] //SHA256
        public string Password { get; set; }
    }
}