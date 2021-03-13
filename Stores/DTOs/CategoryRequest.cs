using System.ComponentModel.DataAnnotations;

namespace Stores.DTOs
{
    public class CategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}