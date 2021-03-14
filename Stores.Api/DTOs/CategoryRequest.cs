using System.ComponentModel.DataAnnotations;

namespace Stores.Api.DTOs
{
    public class CategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}