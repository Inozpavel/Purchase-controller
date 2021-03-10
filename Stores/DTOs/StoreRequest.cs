using System.ComponentModel.DataAnnotations;

namespace Stores.DTOs
{
    public class StoreRequest
    {
        [Required]
        public string StoreName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}