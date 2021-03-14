using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stores.Api.DTOs
{
    public class ProductRequest
    {
        [Required]
        public string ProductName { get; set; }

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public int CountInStock { get; set; }

        public List<int> CategoriesIds { get; set; }
    }
}