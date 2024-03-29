﻿using System.ComponentModel.DataAnnotations;

namespace Purchases.Api.DTOs
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}