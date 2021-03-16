using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable NotNullMemberIsNotInitialized

namespace Purchases.Api.Entities
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        [StringLength(64)] //SHA256
        public string Password { get; set; }

        [JsonIgnore]
        public List<Purchase> Purchases { get; set; }
    }
}