using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stores.Entities
{
    public class Store
    {
        public int StoreId { get; set; }

        [Required]
        public string StoreName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public List<Product> Products { get; set; }
    }
}