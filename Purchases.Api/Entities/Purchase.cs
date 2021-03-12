using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// ReSharper disable NotNullMemberIsNotInitialized
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Purchases.Api.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public decimal Cost { get; set; }
    }
}