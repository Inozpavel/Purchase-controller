using System.ComponentModel.DataAnnotations;

namespace Stores.Entities
{
    public class Product
    {
        public int StoreId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CountInStock { get; set; }
    }
}